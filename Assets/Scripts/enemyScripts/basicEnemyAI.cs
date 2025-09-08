using UnityEngine;
using UnityEngine.AI;

public class basicEnemyAI : MonoBehaviour
{
    public GameObject player;
    protected double distanceFromPlayer;
    protected NavMeshAgent enemy;
    protected collectibleData playerScore = new collectibleData();
    public int scoreDamage = 0;
    public int HP;
    // Rigidbody
    protected Rigidbody rb;
    // Animator
    protected Animator ani;

    protected virtual void Start()
    {
        //ensures that we have the *ONLY* player in the game. Allows for enemies to create other enemies and saves me a headache
        if(player == null)
        {
            player = GameObject.FindWithTag("player");
        }
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();
        // If an animator component exists, get it and assign it to the chosen variable
        if(GetComponentInChildren<Animator>())
        {
            ani = GetComponentInChildren<Animator>();
            ani.Play("idle");
        }
        enemy = GetComponent<NavMeshAgent>();
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    protected virtual void Update()
    {
        // Constant check to see how far away the player is. Probably could be replaced by a Raycast
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceFromPlayer < 25.0f)
        {
            pursuit();
        }
    }

    // Follow the Player (and ram into them)
    protected virtual void pursuit()
    {
        enemy.SetDestination(player.transform.position);
    }

    // Handles collisions and if the player can kill them or not, also prevents multiple collisions at once nuking the player's score
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            rb.AddForce(collision.GetContact(0).normal * 6.0f, ForceMode.Impulse);
            playerScore.subtractScore(scoreDamage);
        }
    }

    public void takeDamage(Collision col)
    {
        if(HP > 1)
        {
            HP--;
            rb.AddForce(col.GetContact(0).normal * 6.0f, ForceMode.Impulse);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
