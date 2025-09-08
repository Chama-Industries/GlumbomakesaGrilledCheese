using UnityEngine;

public class playerAttackBehavior : MonoBehaviour
{
    private Collider objectsCollider;
    private void Start()
    {
        objectsCollider = this.GetComponent<Collider>();
    }

    void Update()
    {
        Destroy(this.gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        if(other.gameObject.tag == "enemy")
        {
            if(other.gameObject.GetComponent<basicEnemyAI>() != null)
            {
                other.gameObject.GetComponent<basicEnemyAI>().takeDamage(objectsCollider);
            }
            else if (other.gameObject.GetComponent<rangedEnemy>())
            {
                other.gameObject.GetComponent<rangedEnemy>().takeDamage(objectsCollider);
            }
        }
    }
}
