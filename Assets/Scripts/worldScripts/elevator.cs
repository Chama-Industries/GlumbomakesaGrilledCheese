using Unity.VisualScripting;
using UnityEngine;

public class elevator : MonoBehaviour
{
    private Transform startpoint;
    public GameObject endpoint;
    private Vector3 endpointCoordinates;

    private bool elevatorActive = false;
    private bool cycle = false;
    public float elevatorSpeed = 3.0f;
    private float counter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpoint = transform;
        if(endpoint == null)
        {
            endpointCoordinates = new Vector3(transform.position.x, transform.position.y + 10.0f, transform.position.z);
        }
        else
        {
            endpointCoordinates = endpoint.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(elevatorActive && !cycle)
        {
            if(this.transform.position.y < endpointCoordinates.y)
            {
                transform.Translate(Vector3.up * Time.deltaTime * elevatorSpeed, Space.Self);
            }
        }
        if(elevatorActive && cycle)
        {
            if (this.transform.position.y > startpoint.position.y)
            {
                transform.Translate(Vector3.down * Time.deltaTime * elevatorSpeed, Space.Self);
            }
        }

        if((this.transform.position.y >= endpointCoordinates.y || this.transform.position.y <= startpoint.position.y) && counter > 1)
        {
            cycle = !cycle;
            counter = 0;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            elevatorActive = true;
        }
    }
}
