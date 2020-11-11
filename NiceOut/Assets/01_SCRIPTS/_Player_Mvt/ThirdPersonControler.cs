using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ThirdPersonControler : MonoBehaviour
{
    Inputs inputs;
    Vector2 move;
    public float speed, turnSmooth, acceleration;
    Transform cam;
    CharacterController charaCtrl;

    private void Awake()
    {
        inputs = new Inputs();

        inputs.Actions.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputs.Actions.Move.canceled += ctx => move = Vector2.zero;

    }
    private void Start()
    {
        cam = Camera.main.transform;
        charaCtrl = GetComponent<CharacterController>();
    }
    void Update()
    {
        Vector3 desiredVelocity = new Vector3(move.x, 0, move.y); //Vecteur de mouvement
        Vector3 velocityNormalized = desiredVelocity.normalized; //Normale du Vecteur de mouvement

        if (move != Vector2.zero)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self); //Mvt du perso

            float targetRotationY = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg + cam.eulerAngles.y; //Angle de rotation du perso

            Vector3 rotation = new Vector3(transform.rotation.x, targetRotationY, transform.rotation.z); //Vecteur de rotation

            transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(rotation), turnSmooth); //Rotation
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
