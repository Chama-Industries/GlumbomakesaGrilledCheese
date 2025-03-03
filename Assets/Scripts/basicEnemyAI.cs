using UnityEngine;
using UnityEngine.AI;

public class basicEnemyAI : MonoBehaviour
{
    public GameObject player;
    protected double distanceFromPlayer;
    protected NavMeshAgent enemy;
    protected collectibleData playerScore = new collectibleData();
    public int scoreDamage = 0;
    // Rigidbody
    private Rigidbody rb;

    protected virtual void Start()
    {
        //ensures that we have the *ONLY* player in the game. Allows for enemies to create other enemies and saves me a headache
        if(player == null)
        {
            player = GameObject.FindWithTag("player");
        }
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();
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
        if (collision.gameObject.tag == "player")
        {
            rb.AddForce(collision.GetContact(0).normal * 10.0f, ForceMode.Impulse);
            playerScore.subtractScore(scoreDamage);
        }
    }
}
