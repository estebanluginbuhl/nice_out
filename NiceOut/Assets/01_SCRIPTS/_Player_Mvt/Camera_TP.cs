using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_TP : MonoBehaviour
{
    Inputs inputs;
    public Transform turningPoint;
    public GameObject player;

    //movement
    float h, v;
    Vector2 moveCam;
    Vector2 deathMoveCam;
    Vector2 scroll;
    public float scrollSpeed, deathScroll, followSpeed;
    public Vector2 vMinMax;
    Vector3 desiredRotation;

    public float sensitivity, distance, height, turnSmooth;

    private void Awake()
    {
        inputs = new Inputs();
        //move in tps
        inputs.Actions.CameraMove.performed += ctx => moveCam = ctx.ReadValue<Vector2>();
        inputs.Actions.CameraMove.canceled += ctx => moveCam = Vector2.zero;
        //move in TopDown
        inputs.Actions.Move.performed += ctx => deathMoveCam = ctx.ReadValue<Vector2>();
        inputs.Actions.Move.canceled += ctx => deathMoveCam = Vector2.zero;

        inputs.Actions.MouseScroll.performed += ctx => scroll = ctx.ReadValue<Vector2>();
        inputs.Actions.MouseScroll.canceled += ctx => scroll = Vector2.zero;
    }

    void Update()
    {
        if (player.GetComponent<Switch_Mode>().GetPause() == false)
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (player.GetComponent<Switch_Mode>().mort == false)
            {
                h += moveCam.x * Time.deltaTime;
                v += moveCam.y * Time.deltaTime;

                v = Mathf.Clamp(v, vMinMax.x, vMinMax.y);// bloque rotation haut bas

                if (moveCam.x != 0 || moveCam.y != 0)
                {
                    transform.rotation = Quaternion.Euler(desiredRotation);
                }
                desiredRotation = new Vector3(-v, h, 0) * sensitivity;

                transform.position = Vector3.SlerpUnclamped(transform.position, turningPoint.transform.position - transform.forward * distance + transform.up * height, followSpeed);
            }
            else
            {
                if (distance > 1)
                {
                    h += deathMoveCam.x * Time.deltaTime * sensitivity * distance;
                    v += deathMoveCam.y * Time.deltaTime * sensitivity * distance;
                }
                else
                {
                    h += deathMoveCam.x * Time.deltaTime * sensitivity;
                    v += deathMoveCam.y * Time.deltaTime * sensitivity;
                }


                if (scroll.y < 0)
                {
                    distance -= 1 * deathScroll * Time.deltaTime;
                }
                if (scroll.y > 0)
                {
                    distance += 1 * deathScroll * Time.deltaTime;
                }
                transform.position = new Vector3(h, distance + 25, v);
                transform.rotation = Quaternion.Euler(Vector3.right * 90);
            }
        }

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
