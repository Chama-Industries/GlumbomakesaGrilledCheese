using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScreenManager : MonoBehaviour
{
    private collectibleData playerScore = new collectibleData();
    public void startGame()
    {
        //Load Scene
        SceneManager.LoadScene("glumboGrilledCheeseLevel1", LoadSceneMode.Single);
    }

    public void returnToMainMenu()
    {
        playerScore.resetScore();
        //Load Scene
        SceneManager.LoadScene("titleScreen", LoadSceneMode.Single);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
