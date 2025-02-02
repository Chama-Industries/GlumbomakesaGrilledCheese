using UnityEngine;

public class collectibleItem : MonoBehaviour
{
    public int itemValue = 0;
    private collectibleData playerScore = new collectibleData();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
