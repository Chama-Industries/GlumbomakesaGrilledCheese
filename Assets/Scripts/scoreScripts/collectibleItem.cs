using UnityEngine;

public class collectibleItem : MonoBehaviour
{
    // Score Value
    public int itemValue = 0;
    // Glumbometer Value (separate from item value)
    public double meterInfluence = 0;
    // This class handles the score
    private collectibleData playerScore = new collectibleData();

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
