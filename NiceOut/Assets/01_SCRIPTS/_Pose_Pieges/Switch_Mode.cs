using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Switch_Mode : MonoBehaviour
{
    Inputs inputs;
    bool test;
    public bool mode = false; //variable du mode de gameplay : true = posage de piège / false = combat

    private void Awake()
    {
        inputs = new Inputs();

        inputs.Actions.Switch.started += ctx => SwitchMode();
    }

    public bool SwitchMode()// passage du mode combat au mode pose de piège
    {
        return mode = !mode;
    }
    public bool GetMode()
    {
        return mode;
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
