using UnityEngine;

public class cameraDataHolder : MonoBehaviour
{
    // North/N - X: -15, Z: 0
    // East/E - X: 0, Z: 15
    // South/S - X: 15, Z: 0
    // West/W - X: 0, Z: -15
    public float desiredX = 0;
    public float desiredZ = 0;

    public GameObject theCamera; 

    void Start()
    {
        theCamera = GameObject.FindWithTag("MainCamera");
    }
    void OnTriggerEnter(Collider onHit)
    {
        if (onHit.tag == "player")
        {
            theCamera.GetComponent<playerCameraBehavior>().offset.x = desiredX;
            theCamera.GetComponent<playerCameraBehavior>().offset.z = desiredZ;
        }
    }
}
