using UnityEngine;
using UnityEngine.AI;

public class basicEnemyAI : MonoBehaviour
{
    public GameObject player;
    protected double distanceFromPlayer;
    protected NavMeshAgent enemy;
    protected collectibleData playerScore = new collectibleData();
    public int scoreDamage = 0;

    protected virtual void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    protected virtual void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceFromPlayer < 25.0f)
        {
            pursuit();
        }
    }

    protected virtual void pursuit()
    {
        enemy.SetDestination(player.transform.position);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            playerScore.subtractScore(scoreDamage);
        }
    }
}
