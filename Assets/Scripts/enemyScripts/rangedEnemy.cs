using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class rangedEnemy : basicEnemyAI
{
    public GameObject bullet;
    public Transform bulletOrigin;
    private float bulletDelay = 0;
    public int volleyCount = 1;
    public float volleyDelay = 0.5f;
    public float timeToAttack = 3;

    protected override void Update()
    {
        if (!disableAI)
        {
            bulletDelay += Time.deltaTime;
            distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceFromPlayer < distanceToStopFromPlayer * 1.1f && bulletDelay > timeToAttack)
            {
                StartCoroutine(Shoot());
                enemy.velocity = Vector3.zero;
                bulletDelay = 0;
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

    // Fires a number of bullets defined by volleyCount, waiting between firing each bullet defined by volleyDelay
    IEnumerator Shoot()
    {
        for (int c = 0; c < volleyCount; c++)
        {
            this.transform.LookAt(player.transform.position);
            bulletOrigin.LookAt(player.transform.position + new Vector3(0f, 1f, 0f));
            GameObject g = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
            yield return new WaitForSeconds(volleyDelay);
        }
        yield break;
    }
}
