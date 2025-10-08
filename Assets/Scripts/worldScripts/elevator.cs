using Unity.VisualScripting;
using UnityEngine;

public class elevator : MonoBehaviour
{
    private Vector3 startpoint;
    public GameObject endpoint;
    private Vector3 endpointCoordinates;

    private bool elevatorActive = false;
    private bool moveUp = true;
    private bool moveDown = false;
    private bool finishCycle = false;
    public float elevatorSpeed = 3.0f;
    private float delay = 0;
    public float timeStoppedAtEnds = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpoint = this.gameObject.transform.position;
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
        if((finishCycle || elevatorActive) && moveUp)
        {
            if(this.transform.position.y < endpointCoordinates.y)
            {
                transform.Translate(Vector3.up * Time.deltaTime * elevatorSpeed);
            }
            if(this.transform.position.y >= endpointCoordinates.y && !moveDown)
            {
                delay += Time.deltaTime;
                if(delay > timeStoppedAtEnds)
                {
                    moveUp = false;
                    moveDown = true;
                    delay = 0;
                }
            }
        }
        if((finishCycle || elevatorActive) && moveDown)
        {
            if(this.transform.position.y > startpoint.y)
            {
                transform.Translate(Vector3.down * Time.deltaTime * elevatorSpeed);
            }
            if (this.transform.position.y <= startpoint.y && !moveUp)
            {
                delay += Time.deltaTime;
                if (delay > timeStoppedAtEnds)
                {
                    moveUp = true;
                    moveDown = false;
                    delay = 0;
                }
                if (finishCycle)
                {
                    finishCycle = false;
                }
            }
        }
       
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            elevatorActive = true;
        }
        if(other.gameObject.GetComponent<Rigidbody>() != null && moveUp)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * elevatorSpeed, ForceMode.Force);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            elevatorActive = false;
        }
        if(this.transform.position.y >= startpoint.y && this.transform.position.y <= endpointCoordinates.y)
        {
            finishCycle = true;
        }    
    }
}
