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
    public TextMeshProUGUI endingScore;
    //Variables used to check the state of the player's score in order to have the HUD react properly
    private double scoreCheck;
    // Currently Temporary Public variables to manage the reactions
    public RawImage currentReaction;
    public Texture2D[] allReactions = new Texture2D[6];
    // Variables to control what image is shown
    private bool alternateImage = true;
    private int firstImage = 0;
    private int secondImage = 1;

    //temporary counter variables to reset the reaction image to the default ones
    private int counter = 0;
    private int resetAt = -1;

    private void FixedUpdate()
    {
        updateScore();
        // Ideally this would be the only code for alternative images giving a false sense of animation (besides I think it looks cool)
        // Find a way to just swap pointers for what image is currently being displayed
        if (alternateImage)
        {
            currentReaction.texture = allReactions[firstImage];
            alternateImage = false;
        }
        // Slower alternation for other reactions, changes every 10 frames
        else if (counter % 10 == 0 && resetAt != -1)
        {
            currentReaction.texture = allReactions[secondImage];
            if (counter % 20 == 0)
            {
                alternateImage = !alternateImage;
            }
        }
        else if (resetAt == -1)
        {
            currentReaction.texture = allReactions[secondImage];
            alternateImage = true;
        }

        // Ensures default image returns after a reaction happens.
        if (counter == resetAt)
        {
            counter = 0;
            firstImage = 0;
            secondImage = 1;
            resetAt = -1;
        }
        counter++;
    }

    void updateScore()
    {
        if (scoreCheck != playerScore.getScore())
        {
            // Check to see if score went up/down and calls the relevant method
            if (playerScore.getScore() - scoreCheck > 0)
            {
                playerScore.changeScoreMult(0.1);
                positiveGlumboReaction();
            }
            else
            {
                playerScore.changeScoreMult(-0.1);
                negativeGlumboReaction();
            }
            //i fixed it, you're welcome.
            theScore.text = "$" + playerScore.getScore()/100;
            scoreCheck = playerScore.getScore();
            glumboMeter.value = (float)playerScore.getScore();
            // Prevents an assignment error with the HUD in Sub-areas
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                endingScore.text = "Score: $" + playerScore.getScore()/100;
            }
        }
    }
    private void  positiveGlumboReaction()
    {
        firstImage = 2;
        secondImage = 3;
        // Sets a manual timer for about 4 seconds before changing back to the base reactions.
        resetAt = counter + 200;
    }

    private void negativeGlumboReaction()
    {
        firstImage = 4;
        secondImage = 5;
        resetAt = counter + 200;
    }
}
