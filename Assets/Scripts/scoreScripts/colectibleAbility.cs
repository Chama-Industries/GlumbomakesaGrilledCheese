using UnityEngine;
using UnityEngine.UIElements;

public class colectibleAbility : MonoBehaviour
{
    // Variables that can modify player stats
    public float speedMult = 1;
    public float jumpMult = 1;

    // Whatever this is attached to will rotate.
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0.01f, 0.25f, 0.01f, Space.Self);
    }

    // Sets the Player's ability to whatever this script is attached to, uses setActive to prevent "Object Doesn't Exist" errors
    void OnTriggerEnter(Collider onHit)
    {
        if (onHit.tag == "player")
        {
            onHit.gameObject.GetComponent<playerMovement>().setPlayerAbility(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
