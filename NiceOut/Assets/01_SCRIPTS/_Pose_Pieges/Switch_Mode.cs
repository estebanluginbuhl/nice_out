using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Switch_Mode : MonoBehaviour
{
    Inputs inputs;
 
    public bool mode = false; //variable du mode de gameplay : true = posage de piège / false = combat
    public bool pause = false;



    private void Awake()
    {
        inputs = new Inputs();

        inputs.Actions.Switch.started += ctx => SwitchMode();
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
