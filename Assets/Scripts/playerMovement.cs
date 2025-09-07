using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerMovement : MonoBehaviour
{
    // Controls the speed of the player
    public float speed = 10.0f;
    private float maxWalkSpeed = 20.0f;
    private float maxRunSpeed = 50.0f;
    private float runDelayCounter = 0;
    private float runDelay = 5;
    private float rotateSpeed = 180f;
    private Vector3 jumpPower = new Vector3(0, 14.0f, 0);
    private Vector3 fallingPower = new Vector3(0, -5.0f, 0);

    // Variables related to the Power Ups the player can aquire
    public Transform attackOrigin;
    public GameObject attackObject;
    public colectibleAbility powerUp;

    // Rigidbody
    private Rigidbody rb;
    private Animator ani;
    private bool playJumpAni = false;

    // Variables for a Raycast
    float distanceToGround;

    // 3D movement variable
    Vector3 movementD;

    // Controls
    // Directional Keys
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    // Verticality Keys
    public KeyCode jump = KeyCode.Space;
    public KeyCode fall = KeyCode.LeftShift;
    // Combat Keys
    public KeyCode attack = KeyCode.Mouse0;
    public KeyCode special = KeyCode.Mouse1;

    // temporary variables related to ending the game
    private bool canControl = true;
    public GameObject playerHUD;
    public GameObject endingHUD;


    // Start is called before the first frame update
    void Start()
    {
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();
        // Get Animator component
        ani = GetComponentInChildren<Animator>();
        ani.SetFloat("playerVelocity", rb.linearVelocity.magnitude);
        ani.SetBool("playerJump", playJumpAni);
        // Setting the player's distance from the ground
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl)
        {
            playerDMove();
            playerVMove();
            playerAbility();
            playerAttack();
        }
    }

    // currently houses random things
    private void FixedUpdate()
    {
        // If everything is on fire hit the explode button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        // Stops Jump Animation from becoming the Idle animation for whatever reason.
        playJumpAni = !isGrounded();

        // Stop people falling into the void if they somehow get there. REMOVE LATER
        if (this.transform.position.y < -150) 
        {
            haltPlayer();
            this.transform.position = new Vector3(0f, 10f, 0f);
        }
    }

    // Basic Directional Movement
    void playerDMove()
    {
        float hIn = Input.GetAxis("Horizontal");
        float vIn = Input.GetAxis("Vertical");

        // Adjusted movement direction based on camera or player orientation
        movementD = new Vector3(vIn, 0, -hIn);
        // Keep Force Multiplication out of the initial movement directional calculation
        movementD.Normalize();

        // Where we move the player
        rb.AddForce(movementD * speed, ForceMode.Acceleration);

        //really ugly code keeping the player from going too fast (less effective vs diagonals)
        if(rb.linearVelocity.x > maxWalkSpeed && runDelayCounter < runDelay)
        {
            rb.linearVelocity = new Vector3(maxWalkSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        else if (rb.linearVelocity.x > maxRunSpeed)
        {
            rb.linearVelocity = new Vector3(maxRunSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        if (rb.linearVelocity.x < -maxWalkSpeed && runDelayCounter < runDelay)
        {
            rb.linearVelocity = new Vector3(-maxWalkSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        else if (rb.linearVelocity.x < -maxRunSpeed)
        {
            rb.linearVelocity = new Vector3(-maxRunSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }


        if (rb.linearVelocity.z > maxWalkSpeed && runDelayCounter < runDelay)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, maxWalkSpeed);
        }
        else if(rb.linearVelocity.z > maxRunSpeed)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, maxRunSpeed);
        }
        if (rb.linearVelocity.z < -maxWalkSpeed && runDelayCounter < runDelay)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, -maxWalkSpeed);
        }
        else if(rb.linearVelocity.z < -maxRunSpeed)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, -maxRunSpeed);
        }


        // Rotates the player to match the direction of movement
        if (movementD != Vector3.zero)
        {
            runDelayCounter += Time.deltaTime;
            ani.Play("glumboRunCycle");
            Quaternion rotationD = Quaternion.LookRotation(movementD, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationD, rotateSpeed * Time.deltaTime);
        }
        else
        {
            runDelayCounter = 0;
        }
    }


    // Basic Vertical Movement
    void playerVMove()
    {
        // Code to apply a vertical force to the player
        if (Input.GetKeyDown(jump) && isGrounded())
        {
            ani.Play("glumboJump");
            rb.AddForce(jumpPower, ForceMode.VelocityChange);
        }
        if(Input.GetKey(fall))
        {
            rb.AddForce(fallingPower, ForceMode.Acceleration);
        }
    }

    void playerAttack()
    {
        if(Input.GetKeyDown(attack))
        {
            Instantiate(attackObject, attackOrigin.position, attackOrigin.rotation);
        }
    }

    // Ends the Level
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("levelEnd"))
        {
            canControl = false;
            playerHUD.SetActive(false);
            endingHUD.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    // Using a Raycast to check if the player is able to jump, aka no more flying
    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }

    // Unused Method for a check to playing a Falling Animation
    public bool isFalling()
    {
        return rb.linearVelocity.y < 0;
    }

    // Makes Mouse 0 do things when pressed
    void playerAbility()
    {
        if (Input.GetKey(special))
        {
            Debug.Log(rb.linearVelocity);
        }
    }

    // Get the Game Object with the script.
    public void setPlayerAbility(GameObject g)
    {
        powerUp = g.GetComponent<colectibleAbility>();
    }

    // Method for me to call to just stop the player whenever
    public void haltPlayer()
    {
        rb.linearVelocity = Vector3.zero;
    }
}

