using UnityEngine;

public class boostPad : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            other.gameObject.GetComponent<playerMovement>().boostSpeed();
        }
    }
}
