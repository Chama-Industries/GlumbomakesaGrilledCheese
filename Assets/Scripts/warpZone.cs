using UnityEngine;
using UnityEngine.SceneManagement;

public class warpZone : MonoBehaviour
{
    public bool staysInScene = true;
    public Vector3 currentWorldDestination;
    private Vector3 returnSpot;


    public void Update()
    {
        
    }

    public void teleportPlayer()
    {
        if(staysInScene)
        {
            //sets player location to desired area
        }
        else
        {
            //Load another scene
        }
    }
}
