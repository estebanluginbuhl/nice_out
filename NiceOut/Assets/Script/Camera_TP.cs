using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_TP : MonoBehaviour
{
    Transform turningPoint;
    float h, v, scroll;
    public Vector2 vMinMax;
    public float sensitivity, distance, height, turnSmooth;

    void Start()
    {
        turningPoint = GameObject.Find("Target").transform; // Attention à appeller cet objet Target

        if(Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        h += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        v -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        v = Mathf.Clamp(v, vMinMax.x, vMinMax.y);// bloque rotation haut bas

        scroll = Input.GetAxis("Mouse ScrollWheel");

        distance += scroll;

        Vector3 desiredRotation = new Vector3(v, h, 0);

        transform.rotation = Quaternion.Euler(desiredRotation);

        transform.position = turningPoint.transform.position - transform.forward * distance + transform.up * height;
    }
}
