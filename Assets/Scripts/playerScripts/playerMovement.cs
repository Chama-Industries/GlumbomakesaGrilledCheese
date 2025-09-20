using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerMovement : MonoBehaviour
{
    // Controls the speed of the player
    public float speed = 10.0f;
    private float maxWalkSpeed = 20.0f;
    private float maxRunSpeed = 30.0f;
    private float runDelayCounter = 0;
    private float runDelay = 5;
    private float drag = 3;
    private float rotateSpeed = 500f;
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
    private bool grounded;

    // 3D movement variable
    Vector3 movementD;

    // Controls
    // Directional Keys
    [HideInInspector] public KeyCode forward = KeyCode.W;
    [HideInInspector] public KeyCode backward = KeyCode.S;
    [HideInInspector] public KeyCode left = KeyCode.A;
    [HideInInspector] public KeyCode right = KeyCode.D;
    // Verticality Keys
    [HideInInspector] public KeyCode jump = KeyCode.Space;
    [HideInInspector] public KeyCode fall = KeyCode.LeftShift;
    // Combat Keys
    [HideInInspector] public KeyCode attack = KeyCode.Mouse0;
    [HideInInspector] public KeyCode special = KeyCode.Mouse1;

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
        grounded = isGrounded();
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
        //removes movement speed cap while in Air
        if (isGrounded())
        {
            rb.linearDamping = drag;
        }
        else
        {
            rb.linearDamping = 0;
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
        rb.AddForce(movementD * speed, ForceMode.Force);

        limitMovementSpeed();

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

    void limitMovementSpeed()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if(flatVelocity.magnitude > maxWalkSpeed && runDelay > runDelayCounter)
        {
            Vector3 cappedVelocity = flatVelocity.normalized * maxWalkSpeed;
            rb.linearVelocity = new Vector3(cappedVelocity.x, rb.linearVelocity.y, cappedVelocity.z);
        }
           else if(flatVelocity.magnitude > maxRunSpeed)
            {
            Vector3 cappedVelocity = flatVelocity.normalized * maxRunSpeed;
            rb.linearVelocity = new Vector3(cappedVelocity.x, rb.linearVelocity.y, cappedVelocity.z);
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

    // Allows for the Player to Damage/Kill enemies at High Speeds
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy" && rb.linearVelocity.magnitude > maxRunSpeed - maxWalkSpeed)
        {
            if (collision.gameObject.GetComponent<basicEnemyAI>() != null)
            {
                collision.gameObject.GetComponent<basicEnemyAI>().takeDamage(this.GetComponent<Collider>());
            }
        }
    }

    // Using a Raycast to check if the player is touching a surface
    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, transform.position.y * 0.55f);
    }

    // Unused Method for a check to playing a Falling Animation
    public bool isFalling()
    {
        return rb.linearVelocity.y < 0;
    }

    // Makes Mouse 2 do things when pressed
    void playerAbility()
    {
        if (Input.GetKey(special))
        {
            // Later
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

