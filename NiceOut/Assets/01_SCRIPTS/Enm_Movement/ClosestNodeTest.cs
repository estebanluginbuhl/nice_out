using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClosestNodeTest : MonoBehaviour
{
    Transform[] enmTransform;
    bool checkEnmCount = false;

    public  double timeChecker = 0;

    void Awake()
    {
        CheckForEnm();
        timeChecker = 0;
    }

    void Update()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enmTransform)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        Debug.Log("best target " + bestTarget);
        
        CheckForEnm();

        timeChecker = timeChecker + Time.deltaTime; //supprimer si fonctionne au proto

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
                enmTransform[i] = GameObject.FindObjectOfType<GameObject>().transform;
                i++;
            }
            Debug.Log("target count " + enmTransform.Length); //supprimer si fonctionne au proto
            checkEnmCount = true;
        }
        else return;
    }
}