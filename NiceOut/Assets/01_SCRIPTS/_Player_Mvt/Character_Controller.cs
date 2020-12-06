using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Character_Controller : MonoBehaviour
{
    Inputs inputs;

    Transform cam;
    CharacterController charaCtrl;
    CapsuleCollider charaColl;
    StatsPlayer playerStats;
    public Animator PlayerAnimator;
    public LayerMask floor;

    [Header("Movement")]
    public float baseSpeed;
    public float boostSpeed;
    public float turnSmooth;
    public float gravity;

    Vector2 move;//input move
    Vector3 moveDir;
    Vector3 rotation;
    //float _rotationSpeed = 8f;

    [Header("Dash")]
    public float dashSpeed;
    bool roll; //input roll
    bool isRolling = false;
    public float invincibleDuration = 0.2f;
    float invincibleCount;
    public float cdRoll; // temps entre roulade;
    float cdCount; // compteur temps entre roulade;
    [SerializeField]
    ParticleSystem isBoosted;

    private void Awake()
    {
        inputs = new Inputs();

        inputs.Actions.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputs.Actions.Move.canceled += ctx => move = Vector2.zero;
        inputs.Actions.Jump.started += ctx => roll = true;
        inputs.Actions.Jump.canceled += ctx => roll = false;

        isBoosted.Stop();
    }
    private void Start()
    {
        cam = Camera.main.transform;
        charaCtrl = GetComponent<CharacterController>();
        charaColl = GetComponent<CapsuleCollider>();
        playerStats = GetComponent<StatsPlayer>();
    }
    void Update()
    {
        if (GetComponent<Switch_Mode>().GetPause() == false)
        {
            if (GetComponent<Switch_Mode>().mort == false)
            {
                float targetRotation = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg + cam.eulerAngles.y;

                Vector3 rotation = new Vector3(transform.rotation.x, cam.eulerAngles.y, transform.rotation.z); //Vecteur de rotation

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
                
                if (isRolling == false)
                {
                    transform.rotation = Quaternion.Euler(rotation);
                    charaCtrl.Move(moveDir.normalized * (baseSpeed + boostSpeed));//Mouvement
                }


                //Input roulade
                if (cdCount > 0)
                {
                    cdCount -= Time.deltaTime;
                }
                else
                {
                    if (roll)
                    {
                        roll = false;
                        Roll();
                    }
                }

                //Mouvement et anim Roulade
                if (invincibleCount > 0)
                {
                    transform.rotation = Quaternion.Euler(0, Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg + cam.eulerAngles.y, 0);
                    charaCtrl.Move(moveDir.normalized * (dashSpeed + boostSpeed));
                    invincibleCount -= Time.deltaTime;
                }
                else
                {
                    if (isRolling == true)
                    {
                        charaColl.enabled = true;
                        PlayerAnimator.ResetTrigger("Roulade");
                        isRolling = false;
                    }
                }
            }
        }
    }

    IEnumerator SpeedBoost(float _boostValue, float _boostTime)
    {
        boostSpeed = _boostValue;
        isBoosted.Play();
        yield return new WaitForSecondsRealtime(_boostTime);
        isBoosted.Stop();
        boostSpeed = 0;

    }
    //roulade
    public void Roll()
    {
        isRolling = true;

        PlayerAnimator.SetTrigger("Roulade");
        invincibleCount = invincibleDuration;
        cdCount = cdRoll;
        charaColl.enabled = false;
    }

    public void UpgradeSpeed(float _moreSpeed)
    {
        baseSpeed = baseSpeed * (1 + (_moreSpeed/100));
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
