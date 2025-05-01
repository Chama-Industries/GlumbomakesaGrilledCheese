using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public Slider glumboMeter;
    private collectibleData playerScore = new collectibleData();
    public TextMeshProUGUI theScore;
    public TextMeshProUGUI endingScore;
    //Variables used to check the state of the player's score in order to have the HUD react properly
    private double scoreCheck;
    // Currently Temporary Public variables to manage the reactions
    public RawImage currentReaction;
    public Texture2D[] allReactions = new Texture2D[4];
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
            // Check to see if score went up/down and flips the relevant boolean
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
            theScore.text = playerScore.getScore()/100 + "$";
            scoreCheck = playerScore.getScore();
            glumboMeter.value = (float)playerScore.getScore()/100;
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                endingScore.text = "Score: " + playerScore.getScore()/100 + "$";
            }
        }
    }
    private void  positiveGlumboReaction()
    {
        firstImage = 2;
        secondImage = 3;
        resetAt = counter + 200;
    }

    private void negativeGlumboReaction()
    {
        firstImage = 4;
        secondImage = 5;
        resetAt = counter + 200;
    }
}
