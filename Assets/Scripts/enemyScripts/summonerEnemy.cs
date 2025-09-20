using UnityEngine;
using UnityEngine.AI;

public class summonerEnemy : basicEnemyAI
{
    public GameObject summon;
    public Transform summonOrigin;
    private float wait = 0;
    private bool canSpawn = false;
    public int Stock;
    public float spawnInterval;
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
        if(spawnInterval == 0)
        {
            spawnInterval = 5;
        }
        if(Stock == 0)
        {
            Stock = 5;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!disableAI)
        {
            //spawns a guy on an interval
            wait += Time.deltaTime;
            if(wait > spawnInterval && Stock != 0 && canSpawn && HP != 0)
            {
                spawn();
            }
        }
    }

    // Method for when the Projectile is fired.
    void spawn()
    {
        GameObject g = Instantiate(summon, summonOrigin.position, summonOrigin.rotation);
        Stock--;
        wait = 0;
    }

    public override void takeDamage(Collider col)
    {
        //getting the vector3 between the attack and the enemy, so that we can send them flying
        Vector3 flyDirection = col.gameObject.transform.position - this.gameObject.transform.position;
        flyDirection.Normalize();
        if (HP > 1)
        {
            HP--;
        }
        else
        {
            // Play Death Animation
            rb.linearDamping = 0;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(-flyDirection * 50.0f, ForceMode.Impulse);
            rb.AddTorque(Vector3.forward * 10.0f, ForceMode.Impulse);
            this.GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, 1.0f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            // play Animation/Indicatior to show the spawner is Active
            canSpawn = true;
        }
    }
}
