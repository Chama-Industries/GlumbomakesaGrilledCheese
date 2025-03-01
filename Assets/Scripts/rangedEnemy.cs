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
        if (distanceFromPlayer < 25.0f)
        {
            if (distanceFromPlayer < 10.0f && !hasFired)
            {
                shoot();
            }
            pursuit();
        }
    }

    void FixedUpdate()
    {
        if (wait == 400)
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
        GameObject g = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
        hasFired = true;
        Destroy(g, 4.0f);
    }
}
