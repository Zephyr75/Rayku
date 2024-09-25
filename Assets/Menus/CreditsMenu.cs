using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour {

    public static bool CreditsMenuIsActive = false;
    public GameObject MainMenuUI;
    public GameObject CreditsMenuUI;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CreditsMenuIsActive=GameObject.Find("Canvas").GetComponent<MainMenu>().CreditsMenuIsOn;
            Debug.Log(CreditsMenuIsActive);
            if (CreditsMenuIsActive)
            {
                LoadMain();
            }
        }
    }

    public void LoadMain()
    {
        CreditsMenuIsActive = false;
        MainMenuUI.SetActive(true);
        CreditsMenuUI.SetActive(false);
    }

}
