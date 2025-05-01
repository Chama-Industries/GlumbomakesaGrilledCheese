using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingManager : MonoBehaviour
{
    private collectibleData playerScore = new collectibleData();
    public RawImage endingReaction;
    public Texture2D[] allEndingReactions = new Texture2D[3];
    public TextMeshProUGUI rankText;

    // Starts when GameObject is set to True
    private void OnEnable()
    {
        //only checks the player score once to save on resources (since method only runs once)
        playerScore.getScore();
        // Checks for rank score
        if(playerScore.getScore() > 1000)
        {
            endingReaction.texture = allEndingReactions[0];
            rankText.text = "Glumbo's Judgement:\nGlumbo is Proud of you.";
        }
        else if(playerScore.getScore() > 200)
        {
            endingReaction.texture = allEndingReactions[1];
            rankText.text = "Glumbo's Judgement:\nGlumbo is pleased.";
        }
        else
        {
            endingReaction.texture = allEndingReactions[2];
            rankText.text = "Glumbo's Judgement:\nYou tried.";
        }
    }
}
