 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public bool SettingsMenuIsOn = false;
    public bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;

    // Update is called once per frame
    void FixedUpdate()
    {
        SettingsMenuIsOn = false;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc");
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        GameIsPaused = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

    }

    public void Resume()
    {
        GameIsPaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadSettings()
    {
        SettingsMenuIsOn = true;
        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(true);
    }

    public void LoadRetry()
    {
        SceneManager.LoadScene("Level1");
    }

}
