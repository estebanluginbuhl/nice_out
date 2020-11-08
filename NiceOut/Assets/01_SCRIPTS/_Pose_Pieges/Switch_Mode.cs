using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Mode : MonoBehaviour
{
    public bool mode = false; //variable du mode de gameplay : true = posage de piège / false = combat

    private void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            SwitchMode();
        }
    }

    public bool SwitchMode()// passage du mode combat au mode pose de piège
    {
        return mode = !mode;
    }
    public bool GetMode()
    {
        return mode;
    }
}
