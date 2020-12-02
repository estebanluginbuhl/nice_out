using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class Camera_Controller : MonoBehaviour
{
    Inputs inputs;
    Vector2 moveCam;
    Vector2 scroll;
    
    [SerializeField]
    float verticalSensitivity = 0.5f;

    [SerializeField]
     Switch_Mode switchMode;

    [SerializeField]
    Transform focus = default;
    [SerializeField]
    float distance = 5f;
    [SerializeField]
    float height = 5f;
    [SerializeField, Min(0f)]
    float focusRadius = 1f;
    Vector3 focusPoint, previousFocusPoint;
    [SerializeField, Range(0f, 1f)]
    float focusCentering = 0.5f;
    [SerializeField, Range(1f, 360f)]
    float rotationSpeed = 90f;

    Vector2 orbitAngles = new Vector2(45f, 0f);
    [SerializeField, Range(-89f, 89f)]
    float minVerticalAngle = 5f, maxVerticalAngle = 85f;
    [SerializeField, Min(0f)]
    float lastManualRotationTime;

    Camera regularCamera;

    [SerializeField]
    LayerMask obstructionMask = -1;

    private void Awake()
    {
        inputs = new Inputs();
        //move in tps
        inputs.Actions.CameraMove.performed += ctx => moveCam = ctx.ReadValue<Vector2>();
        inputs.Actions.CameraMove.canceled += ctx => moveCam = Vector2.zero;

        inputs.Actions.MouseScroll.performed += ctx => scroll = ctx.ReadValue<Vector2>();
        inputs.Actions.MouseScroll.canceled += ctx => scroll = Vector2.zero;

        regularCamera = GetComponent<Camera>();
        focusPoint = focus.position;
        transform.localRotation = Quaternion.Euler(orbitAngles);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (switchMode.GetMort() == false)
        {
            if(switchMode.GetPause() == false)
            {
                distance += scroll.y * Time.deltaTime * 0.1f;

                UpdateFocusPoint();
                ManualRotation();
                Quaternion lookRotation;
                if (ManualRotation())
                {
                    ConstrainAngles();
                    lookRotation = Quaternion.Euler(orbitAngles);
                }
                else
                {
                    lookRotation = transform.localRotation;
                }

                Vector3 lookDirection = lookRotation * Vector3.forward;
                Vector3 lookPosition = focusPoint - lookDirection * distance + Vector3.up * height;

                Vector3 rectOffset = lookDirection * regularCamera.nearClipPlane;
                Vector3 rectPosition = lookPosition + rectOffset;
                Vector3 castFrom = focus.position;
                Vector3 castLine = rectPosition - castFrom;
                float castDistance = castLine.magnitude;
                Vector3 castDirection = castLine / castDistance;


                if (Physics.BoxCast(castFrom, CameraHalfExtends, castDirection, out RaycastHit hit, lookRotation, castDistance, obstructionMask))
                {
                    rectPosition = castFrom + castDirection * hit.distance;
                    lookPosition = rectPosition - rectOffset;
                }

                transform.SetPositionAndRotation(lookPosition, lookRotation);
            }
        }

    }

    void UpdateFocusPoint()
    {
        previousFocusPoint = focusPoint;
        Vector3 targetPoint = focus.position;
        if (focusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, focusPoint);
            float t = 1f;
            if (distance > 0.01f && focusCentering > 0f)
            {
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            }
            if (distance > focusRadius)
            {
                t = Mathf.Min(t, focusRadius / distance);
            }
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else
        {
            focusPoint = targetPoint;
        }
    }

    bool ManualRotation()
    {
        const float e = 0.001f;
        if(moveCam.x < -e || moveCam.x > e || moveCam.y < -e || moveCam.y > e)
        {
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * new Vector2(moveCam.y * verticalSensitivity, moveCam.x);
            lastManualRotationTime = Time.unscaledTime;
            return true;
        }
        return false;
    }

    void ConstrainAngles()
    {
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);
        if (orbitAngles.y < 0f)
        {
            orbitAngles.y += 360f;
        }
        else if (orbitAngles.y >= 360f)
        {
            orbitAngles.y -= 360f;
        }
    }

    Vector3 CameraHalfExtends
    {
        get
        {
            Vector3 halfExtends;
            halfExtends.y = regularCamera.nearClipPlane * Mathf.Tan(0.5f * Mathf.Deg2Rad * regularCamera.fieldOfView);
            halfExtends.x = halfExtends.y * regularCamera.aspect;
            halfExtends.z = 0f;
            return halfExtends;
        }
    }

    static float GetAngle(Vector2 direction)
    {
        float angle = Mathf.Acos(direction.x) * Mathf.Rad2Deg;
        return direction.x < 0f ? 360f - angle : angle;
    }

    void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
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
