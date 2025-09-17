using UnityEngine;
using UnityEngine.AI;

public class summonerEnemy : basicEnemyAI
{
    public GameObject summon;
    public Transform summonOrigin;
    private int wait = 0;
    private bool hasFired = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        //ensures that we have the *ONLY* player in the game. Allows for enemies to create other enemies and saves me a headache
        if (player == null)
        {
            player = GameObject.FindWithTag("player");
        }
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();
        // If an animator component exists, get it and assign it to the chosen variable
        if (GetComponentInChildren<Animator>())
        {
            ani = GetComponentInChildren<Animator>();
            ani.Play("idle");
        }
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!disableAI)
        {
            //spawns a guy on an interval
        }
    }

    // Cooldown to prevent spam
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

    // Method for when the Projectile is fired.
    void shoot()
    {
        GameObject g = Instantiate(summon, summonOrigin.position, summonOrigin.rotation);
        hasFired = true;
    }
}
