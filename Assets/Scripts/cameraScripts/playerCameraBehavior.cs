using UnityEngine;
using UnityEngine.UIElements;

public class playerCameraBehavior : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
        Debug.Log(offset);
    }


    void OnTriggerEnter(Collider onHit)
    {
        if (onHit.tag == "cameraData")
        {
            offset.x = onHit.GetComponent<cameraRotationHolder>().desiredX;
            offset.z = onHit.GetComponent<cameraRotationHolder>().desiredZ;
        }
    }
}
