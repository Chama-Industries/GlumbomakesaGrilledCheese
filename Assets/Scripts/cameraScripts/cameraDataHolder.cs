using UnityEngine;

public class cameraDataHolder : MonoBehaviour
{
    // Notes for where to set the Camera's offset to get the correct view
    // North/N - X: -15, Z: 0
    // East/E - X: 0, Z: 15
    // South/S - X: 15, Z: 0
    // West/W - X: 0, Z: -15
    // Variables to set the offset to
    public float desiredX = 0;
    public float desiredZ = 0;

    // reference to the main camera
    public GameObject theCamera; 

    // gets the main camera
    void Start()
    {
        theCamera = GameObject.FindWithTag("MainCamera");
    }

    // if the player enters the trigger the camera's offset will update
    void OnTriggerEnter(Collider onHit)
    {
        if (onHit.tag == "player")
        {
            theCamera.GetComponent<playerCameraBehavior>().offset.x = desiredX;
            theCamera.GetComponent<playerCameraBehavior>().offset.z = desiredZ;
        }
    }
}
