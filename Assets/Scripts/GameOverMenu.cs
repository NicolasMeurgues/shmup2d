using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public GameObject gameOverUI;


    public static GameOverMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de GameOverMenu dans la scène");
            return;
        }
        instance = this;
    }

    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
    }


    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
