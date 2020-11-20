using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_TurnOver_Diorama : MonoBehaviour
{
    public Transform camTurningPoint;
    public float camSpeed;

    void Update()
    {
        Vector3 camRotation = camTurningPoint.transform.eulerAngles;

        camRotation.y += camSpeed * Time.deltaTime;

        camTurningPoint.transform.eulerAngles = camRotation;
    }
}
