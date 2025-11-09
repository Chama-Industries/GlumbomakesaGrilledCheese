using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class playerMovement : MonoBehaviour
{
    // Controls the speed of the player
    public float speed = 15.0f;
    private float maxNormalSpeed = 30.0f;
    private float maxBoostSpeed = 45.0f;
    private bool overclockSpeed = false;
    private float drag = 3;
    private float rotateSpeed = 500f;
    private Vector3 jumpPower = new Vector3(0, 30.0f, 0);
    private Vector3 fallingPower = new Vector3(0, -10.0f, 0);
    public int destructionStrength = 1;
    // Variables that dictate movement direction
    protected float hIn;
    protected float vIn;

    // Variables related to the Power Ups the player can aquire
    public Transform attackOrigin;
    public GameObject attackObject;
    public colectibleAbility powerUp;

    // Rigidbody
    private Rigidbody rb;
    private Animator ani;

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
        grounded = !isGrounded();
        if (isGrounded())
        {
            ani.SetBool("isAirborne", grounded);
            rb.linearDamping = drag;
        }
        else
        {
            ani.SetBool("isAirborne", grounded);
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
    }

    // Basic Directional Movement
    void playerDMove()
    {
        hIn = Input.GetAxis("Horizontal");
        vIn = Input.GetAxis("Vertical");

        // Adjusted movement direction based on camera or player orientation
        movementD = new Vector3(vIn, 0, -hIn);
        // Keep Force Multiplication out of the initial movement directional calculation
        movementD.Normalize();

        ani.SetFloat("movementMagnitude", movementD.magnitude);
        // Where we move the player
        // AddRelativeForce can get movement better with the camera but its kinda jank, find a good fix/tutorial later
        rb.AddForce(movementD * speed, ForceMode.Force);

        limitMovementSpeed();

        // Rotates the player to match the direction of movement
        if (movementD != Vector3.zero)
        {
            ani.Play("runCycle");
            Quaternion rotationD = Quaternion.LookRotation(movementD, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationD, rotateSpeed * Time.deltaTime);
        }
    }


    // Basic Vertical Movement
    void playerVMove()
    {
        // Code to apply a vertical force to the player
        if (Input.GetKeyDown(jump) && isGrounded())
        {
            ani.Play("jump");
            rb.AddForce(jumpPower, ForceMode.Impulse);
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
        if(flatVelocity.magnitude > maxNormalSpeed && !overclockSpeed)
        {
            Vector3 cappedVelocity = flatVelocity.normalized * maxNormalSpeed;
            rb.linearVelocity = new Vector3(cappedVelocity.x, rb.linearVelocity.y, cappedVelocity.z);
        }
           else if(flatVelocity.magnitude > maxBoostSpeed)
            {
            Vector3 cappedVelocity = flatVelocity.normalized * maxBoostSpeed;
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
        if(collision.gameObject.tag == "enemy" && rb.linearVelocity.magnitude > maxNormalSpeed)
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
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
    }

    // Unused Method for a check to playing a Falling Animation
    public bool isFalling()
    {
        return rb.linearVelocity.y < 0;
    }

    // Method Call for other Scripts if they need to check if the player is Moving
    public bool isPlayerMoving()
    {
        if(rb.linearVelocity.magnitude > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    public void resetBoostedSpeed()
    {
        if (overclockSpeed)
        {
            overclockSpeed = false;
        }
    }
}

