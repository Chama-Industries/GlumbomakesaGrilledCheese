using UnityEngine;

public class cameraDataHolder : MonoBehaviour
{
    public bool lookNorth = true;
    public bool lookSouth = false;
    public bool lookEast = false;
    public bool lookWest = false;

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
            theCamera.GetComponent<playerCameraBehavior>().changePerspective(lookNorth, lookEast, lookSouth, lookWest);
        }
    }
}
