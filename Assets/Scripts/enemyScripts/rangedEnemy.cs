using Unity.VisualScripting;
using UnityEngine;

public class rangedEnemy : basicEnemyAI
{
    public GameObject bullet;
    public Transform bulletOrigin;
    private int wait = 0;
    private bool hasFired = false;

    // Update is called once per frame
    protected override void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceFromPlayer < 30.0f && !hasFired)
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

    void FixedUpdate()
    {
        if (wait == 200 && hasFired)
        {
            wait = 0;
            hasFired = false;
        }
        else
        {
            wait++;
        }
    }

    void shoot()
    {
        bulletOrigin.LookAt(player.transform.position + new Vector3(0f, 1f, 0f));
        GameObject g = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
        hasFired = true;
    }
}
