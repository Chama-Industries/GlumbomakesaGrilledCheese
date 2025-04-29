using UnityEngine;
using UnityEngine.UIElements;

public class playerCameraBehavior : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    private Vector3 swivelOffset;


    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            swivelOffset.x += Input.GetAxis("Mouse X") * 3;
            swivelOffset.y -= Input.GetAxis("Mouse Y") * 3;

            swivelOffset.y = Mathf.Clamp(swivelOffset.y, 0.0f, 80.0f);
            Quaternion rotationSet = Quaternion.Euler(swivelOffset.y, swivelOffset.x, 0.0f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotationSet, Time.deltaTime * 10);
            transform.position = player.transform.position + new Vector3 (0, 4.5f, 0);
        }
        else
        {
            transform.position = player.transform.position + offset;
            transform.LookAt(player.transform.position);
        }
    }
}
