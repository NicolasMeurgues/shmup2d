using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public GameObject gameOverUI;


    public static GameOverMenu instance;

    //Check whether there is more than one instance of GameOverMenu, returns an error and kills the game.
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de GameOverMenu dans la scène");
            return;
        }
        instance = this;
    }

    //Activate on player death
    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
    }

    //Quite explicit, actually
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Kill the game the proper way.
    public void QuitButton()
    {
        Application.Quit();
    }

}
