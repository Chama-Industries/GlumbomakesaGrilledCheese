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
    public float eAcceleration = 8.0f;
    public float distanceToStopFromPlayer = 0.0f;
    public float recoilMult = 200.0f;

    // Rigidbody
    protected Rigidbody rb;
    // Animator
    protected Animator ani;

    /*
     * TO DO
     * Investigate OnTriggerEnter and Exit instead of constantly calculating the distance between the enemy and the player. [Performance]
     * Clean up Start() [Readability]
     */

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
        }
        enemy = GetComponent<NavMeshAgent>();
        if(enemy != null)
        {
            enemy.acceleration = eAcceleration;
            enemy.stoppingDistance = distanceToStopFromPlayer;
        }
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    protected virtual void Update()
    {
        // Constant check to see how far away the player is. Probably could be replaced by a Raycast
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceFromPlayer < 35.0f && !disableAI)
        {
            if (GetComponentInChildren<Animator>())
            {
                ani.SetBool("pursuit", true);
            }
            pursuit();
        }
        else
        {
            enemy.ResetPath();
            if (GetComponentInChildren<Animator>())
            {
                ani.SetBool("pursuit", false);
            }
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
            recoilDirection = new Vector3(recoilDirection.x, 0.0f, recoilDirection.z);
            recoilDirection.Normalize();

            collision.gameObject.GetComponent<playerMovement>().haltPlayer();
            collision.gameObject.GetComponent<Rigidbody>().AddForce(recoilDirection * recoilMult, ForceMode.Impulse);
            playerScore.subtractScore(scoreDamage);
        }
    }

    public virtual void takeDamage(Collider col)
    {
        //getting the vector3 between the attack and the enemy, so that we can send them flying (also prevents the spontaneous combustion of either Player Score or Enemy's health
        Vector3 flyDirection = col.gameObject.transform.position - this.gameObject.transform.position;
        flyDirection.Normalize();
        if(HP > 1)
        {
            HP--;
            rb.AddForce(-flyDirection * 20.0f, ForceMode.Impulse);
        }
        else
        {
            rb.linearDamping = 0;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(-flyDirection * 50.0f, ForceMode.Impulse);
            rb.AddTorque(Vector3.up * 10.0f, ForceMode.Impulse);
            this.GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, 1.0f);
        }
    }
}
