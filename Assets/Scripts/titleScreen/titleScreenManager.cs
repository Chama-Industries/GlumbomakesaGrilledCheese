using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScreenManager : MonoBehaviour
{
    public void startGame()
    {
        //Load Scene
        SceneManager.LoadScene("glumboGrilledCheeseLevel1", LoadSceneMode.Single);
    }

    public void returnToMainMenu()
    {
        //Load Scene
        SceneManager.LoadScene("titleScreen", LoadSceneMode.Single);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
