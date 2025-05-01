using UnityEngine;
using UnityEngine.SceneManagement;

public class warpZone : MonoBehaviour
{
    // Variables for logic
    public bool staysInScene = true;
    public Vector3 currentWorldDestination;
    public static Vector3 returnSpot;
    // reference to hold the player so we do stuff when it hits triggers
    private GameObject thePlayer;

    private void Start()
    {
        if (thePlayer == null)
        {
           thePlayer = GameObject.FindWithTag("player");
        }
    }

    // If the player hits the trigger it'll teleport the player to given location or another scene.
    void OnTriggerEnter(Collider onHit)
    {
        if (onHit.tag == "player")
        {
            if (returnSpot == null)
            {
                returnSpot = this.transform.position;
            }
            thePlayer = onHit.gameObject;
            returnSpot = this.transform.position;
            if (staysInScene)
            {
                //sets player location to desired area
                thePlayer.transform.position = currentWorldDestination;
                thePlayer.GetComponent<playerMovement>().haltPlayer();
            }
            else
            {
                //Load another scene
                if(SceneManager.GetActiveScene().buildIndex == 1)
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    SceneManager.LoadScene(1);
                }

            }
        }
    }
}
