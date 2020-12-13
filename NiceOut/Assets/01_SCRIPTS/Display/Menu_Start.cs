using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Start : MonoBehaviour
{
    public string gameScene;
    public GameObject ui_Credits, ui_Controls, ui_StartPanel;
    bool creditsOpen, controlsOpen;

    void Start()
    {
        ui_Credits.SetActive(false);
        ui_Controls.SetActive(false);
    }
    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(gameScene);
    }    
    public void OpenCloseCredits()
    {
        if (creditsOpen)
        {
            ui_Credits.SetActive(false);
            ui_StartPanel.SetActive(true);
            creditsOpen = false;
        }
        else
        {
            ui_Credits.SetActive(true);
            ui_StartPanel.SetActive(false);
            creditsOpen = true;
        }
    }    
    public void OpenCloseControlsPanel()
    {
        if (controlsOpen)
        {
            ui_Controls.SetActive(false);
            ui_StartPanel.SetActive(true);
            controlsOpen = false;
        }
        else
        {
            ui_Controls.SetActive(true);
            ui_StartPanel.SetActive(false);
            controlsOpen = true;
        }
    }    
    public void Quit()
    {
        Application.Quit();
    }
}
