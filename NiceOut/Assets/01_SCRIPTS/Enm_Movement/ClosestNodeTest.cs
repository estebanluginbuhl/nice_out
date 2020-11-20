using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClosestNodeTest : MonoBehaviour
{
    Transform[] enmTransform;
    bool checkEnmCount = false;
    public Color gizmosColor;

    public  double timeChecker = 0;

    void Awake()
    {
        CheckForEnm();
        timeChecker = 0;
    }

    void Update()
    {
        CheckForEnm();

        timeChecker = timeChecker + Time.deltaTime;

        if (timeChecker >= 5)
        {
            timeChecker = 0;
            checkEnmCount = false;
        }
        else return;
    }

    void CheckForEnm()
    {
        if (checkEnmCount == false)
        {
            enmTransform = new Transform[transform.childCount];
            for (int i = 0; i < enmTransform.Length; i++)
            {
                enmTransform[i] = FindObjectOfType<GameObject>().transform;
                i++;
            }
            Debug.Log("target count " + enmTransform.Length); //supprimer si fonctionne au proto
            checkEnmCount = true;
        }
        else return;
    }
}