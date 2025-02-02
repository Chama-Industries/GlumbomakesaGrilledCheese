using UnityEngine;
using TMPro;

public class playerMovement : MonoBehaviour
{
    // Controls the speed of the player
    private float speed = 2.0f;
    private float rotateSpeed = 180f;
    private Vector3 jumpPower = new Vector3(0, 10.0f, 0);
    private Vector3 fallingPower = new Vector3(0, -0.4f, 0);

    // Rigidbody
    private Rigidbody rb;

    // Variables for a Raycast
    float distanceToGround;

    // 3D movement variable
    Vector3 movementD;

    // Controls
    public KeyCode jump = KeyCode.Space;

    // Variables to Control & Display the Player's Score
    public TextMeshProUGUI theScore;
    private collectibleData playerScore = new collectibleData();
    private int scoreCheck;

    // Start is called before the first frame update
    void Start()
    {
        // Assigns player speed
        speed = 25f;
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();
        // Score Reference
        scoreCheck = playerScore.getScore();
        // Setting the player's distance from the ground
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        updateScore();
    }

    // FixedUpdate is called at fixed intervals
    void FixedUpdate()
    {
        if (Input.GetKey(jump) && isGrounded())
        {
            rb.AddForce(jumpPower, ForceMode.VelocityChange);
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
        if (rb.linearVelocity.magnitude > 10)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 25.0f);
        }

        //transform.Translate(movementD * speed * Time.deltaTime, Space.World);

        // Rotate the player in the direction of movement
        if (movementD != Vector3.zero)
        {
            Quaternion rotationD = Quaternion.LookRotation(movementD, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationD, rotateSpeed * Time.deltaTime);
        }
    }

    void updateScore()
    {
        if (scoreCheck != playerScore.getScore())
        {
            theScore.text = "Score: " + playerScore.getScore() + " ";
            scoreCheck = playerScore.getScore();
        }
    }

    // Using a Raycast to check if the player is able to jump, aka no more flying
    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }
}

