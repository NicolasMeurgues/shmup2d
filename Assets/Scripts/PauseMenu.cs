using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //function names are explicit.

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject settingsWindow;


    //Check whether the escape key has been pressed and toggle the pause menu.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }


    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

}
