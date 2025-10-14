using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    // UI Elements to be assigned manually. Could just get them on Start of Scene using tags, but no time rn
    public Slider glumboMeter;
    private collectibleData playerScore = new collectibleData();
    public TextMeshProUGUI theScore;
    //Variables used to check the state of the player's score in order to have the HUD react properly
    private double scoreCheck;
    // Currently Temporary Public variables to manage the reactions
    public RawImage currentReaction;
    // 2D Arrays dont like the Inspector, this is a workaround. Fortunately the Types of Reactions are finite
    public Texture2D[] idle;
    public Texture2D[] happy;
    public Texture2D[] unhappy;
    /*
     * 0 - Idle
     * 1 - Happy
     * 2 - Unhappy
     */
    public Texture2D[][] allReactions = new Texture2D[3][];
    // Pointer for which Type of Reaction we use
    private int reactionType = 0;
    private bool cycleImages = true;

    private void Start()
    {
        allReactions[0] = idle;
        allReactions[1] = happy;
        allReactions[2] = unhappy;
    }

    private void Update()
    {
        updateScore();
        // Boolean stops unity from calling the Coroutine multiple times before it's finished
        if(cycleImages)
        {
            StartCoroutine(animateReactions());
            cycleImages = false;
        }
    }

    void updateScore()
    {
        if (scoreCheck != playerScore.getScore())
        {
            theScore.text = "$" + playerScore.getScore()/100;
            scoreCheck = playerScore.getScore();
            glumboMeter.value = (float)playerScore.getScore();
        }
    }

    IEnumerator animateReactions()
    {
       // Uses Jagged Arrays, as each type of Reaction wont have an equal number of frames/items
       Texture2D[] reaction = allReactions[reactionType];
       for(int j = 0; j <  reaction.Length; j++)
       {
            currentReaction.texture = reaction[j];
            yield return new WaitForSeconds(0.25f);
       }
        cycleImages = true;
    }
}
