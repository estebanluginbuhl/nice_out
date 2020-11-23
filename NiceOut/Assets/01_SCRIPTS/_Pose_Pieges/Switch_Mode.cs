using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Switch_Mode : MonoBehaviour
{
    Inputs inputs;
 
    public bool mode = false; //variable du mode de gameplay : true = posage de piège / false = combat
    public bool pause = false;
    public bool mort = false;
    public float cptMort;

    public TextMeshProUGUI ui_deathTimer;
    public GameObject ui_DeathPanel;

    private void Awake()
    {
        inputs = new Inputs();

        inputs.Actions.Switch.started += ctx => SwitchMode();
        ui_DeathPanel.SetActive(false);
    }

    private void Update()
    {

        if (cptMort > 0)
        {
            if (ui_DeathPanel.activeInHierarchy == false)
            {
                ui_DeathPanel.SetActive(true);
            }
            cptMort -= Time.deltaTime;
            ui_deathTimer.text = (Mathf.RoundToInt(cptMort)).ToString();
        }
        if(cptMort <= 0)
        {
            if (mort == true)
            {
                if (ui_DeathPanel.activeInHierarchy)
                {
                    ui_DeathPanel.SetActive(false);
                }
                mort = false;
                GetComponent<StatsPlayer>().Respawn();
            }
        }
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
        pause = !pause;
    }

    public bool GetPause()
    {
        return pause;
    }

    private void OnEnable()
    {
        inputs.Actions.Enable();
    }

    private void OnDisable()
    {
        inputs.Actions.Disable();
    }
}
