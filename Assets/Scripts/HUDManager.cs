using System.Collections;
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
    // Currently Temporary Public variables to manage the reactions
    public RawImage currentReaction;
    public Texture2D[] allReactions = new Texture2D[4];
    private bool alternateImage = true;
    private int firstImage = 0;
    private int secondImage = 1;

    //temporary counter variables to reset the reaction image to the default ones
    private int counter = 0;
    private int resetAt = 125;

    private void FixedUpdate()
    {
        updateScore();
        // Ideally this would be the only code for alternative images giving a false sense of animation (besides I think it looks cool)
        // Find a way to just swap pointers for what image is currently being displayed
        if(alternateImage && counter % 25 == 0)
        {
            currentReaction.texture = allReactions[firstImage];
            alternateImage = false;
        }
        else if(counter % 40 == 0) 
        {
            currentReaction.texture = allReactions[secondImage];
            alternateImage = true;
        }

        if (scoreIncreased)
        {
            positiveGlumboReaction();
            scoreIncreased = false;
        }
        if (scoreDecreased)
        {
            negativeGlumboReaction();
            scoreDecreased = false;
        }
        counter++;
        if (counter >= resetAt)
        {
            firstImage = 0;
            secondImage = 1;
        }
    }

    void updateScore()
    {
        if (scoreCheck != playerScore.getScore())
        {
            // Check to see if score went up/down and flips the relevant boolean
            if (playerScore.getScore() - scoreCheck > 0 || playerScore.getScore() - scoreCheck < 0)
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
    void positiveGlumboReaction()
    {
        firstImage = 2;
        secondImage = 3;
    }

    void negativeGlumboReaction()
    {

    }
}
