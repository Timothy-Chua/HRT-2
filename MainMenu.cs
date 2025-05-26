using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;
    public void Game()
    {
        SceneManager.LoadSceneAsync("LRT");
    }
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void closeCredit()
    {
        creditsPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }


}
