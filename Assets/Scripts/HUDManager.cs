using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Slider glumboMeter;
    private collectibleData playerScore = new collectibleData();
    public TextMeshProUGUI theScore;
    //Variables used to check the state of the player's score in order to have the HUD react properly
    private int scoreCheck;
    private bool scoreIncreased = false;
    private bool scoreDecreased = false;

    private void FixedUpdate()
    {
        updateScore();
        if(scoreIncreased)
        {
            //change Glumbo Image
            scoreIncreased = false;
        }
        if (scoreDecreased)
        {
            //change Glumbo Image
            scoreDecreased = false;
        }
    }

    void updateScore()
    {
        if (scoreCheck != playerScore.getScore())
        {
            // Check to see if score went up/down and flips the relevant boolean
            if(playerScore.getScore() - scoreCheck > 0)
            {
                scoreIncreased = true;
            }
            else
            {
                scoreDecreased = true;
            }
                theScore.text = " " + playerScore.getScore() + ".00$";
            scoreCheck = playerScore.getScore();
            glumboMeter.value = playerScore.getScore();
        }
    }
}
