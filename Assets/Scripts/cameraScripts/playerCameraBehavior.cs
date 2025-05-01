using UnityEngine;
using UnityEngine.UIElements;

public class playerCameraBehavior : MonoBehaviour
{
    // Reference to the player
    public GameObject player;
    // offset for 3rd Person view
    public Vector3 offset;
    // offset for "1st" person view
    private Vector3 swivelOffset;

    // gets the difference between the player and the camera's positions
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Depending on player input the camera will either be in 1st or 3rd person
    void LateUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            // gets the offset using the mouse's position on the screen
            swivelOffset.x += Input.GetAxis("Mouse X") * 3;
            swivelOffset.y -= Input.GetAxis("Mouse Y") * 3;

            // Keeps the Y value normal and not allow the player to shake the mouse and get motion sickness
            swivelOffset.y = Mathf.Clamp(swivelOffset.y, 0.0f, 80.0f);
            Quaternion rotationSet = Quaternion.Euler(swivelOffset.y, swivelOffset.x, 0.0f);
            // setting player rotation relevant to mouse position on the screen
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotationSet, Time.deltaTime * 10);
            // puts the camera right above the player
            transform.position = player.transform.position + new Vector3 (0, 4.5f, 0);
        }
        else
        {
            // keeps the camera moving with the player
            transform.position = player.transform.position + offset;
            transform.LookAt(player.transform.position);
        }
    }
}
