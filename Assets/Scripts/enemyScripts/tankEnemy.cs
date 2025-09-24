using UnityEngine;

public class tankEnemy : basicEnemyAI
{
    public override void takeDamage(Collider col)
    {
        // WIP

        // Meant to be a harder enemy to take down as it launches the player away from it when it's damaged
        Vector3 flyDirection = col.gameObject.transform.position - this.gameObject.transform.position;
        flyDirection.Normalize();
        if (HP > 1)
        {
            HP--;
            col.gameObject.GetComponent<playerMovement>().haltPlayer();
            col.gameObject.GetComponent<Rigidbody>().AddForce(flyDirection * 25.0f, ForceMode.Impulse);
        }
        else
        {
            rb.linearDamping = 0;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(-flyDirection * 100.0f, ForceMode.Impulse);
            rb.AddTorque(Vector3.up * 20.0f, ForceMode.Impulse);
            rb.AddTorque(Vector3.left * 20.0f, ForceMode.Impulse);
            this.GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, 1.0f);
        }
    }
}
