using UnityEngine;
using UnityEngine.UIElements;

public class colectibleAbility : MonoBehaviour
{
    // Variables that can modify player stats
    public float speedMult = 1;
    public float jumpMult = 1;

    // The Paint Can will rotate
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0.01f, 0.25f, 0.01f, Space.Self);
    }

    void OnTriggerEnter(Collider onHit)
    {
        if (onHit.tag == "player")
        {
            onHit.gameObject.GetComponent<playerMovement>().setPlayerAbility(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
