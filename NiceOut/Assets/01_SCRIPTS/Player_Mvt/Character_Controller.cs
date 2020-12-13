﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Character_Controller : MonoBehaviour
{
    Inputs inputs;

    Transform cam;
    CharacterController charaCtrl;
    CapsuleCollider charaColl;
    Player_Stats playerStats;
    public Animator PlayerAnimator;
    public LayerMask floor;

    [Header("Movement")]
    public float baseSpeed;
    float boostSpeed;
    float boostCountdown;
    public float turnSmooth;
    public float gravity;
    [SerializeField]
    Affichage_Boost afficheBoost;

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
        playerStats = GetComponent<Player_Stats>();
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
                    moveDir = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward + Vector3.down * gravity;
                }
                else
                {
                    PlayerAnimator.SetBool("Forward", false);
                    moveDir = Vector3.down * gravity;
                }
                
                if (isRolling == false)
                {
                    transform.rotation = Quaternion.Euler(rotation);
                    charaCtrl.Move(moveDir.normalized * (baseSpeed + boostSpeed) * Time.deltaTime);//Mouvement
                }
                if(boostCountdown > 0)
                {
                    boostCountdown -= Time.deltaTime;
                }
                else
                {
                    if (boostSpeed > 0)
                    {
                        StopSpeedBoost();
                    }
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
                    charaCtrl.Move(moveDir.normalized * (dashSpeed + boostSpeed) * Time.deltaTime);
                    GetComponent<Player_Stats>().Invincibility(true);
                    invincibleCount -= Time.deltaTime;
                }
                else
                {
                    if (isRolling == true)
                    {
                        charaColl.enabled = true;
                        PlayerAnimator.ResetTrigger("Roulade");
                        GetComponent<Player_Stats>().Invincibility(false);
                        isRolling = false;
                    }
                }
            }
        }
    }

    void StartSpeedBoost(float _boostValue, float _boostTime)
    {
        boostSpeed = _boostValue;
        afficheBoost.StartBoostDisplay(_boostTime);
        isBoosted.Play();
        boostCountdown = _boostTime;
    }
    void StopSpeedBoost()
    {
        boostSpeed = 0;
        afficheBoost.StopBoostDisplay();
        isBoosted.Stop();
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
            Speed_Boost boostComponent = other.GetComponentInParent<Speed_Boost>();
            StartSpeedBoost(boostComponent.boostValue, boostComponent.boostTime);
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
