using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class basicEnemyAI : MonoBehaviour
{
    public GameObject player;
    protected double distanceFromPlayer;
    protected NavMeshAgent enemy;
    protected collectibleData playerScore = new collectibleData();
    public bool disableAI = false;

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
        if (distanceFromPlayer < 25.0f && !disableAI)
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
        if (collision.gameObject.tag == "player" && HP > 0)
        {
            Vector3 recoilDirection = collision.collider.gameObject.transform.position - this.gameObject.transform.position;

            collision.gameObject.GetComponent<playerMovement>().haltPlayer();
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-recoilDirection * 10.0f, ForceMode.VelocityChange);
            playerScore.subtractScore(scoreDamage);
        }
    }

    public void takeDamage(Collider col)
    {
        //getting the vector3 between the attack and the enemy, so that we can send them flying
        Vector3 flyDirection = col.gameObject.transform.position - this.gameObject.transform.position;
        //figure out a way calculate the normal, limit it to away from Glumbo in local Space (aka in relation to where glumbo is)
        flyDirection.Normalize();
        if(HP > 1)
        {
            HP--;
            rb.AddForce(-flyDirection * 20.0f, ForceMode.VelocityChange);
        }
        else
        {
            rb.AddForce(-flyDirection * 50.0f, ForceMode.VelocityChange);
            this.GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, 1.0f);
        }
    }
}
