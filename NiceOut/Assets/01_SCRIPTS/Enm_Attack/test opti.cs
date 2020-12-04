using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

public class testopti : MonoBehaviour
{
    private Stopwatch stopwatch;
    private Collider[] Target;

    //Stopwatch.elapsed;

    void Update()
    {
        for (int i = 0; i < 10000000; i++)
        {
            if (i % 2 != 0)
            {
                UnityEngine.Debug.Log("idiot");

            }
            else
            {
                UnityEngine.Debug.Log("return");
                return;
            }
        }

        UnityEngine.Debug.Log("RunTime " + "//elapsedtime");
    }
}