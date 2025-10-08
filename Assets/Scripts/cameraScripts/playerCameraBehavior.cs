using UnityEngine;
using UnityEngine.UIElements;

public class playerCameraBehavior : MonoBehaviour
{
    // Reference to the player
    public GameObject player;
    // offset for 3rd Person view
    public Vector3 currentOffset = new Vector3(-35, 10, 0);

    public Vector3[] allOffsets = new Vector3[4];

    // gets the difference between the player and the camera's positions
    void Start()
    {
        // North
        allOffsets[0] = new Vector3(-35, 10, 0);
        // East
        allOffsets[1] = new Vector3(0, 10, 35);
        // South
        allOffsets[2] = new Vector3(35, 10, 0);
        // West
        allOffsets[3] = new Vector3(0, 10, -35);
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
    public void changePerspective(bool n, bool e, bool s, bool w)
    {
        if (n)
        {
            currentOffset = allOffsets[0];
        }
        if (e)
        {
            currentOffset = allOffsets[1];
        }
        if(s)
        {
            currentOffset = allOffsets[2];
        }
        if(w)
        {
            currentOffset = allOffsets[3];
        }
    }
}

