using UnityEngine;

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
        transform.rotation = player.transform.rotation;
        transform.LookAt(player.transform.position);
    }

}
