using UnityEngine;

public class collectibleItem : MonoBehaviour
{
    public int itemValue = 0;
    private collectibleData playerScore = new collectibleData();

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0f, 1.5f, 0f, Space.Self);
    }

    void OnTriggerEnter(Collider onHit)
    {
        if (onHit.tag == "player")
        {
            playerScore.addScore(itemValue);
            Destroy(this.gameObject);
        }
    }

}
