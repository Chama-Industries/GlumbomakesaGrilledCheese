using UnityEngine;

public class collectibleItem : MonoBehaviour
{
    // Score Value
    public int itemValue = 0;
    // reference to a class
    private collectibleData playerScore = new collectibleData();

    // Makes whatever this is attached to rotate
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0f, 1.5f, 0f, Space.Self);
    }

    // Adds it's assigned value to the player's score.
    void OnTriggerEnter(Collider onHit)
    {
        if (onHit.tag == "player")
        {
            playerScore.addScore(itemValue);
            Destroy(this.gameObject);
        }
    }

}
