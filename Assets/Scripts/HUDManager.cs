using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class HUDManager : MonoBehaviour
{
    // UI Elements to be assigned manually. Could just get them on Start of Scene using tags, but no time rn
    public Slider glumboMeter;
    private collectibleData playerScore = new collectibleData();
    public TextMeshProUGUI theScore;
    //Variables used to check the state of the player's score in order to have the HUD react properly
    private double scoreCheck = 0;
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
    // Boolean to keep the coroutine from constantly firing
    private bool cycleImages = true;
    public Sprite[] meterIcons = new Sprite[3];
    private Image currentMeterImage;

    // Holders for getting objects that aren't defined in the Inspector
    private Slider[] sliderHolder;
    private TextMeshProUGUI[] textHolder;
    private RawImage[] imageHolder;

    private void Start()
    {
        allReactions[0] = idle;
        allReactions[1] = happy;
        allReactions[2] = unhappy;

        // Ensures that we get the correct items without having to manually assign them in the Inspector. Or in case we forgot to assign them in the inspector.
        if(glumboMeter == null)
        {
            sliderHolder = FindObjectsByType<Slider>(FindObjectsSortMode.None);
            foreach(var slider in sliderHolder)
            {
                if(slider.name == "glumbometer")
                {
                    glumboMeter = slider;
                }
            }
        }
        if(theScore == null)
        {
            textHolder = FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);
            foreach(var text in textHolder)
            {
                if(text.name == "scoreDisplay")
                {
                    theScore = text;
                }
            }
        }
        if(currentReaction == null)
        {
            imageHolder = FindObjectsByType<RawImage>(FindObjectsSortMode.None);
            foreach (var image in imageHolder)
            {
                if(image.name == "glumboReactions")
                {
                    currentReaction = image;
                }
            }
        }

        currentMeterImage = glumboMeter.GetComponentsInChildren<Image>()[2];
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
        // updates the score anytime the stored value (in this script) doesn't match the player's score (stored NOT in this script)
        if (scoreCheck != playerScore.getScore())
        {
            theScore.text = playerScore.formatScore();
            glumboMeter.value += (float)(playerScore.getScore() - scoreCheck);
            Debug.Log(glumboMeter.value);
            playerScore.changeScoreMult(glumboMeter.value);
            scoreCheck = playerScore.getScore();
        }
        if(glumboMeter.normalizedValue > 0.5)
        {
            currentMeterImage.sprite = meterIcons[0];
        }
        else if(glumboMeter.normalizedValue < -0.5)
        {
            currentMeterImage.sprite = meterIcons[2];
        }
        else
        {
            currentMeterImage.sprite = meterIcons[1];
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
