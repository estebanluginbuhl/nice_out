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
    float h, v, scroll;
    Vector2 moveCam;
    Vector2 deathMoveCam;
    Vector2 scroll2;
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

        //gamepad scroll
        inputs.Actions.Scroll.performed += ctx => scroll = ctx.ReadValue<float>();
        inputs.Actions.Scroll.canceled += ctx => scroll = 0;
        //mouse scroll
        inputs.Actions.MouseScroll.performed += ctx => scroll2 = ctx.ReadValue<Vector2>();
        inputs.Actions.MouseScroll.canceled += ctx => scroll2 = Vector2.zero;
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

                //Camera suis le joueur
                if (scroll != 0)
                {
                    distance -= scroll * scrollSpeed * Time.deltaTime;
                }
                if (scroll2.y < 0)
                {
                    distance -= 1 * scrollSpeed * Time.deltaTime;
                }
                if (scroll2.y > 0)
                {
                    distance += 1 * scrollSpeed * Time.deltaTime;
                }

                transform.position = Vector3.SlerpUnclamped(transform.position, turningPoint.transform.position - transform.forward * distance + transform.up * height, followSpeed);
            }
            else
            {
                if(distance > 1)
                {
                    h += deathMoveCam.x * Time.deltaTime * sensitivity * distance;
                    v += deathMoveCam.y * Time.deltaTime * sensitivity * distance;
                }
                else
                {
                    h += deathMoveCam.x * Time.deltaTime * sensitivity;
                    v += deathMoveCam.y * Time.deltaTime * sensitivity;
                }

                if (scroll != 0)
                {
                    distance -= scroll * deathScroll * Time.deltaTime;
                }
                if (scroll2.y < 0)
                {
                    distance -= 1 * deathScroll * Time.deltaTime;
                }
                if (scroll2.y > 0)
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
