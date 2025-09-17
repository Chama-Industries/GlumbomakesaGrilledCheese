using UnityEngine;
using UnityEngine.UIElements;

public class playerCameraBehavior : MonoBehaviour
{
    // Reference to the player
    public GameObject player;
    // offset for 3rd Person view
    public Vector3 offset = new Vector3(-15, 5, 0);
    // offset for "1st" person view
    private Vector3 swivelOffset;

    // gets the difference between the player and the camera's positions
    void Start()
    {
        if(offset == new Vector3(0, 0, 0) || offset == null)
        {
            offset = transform.position - player.transform.position;
        }
    }

    // Depending on player input the camera will either be in 1st or 3rd person
    void LateUpdate()
    {
        // keeps the camera moving with the player
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
        
    }
}
