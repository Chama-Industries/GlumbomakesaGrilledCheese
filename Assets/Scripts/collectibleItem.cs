using UnityEngine;

public class collectibleItem : MonoBehaviour
{
    public int itemValue = 0;
    private collectibleData playerScore = new collectibleData();
    void OnTriggerEnter(Collider onHit)
    {
        if (onHit.tag == "player")
        {
            playerScore.addScore(itemValue);
            Destroy(this.gameObject);
        }
    }

}
