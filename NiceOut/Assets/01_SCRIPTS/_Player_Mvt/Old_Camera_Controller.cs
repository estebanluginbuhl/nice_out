using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Old_Camera_Controller : MonoBehaviour
{
    Inputs inputs;
    public Transform turningPoint;
    public GameObject player;
    public LayerMask floor;
    public LayerMask worldObject;
    //movement
    float h, v;
    Vector2 moveCam;
    Vector2 deathMoveCam;
    Vector2 scroll;
    public float scrollSpeed, deathScroll, followSpeed;
    public Vector2 vMinMax;
    public Vector2 distanceMinMax;
    Vector3 desiredRotation;
    Vector3 cameraDirection;

    public float sensitivity, distance, thirdPersonDistance, height, turnSmooth;
    public float collisionRadius;

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
    private void Start()
    {
        cameraDirection = transform.localPosition.normalized;
        thirdPersonDistance = distanceMinMax.y;
    }
    void LateUpdate()
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
                MoveThirdPerson();
            }
            else
            {
                MoveTopDown();
            }
        }
    }


    void MoveThirdPerson()
    {
        h += moveCam.x * Time.deltaTime;
        v += moveCam.y * Time.deltaTime;

        v = Mathf.Clamp(v, vMinMax.x, vMinMax.y);// bloque rotation haut bas

        if (moveCam.x != 0 || moveCam.y != 0)
        {
            transform.rotation = Quaternion.Euler(desiredRotation);
        }
        desiredRotation = new Vector3(-v, h, 0) * sensitivity;

        CollisionCamera();

       // transform.position = Vector3.SlerpUnclamped(transform.position, turningPoint.transform.position - transform.forward * adjustDistance + transform.up * height, followSpeed);
    }


    public void CollisionCamera()
    {
        Ray ray = new Ray(turningPoint.position - (Vector3.down * -1), transform.position);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Vector3.Distance(turningPoint.position - (Vector3.down * -1), turningPoint.position), floor | worldObject))
        {
            Debug.Log("penis");
            thirdPersonDistance = Mathf.Clamp(hit.distance * 0.9f, distanceMinMax.x, distanceMinMax.y);
        }
        else
        {
            thirdPersonDistance = distanceMinMax.y;
        }
        transform.position = Vector3.SlerpUnclamped(transform.position, turningPoint.transform.position - transform.forward * thirdPersonDistance, followSpeed);
    }

    void MoveTopDown()
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

    private void OnEnable()
    {
        inputs.Actions.Enable();
    }
    private void OnDisable()
    {
        inputs.Actions.Disable();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Ray(turningPoint.position - (Vector3.down * -1), transform.position));
    }
}
