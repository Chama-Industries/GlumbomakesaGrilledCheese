using UnityEngine;

public class playerAttackBehavior : MonoBehaviour
{
    private Collider objectsCollider;
    private void Start()
    {
        objectsCollider = this.GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(-1, 0, 0));
    }

    private void Awake()
    {
        Destroy(this.gameObject, 0.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            if(other.gameObject.GetComponent<basicEnemyAI>() != null)
            {
                other.gameObject.GetComponent<basicEnemyAI>().takeDamage(objectsCollider);
            }
            else if (other.gameObject.GetComponent<rangedEnemy>() != null)
            {
                other.gameObject.GetComponent<rangedEnemy>().takeDamage(objectsCollider);
            }
            Destroy(this.gameObject);
        }
    }
}
