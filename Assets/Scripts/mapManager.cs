using UnityEngine;
using UnityEngine.SceneManagement;

public class mapManager : MonoBehaviour
{
    public void loadLevel(string name)
    {
       SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
