using UnityEngine;

public class playerAttackBehavior : MonoBehaviour
{
    void Update()
    {
        Destroy(this.gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        if(other.gameObject.tag == "enemy")
        {
            // calls the hurting
        }
        //This is where enemies get hurt
    }
}
