using UnityEngine;

public class projectile : MonoBehaviour
{
    private float speed;
    private collectibleData playerScore = new collectibleData();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 15f;
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            playerScore.subtractScore(10);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}