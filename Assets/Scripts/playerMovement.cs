using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerMovement : MonoBehaviour
{
    // Controls the speed of the player
    private float speed = 25.0f;
    private float rotateSpeed = 180f;
    private Vector3 jumpPower = new Vector3(0, 12.0f, 0);
    private Vector3 fallingPower = new Vector3(0, -0.4f, 0);
    private bool isSprinting = false;
    private int counter = 0;
    //private float lastClamp;

    // Rigidbody
    private Rigidbody rb;

    // Variables for a Raycast
    float distanceToGround;

    // 3D movement variable
    Vector3 movementD;

    // Controls
    public KeyCode jump = KeyCode.Space;

    // Booleans to control Reaction Images based on player Speed
    // Think like a collection of booleans that all respond to the same call and return whichever one is active
    // IEnumerator Coroutine?


    // Start is called before the first frame update
    void Start()
    {
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();
        // Setting the player's distance from the ground
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
    }

    // FixedUpdate is called at fixed intervals
    void FixedUpdate()
    {
        if (Input.GetKey(jump) && isGrounded())
        {
            rb.AddForce(jumpPower, ForceMode.VelocityChange);
        }

        if (rb.linearVelocity.magnitude > 1)
        {
            counter++;
            if (counter > 100)
            {
                isSprinting = true;
            }
        }
        else
        {
            counter = 0;
            isSprinting = false;
        }
    }

    // Basic WASD movement controls
    void playerMove()
    {
        float hIn = Input.GetAxis("Horizontal");
        float vIn = Input.GetAxis("Vertical");

        // Adjusted movement direction based on camera or player orientation
        movementD = new Vector3(vIn, 0, -hIn);
        movementD.Normalize();

        rb.linearVelocity += movementD * speed * Time.deltaTime;
        if (rb.linearVelocity.magnitude > 10 && !isSprinting)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 12.5f);
            //lastClamp = 20.0f;
        }
        if (isSprinting && rb.linearVelocity.magnitude > 10)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 40.0f);
            //lastClamp = 60.0f;
        }

        // Rotate the player in the direction of movement
        if (movementD != Vector3.zero)
        {
            Quaternion rotationD = Quaternion.LookRotation(movementD, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationD, rotateSpeed * Time.deltaTime);
        }

        /*doesn't work, overriden by previous statements
        if (hIn == 0 && vIn == 0)
        {
            lastClamp = lastClamp * 0.75f;
            //Stopping is too abrubt, maybe have a loop continuiously clamp down instead of hard clamping?
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, lastClamp);
        }
        */
    }

    // Using a Raycast to check if the player is able to jump, aka no more flying
    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }
}

