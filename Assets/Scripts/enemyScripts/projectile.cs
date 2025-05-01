using UnityEngine;

public class projectile : MonoBehaviour
{
    private float speed;
    private collectibleData playerScore = new collectibleData();
    // Setting of Variables
    void Start()
    {
        speed = 15f;
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
    }

    // since projectiles are separate from enemies they need their own check
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