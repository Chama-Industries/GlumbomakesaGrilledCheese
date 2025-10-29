using UnityEngine;
using UnityEngine.UIElements;

public class destroyableObject : MonoBehaviour
{
    public int objectHP = 1;
    public int objectStrength = 1;
    public int flingForce = 10;
    private Rigidbody rb;
    private Collider objCollider;

    public bool crumble = false;
    public bool cannotDestroy = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        getCollider(this.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Vector3 recoilDirection = collision.collider.gameObject.transform.position - this.gameObject.transform.position;
        recoilDirection = new Vector3(recoilDirection.x, 2.5f, recoilDirection.z);
        recoilDirection.Normalize();
        if (collision.gameObject.tag == "player")
        {
            if (objectStrength > collision.gameObject.GetComponent<playerMovement>().destructionStrength)
            {
                collision.gameObject.GetComponent<playerMovement>().haltPlayer();
                collision.gameObject.GetComponent<Rigidbody>().AddForce(recoilDirection * flingForce, ForceMode.Impulse);
            }
            else if(objectStrength == collision.gameObject.GetComponent<playerMovement>().destructionStrength)
            {
                collision.gameObject.GetComponent<playerMovement>().haltPlayer();
                collision.gameObject.GetComponent<Rigidbody>().AddForce(recoilDirection * flingForce, ForceMode.Impulse);
                // State Change to show Damage
                objectHP--;
            }
            else
            {
                objectHP--;
            }
        }
        if (objectHP == 0)
        {
            if (crumble)
            {
                Destroy(this.gameObject);
            }
            else
            {
                rb.linearDamping = 0;
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(-recoilDirection * 50.0f, ForceMode.Impulse);
                rb.AddTorque(Vector3.up * 10.0f, ForceMode.Impulse);
                objCollider.enabled = false;
                Destroy(this.gameObject, 1.0f);
            }
        }
    }

    // Meant for use when a collision happens with Triggers (ex. the Player's attack)
    public void impactReaction(Collider col)
    {
        Vector3 flyDirection = col.gameObject.transform.position - this.gameObject.transform.position;
        flyDirection.Normalize();

        if (objectHP > 1)
        {
            objectHP--;
        }
        else
        {
            rb.linearDamping = 0;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(-flyDirection * 50.0f, ForceMode.Impulse);
            rb.AddTorque(Vector3.up * 10.0f, ForceMode.Impulse);
            objCollider.enabled = false;
            Destroy(this.gameObject, 1.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "enemy")
        {
            impactReaction(other);
        }
    }

    private void getCollider(GameObject r)
    {
        if (r.GetComponent<Collider>() == null)
        {
            getCollider(r.transform.GetChild(0).gameObject);
        }
        else
        {
            objCollider = this.GetComponent<Collider>();
        }
    }
}
