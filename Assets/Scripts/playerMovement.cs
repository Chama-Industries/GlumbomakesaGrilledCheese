using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerMovement : MonoBehaviour
{
    // Controls the speed of the player
    private float speed = 25.0f;
    private float rotateSpeed = 180f;
    private Vector3 jumpPower = new Vector3(0, 14.0f, 0);
    private float fallingPower = 5.0f;
    private float maxSpeed = 50.0f;
    // Variables related to the Power Ups the player can aquire
    public colectibleAbility powerUp;
    public bool canKill = false;
    private bool multOnce = true;

    // Rigidbody
    private Rigidbody rb;
    private Animator ani;

    // Variables for a Raycast
    float distanceToGround;

    // 3D movement variable
    Vector3 movementD;

    // Controls
    public KeyCode jump = KeyCode.Space;
    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode special = KeyCode.E;

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
        ani = GetComponent<Animator>();
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
        }
    }

    // FixedUpdate is called at fixed intervals
    void FixedUpdate()
    {
        if(Input.GetKey(sprint))
        {
            maxSpeed = maxSpeed * 2;
        }
        else
        {
            maxSpeed = 20.0f;
        }
    }

    // Basic Directional Movement
    void playerDMove()
    {
        float hIn = Input.GetAxis("Horizontal");
        float vIn = Input.GetAxis("Vertical");

        // Adjusted movement direction based on camera or player orientation
        movementD = new Vector3(vIn, 0, -hIn);
        movementD.Normalize();

        rb.linearVelocity += movementD * speed * Time.deltaTime;
        // This still doesnt feel good, find another way to cap player speed without forcefully halting momentum
        if (rb.linearVelocity.magnitude > 50 && isGrounded())
        {
            //rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }

        // Rotates the player to match the direction of movement
        if (movementD != Vector3.zero)
        {
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
            rb.AddForce(jumpPower, ForceMode.VelocityChange);
        }
        // Code to make movement feel more weighty/less floaty
        if(rb.linearVelocity.y < 1 && !isGrounded())
        {
            rb.linearVelocity += Physics.gravity * fallingPower * Time.deltaTime;
            rb.linearVelocity.Normalize();
        }
    }

    // Using a Raycast to check if the player is able to jump, aka no more flying
    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }

    void playerAbility()
    {
        if (Input.GetKey(special))
        {
            canKill = true;
            if(powerUp != null && multOnce)
            {
                speed = speed * powerUp.speedMult;
                jumpPower = jumpPower * powerUp.jumpMult;
                multOnce = false;
            }
        }
        else
        {
            canKill = false;
            speed = 25.0f;
            jumpPower = new Vector3(0f, 14.0f, 0f);
            multOnce = true;
        }
    }

    public void setPlayerAbility(GameObject g)
    {
        powerUp = g.GetComponent<colectibleAbility>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("levelEnd"))
        {
            canControl = false;
            playerHUD.SetActive(false);
            endingHUD.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}

