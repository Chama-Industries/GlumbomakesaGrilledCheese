using UnityEngine;
using UnityEngine.SceneManagement;

public class warpZone : MonoBehaviour
{
    public bool staysInScene = true;
    public Vector3 currentWorldDestination;
    public static Vector3 returnSpot;
    private GameObject thePlayer;

    private void Start()
    {
        if (thePlayer == null)
        {
           thePlayer = GameObject.FindWithTag("player");
        }
    }

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
