using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ThirdPersonControler : MonoBehaviour
{
    Inputs inputs;
    Vector2 move;
    public float boostSpeed, baseSpeed, dashSpeed, turnSmooth, acceleration;
    Transform cam;
    CharacterController charaCtrl;
    public Animator PlayerAnimator;
    bool isRolling, roll, canWalk;
    public float cdRoll;
    public float gravity;
    Vector3 moveDir;
    Vector3 rotation;
    public LayerMask floor;
    //float _rotationSpeed = 8f;

    private void Awake()
    {
        inputs = new Inputs();

        inputs.Actions.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputs.Actions.Move.canceled += ctx => move = Vector2.zero;
        inputs.Actions.Jump.started += ctx => roll = true;
        inputs.Actions.Jump.canceled += ctx => roll = false;
    }
    private void Start()
    {
        cam = Camera.main.transform;
        charaCtrl = GetComponent<CharacterController>();
        canWalk = true;
    }
    void Update()
    {
        if (GetComponent<Switch_Mode>().GetPause() == false)
        {
            float targetRotation = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg + cam.eulerAngles.y;

            Vector3 rotation = new Vector3(transform.rotation.x, cam.eulerAngles.y, transform.rotation.z); //Vecteur de rotation

            if (canWalk)
            {
                transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(rotation), turnSmooth); //Rotation
                charaCtrl.Move(moveDir.normalized * (baseSpeed + boostSpeed) * Time.deltaTime);//Mouvement
            }

            if (move != Vector2.zero)
            {
                PlayerAnimator.SetBool("Forward", true);
                moveDir = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward + Vector3.down * gravity * Time.deltaTime;
            }
            else
            {
                PlayerAnimator.SetBool("Forward", false);
                moveDir = Vector3.down * gravity * Time.deltaTime;
            }
            /*
            if (isRolling == false)
            {
                StopCoroutine(Roll());
                StopCoroutine(IsRolling());
                if (roll)
                {
                    roll = false;
                    StartCoroutine(Roll());
                    StartCoroutine(IsRolling());
                }
            }*/
        }
    }

    IEnumerator SpeedBoost(float _boostValue, float _boostTime)
    {
        boostSpeed = _boostValue;
        yield return new WaitForSecondsRealtime(_boostTime);
        boostSpeed = 0;

    }
    //roulade
    public IEnumerator Roll()
    {
        isRolling = true;

        Debug.Log("benis");
        PlayerAnimator.SetTrigger("Roulade");

        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg + cam.eulerAngles.y, 0);
        charaCtrl.Move(moveDir.normalized * dashSpeed * Time.deltaTime);

        yield return new WaitForSecondsRealtime(cdRoll);
        isRolling = false;
    }
    public IEnumerator IsRolling()
    {
        canWalk = false;
        yield return new WaitForSecondsRealtime(0.6f);
        canWalk =true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Boost"))
        {
            Speed_Boost boostComponent = other.GetComponent<Speed_Boost>();
            StartCoroutine(SpeedBoost(boostComponent.boostValue, boostComponent.boostTime));
            boostComponent.StartRespawn();
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
