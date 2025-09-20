using Unity.VisualScripting;
using UnityEngine;

public class rangedEnemy : basicEnemyAI
{
    public GameObject bullet;
    public Transform bulletOrigin;
    private float wait = 0;

    // Check to see player's location compared to whatever this is attached to. Controls actions the object can take in response to the player
    protected override void Update()
    {
        if (!disableAI)
        {
            wait += Time.deltaTime;
            distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceFromPlayer < 30.0f && wait > 3)
            {
                shoot();
                enemy.velocity = Vector3.zero;
            }
            else if (distanceFromPlayer < 50.0f)
            {
                enemy.SetDestination(player.transform.position);
            }
            else
            {
                enemy.ResetPath();
                enemy.velocity = Vector3.zero;
            }
        }
    }

    // Method for when the Projectile is fired.
    void shoot()
    {
        bulletOrigin.LookAt(player.transform.position + new Vector3(0f, 1f, 0f));
        GameObject g = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
        wait = 0;
    }
}
