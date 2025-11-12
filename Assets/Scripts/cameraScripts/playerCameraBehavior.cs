using UnityEngine;
using UnityEngine.UIElements;

public class playerCameraBehavior : MonoBehaviour
{
    // Reference to the player
    public GameObject player;
    // offset for 3rd Person view
    public Vector3 currentOffset = new Vector3(-35, 10, 0);

    public Vector3[] allOffsets = new Vector3[2];

    // gets the difference between the player and the camera's positions
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("player");
        }
        // North
        allOffsets[0] = new Vector3(-35, 10, 0);
        // South
        allOffsets[1] = new Vector3(35, 10, 0);
        if (currentOffset == new Vector3(0, 0, 0) || currentOffset == null)
        {
            currentOffset = transform.position - player.transform.position;
        }
    }

    void LateUpdate()
    {
        // keeps the camera moving with the player
        transform.position = player.transform.position + currentOffset;
        transform.LookAt(player.transform.position);
    }

    //Uses another object to trigger perspecitve changes based on what booleans are true
    public void changePerspective(bool n, bool s)
    {
        if (n)
        {
            currentOffset = allOffsets[0];
            player.GetComponent<playerMovement>().flipMovementDirection(!n);
        }
        if(s)
        {
            currentOffset = allOffsets[1];
            player.GetComponent<playerMovement>().flipMovementDirection(s);
        }
    }
}

