using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public bool CreditsMenuIsOn = false;
    public GameObject MainMenuUI;
    public GameObject CreditsMenuUI;
    public GameObject mistralImage;
    int time = 0;
    
    void FixedUpdate () {

        CreditsMenuIsOn = false;
    }

    public void NewGame()
    {
        MainMenuUI.SetActive(false);
        SceneManager.LoadScene("Intro");
    }

    public void Play()
    {
        MainMenuUI.SetActive(false);
        SceneManager.LoadScene("Level1");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        CreditsMenuIsOn = true;
        MainMenuUI.SetActive(false);
        CreditsMenuUI.SetActive(true);
    }
}
