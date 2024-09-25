using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public bool isAzerty = true;
    public static bool SettingsMenuIsActive = false;
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;
    public Toggle qwerty,azerty;
    public AudioSource music, sfx1, sfx2, sfx3, sfx4, sfx5, sfx6, sfx7, sfx8, sfx9, sfx10;
    public Slider musicSlider,sfxSlider;
    public GameObject azertyPic, qwertyPic;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsMenuIsActive=GameObject.Find("PauseMenu").GetComponent<PauseMenu>().SettingsMenuIsOn;
            if (SettingsMenuIsActive)
            {
                LoadPause();
            }
        }
    }

    public void LoadPause()
    {
        SettingsMenuIsActive = false;
        PauseMenuUI.SetActive(true);
        SettingsMenuUI.SetActive(false);
    }

    public void Azerty()
    {
        if (azerty.isOn == true)
        {
            isAzerty = true;
            qwerty.isOn = false;
            azertyPic.SetActive(true);
            qwertyPic.SetActive(false);
        }
        if (azerty.isOn == false)
        {
            isAzerty = false;
            qwerty.isOn = true;
            azertyPic.SetActive(false);
            qwertyPic.SetActive(true);
        }

    }

    public void Qwerty()
    {
        if (qwerty.isOn == true)
        {
            isAzerty = false;
            azerty.isOn = false;
            azertyPic.SetActive(false);
            qwertyPic.SetActive(true);
        }
        if (qwerty.isOn == false)
        {
            isAzerty = true;
            azerty.isOn = true;
            azertyPic.SetActive(true);
            qwertyPic.SetActive(false);
        }

    }

    public void Music()
    {
        music.volume = musicSlider.value;
    }

    public void Sfx()
    {
        sfx1.volume = sfxSlider.value/2;
        sfx2.volume = sfxSlider.value/2;
        sfx3.volume = sfxSlider.value/2;
        sfx4.volume = sfxSlider.value/3;
        sfx5.volume = sfxSlider.value/5;
        sfx6.volume = sfxSlider.value/2;
        sfx7.volume = sfxSlider.value/5;
        sfx8.volume = sfxSlider.value/10;
        sfx9.volume = sfxSlider.value/5;
        sfx10.volume = sfxSlider.value/2;
    }
}
