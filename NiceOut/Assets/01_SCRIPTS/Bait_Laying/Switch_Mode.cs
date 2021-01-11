using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class Switch_Mode : MonoBehaviour
{
    PapogayInputs modeInputs;
    
    public string menuScene;
    [HideInInspector]
    public bool mode = false; //variable du mode de gameplay : true = posage de piège / false = combat
    //[HideInInspector]
    public bool pause = false;
    [HideInInspector]
    public bool realPause = false;
    [HideInInspector]
    public bool isShopping = false;
    //[HideInInspector]
    public bool mort = false;
    [HideInInspector]
    public float cptMort;

    [Header("UI Elements")]
    public GameObject ui_DeathPanel;
    public GameObject ui_PausePanel;

    private void Awake()
    {
        modeInputs = new PapogayInputs();

        modeInputs.Actions.Switch.started += ctx => SwitchMode();
        modeInputs.Actions.Echap.started += ctx => SetPause();
        ui_DeathPanel.SetActive(false);
        ui_PausePanel.SetActive(false);
    }

    public void SwitchMode()// passage du mode combat au mode pose de piège
    {
        if(pause == false)
        {
            mode = !mode;
        }
        else
        {
            return;
        }

    }

    public bool GetMode()
    {
        return mode;
    }

    public bool GetMort()
    {
        return mort;
    }

    public void SetPause()
    {
        if (isShopping == false)
        {
            pause = !pause;
            if (pause == true && ui_PausePanel.activeInHierarchy == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ui_PausePanel.SetActive(true);
                realPause = true;
                Time.timeScale = 0;
            }
            if (pause == false && ui_PausePanel.activeInHierarchy == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ui_PausePanel.SetActive(false);
                realPause = false;
                Time.timeScale = 1;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void SetShopping()
    {
        pause = !pause;
        if (pause == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isShopping = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isShopping = false;
        }
    }

    public bool GetPause()
    {
        return pause;
    }

    private void OnEnable()
    {
        modeInputs.Actions.Enable();
    }

    private void OnDisable()
    {
        modeInputs.Actions.Disable();
    }
    
    public void Restart()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name.ToString());
    }
    public void Resume()
    {
        pause = false;
        realPause = false;
        Time.timeScale = 1;
        ui_PausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Quitter()
    {
        Debug.Log("Quitter");
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
