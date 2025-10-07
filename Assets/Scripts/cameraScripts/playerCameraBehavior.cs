using UnityEngine;
using UnityEngine.UIElements;

public class playerCameraBehavior : MonoBehaviour
{
    // Reference to the player
    public GameObject player;
    // offset for 3rd Person view
    public Vector3 currentOffset = new Vector3(-15, 5, 0);

    // gets the difference between the player and the camera's positions
    void Start()
    {
        if(currentOffset == new Vector3(0, 0, 0) || currentOffset == null)
        {
            currentOffset = transform.position - player.transform.position;
        }
    }

    // Depending on player input the camera will either be in 1st or 3rd person
    void LateUpdate()
    {
        // keeps the camera moving with the player
        transform.position = player.transform.position + currentOffset;
        transform.LookAt(player.transform.position);
        
    }

    //slowly move to the new offset
    private void smoothTransition(cameraData desiredPosition, cameraData currentPosition)
    {

    }
}


public class cameraData
{
    private Vector3 offsetValue;
    private string direction;

    public cameraData(Vector3 inValue, string inCompass)
    {
        offsetValue = inValue;
        direction = inCompass;
    }

    public void setOffset(Vector3 inValue)
    {
        offsetValue = inValue;
    }

    public Vector3 getOffset()
    {
        return offsetValue;
    }

    public void setDirection(string inD)
    {
        direction = inD;
    }

    public string getDirection()
    {
        return direction;
    }
}
