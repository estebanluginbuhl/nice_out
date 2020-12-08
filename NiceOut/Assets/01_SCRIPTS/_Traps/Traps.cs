﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Traps : MonoBehaviour // detail d'achat et d'upgrade des pieges
{
    public int trapType;//Le type de piège sert à verif si on l'a deja dans l'inventaire

    [Header("Usure")]
    [SerializeField]
    float[] fullUsure;
    float usure;
    [HideInInspector]
    public float UsurePercentage;

    [Header("Cooldowns")]
    public float[] cooldownPose;
    public float[] cooldownSpawn;
    GameObject preview;
    Canvas previewTimer;
    [SerializeField]
    Canvas ui_Preview_Timer;
    [HideInInspector]
    public float cooldownCountdown;
    bool hasSPawned;

    [Header("Paramètre de spawn")]
    public GameObject[] trapAndUpgrades;
    public float[] offsetPositions;
    public Vector3 colliderSize;
    BoxCollider box;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject child;
    [HideInInspector]
    public Vector3 transformTrap;
    Vector3 rotationTrap;

    [Header("Parametres de spawn")]
    public int[] costs;
    public int[] sellCosts;

    public int upgradeIndex = 0; //Numero d'upgrade

    [Header("Elements d'UI")]
    [SerializeField]
    Canvas ui_healthBar;
    float ui_hbHeight = 4f;
    public Sprite[] ui_Image;
    public string description;

    private void Awake()
    {
        cooldownCountdown = 0;
        hasSPawned = false;
    }
    private void Start()
    {
        rotationTrap = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        preview = GameObject.Instantiate(this.trapAndUpgrades[trapAndUpgrades.Length - 1], transform.position, Quaternion.Euler(rotationTrap));
        previewTimer = Instantiate(ui_Preview_Timer, transform.position + Vector3.up * (ui_hbHeight/2), Quaternion.identity);
        previewTimer.transform.localScale = Vector3.one * 0.04f;
        previewTimer.GetComponent<Trap_Preview_Timer>().trap = this;

    }
    private void Update()
    {
        bool isPaused = player.GetComponent<Switch_Mode>().GetPause();
        if (isPaused == false)
        {
            if (cooldownCountdown < cooldownSpawn[upgradeIndex])
            {
                cooldownCountdown += Time.deltaTime;
            }
            if (cooldownCountdown >= cooldownSpawn[upgradeIndex] && hasSPawned == false)
            {
                box = GetComponent<BoxCollider>();
                if (box != null)
                {
                    Destroy(previewTimer.gameObject);
                    Destroy(preview);
                    box.size = colliderSize;
                    box.center = new Vector3(0, colliderSize.y / 2 + offsetPositions[upgradeIndex], 0);
                    box.isTrigger = true;
                }
                usure = fullUsure[this.upgradeIndex];
                this.UsurePercentage = usure / fullUsure[this.upgradeIndex];
                this.child = GameObject.Instantiate(this.trapAndUpgrades[upgradeIndex], transform.position, Quaternion.Euler(this.rotationTrap));
                this.child.GetComponent<Trap_Attack>().parentTrap = this.gameObject;
                this.child.GetComponent<Trap_Attack>().type = this.trapType;

                Canvas hb = Instantiate(ui_healthBar, transform.position + Vector3.up * ui_hbHeight, Quaternion.identity);
                hb.transform.localScale = Vector3.one * 0.05f;
                hb.GetComponent<Trap_Stats>().trap = this;
                hb.GetComponent<Trap_Stats>().player = this.player;
                hb.transform.SetParent(child.transform);
                hasSPawned = true;
            }
            if (cooldownCountdown >= cooldownSpawn[upgradeIndex] && hasSPawned == true)
            {
                usure -= Time.deltaTime;

                this.UsurePercentage = usure / fullUsure[this.upgradeIndex];

                if (this.upgradeIndex > this.trapAndUpgrades.Length - 1)
                {
                    this.upgradeIndex = this.trapAndUpgrades.Length - 1;
                }

                if (this.usure <= 1f)
                {
                    child.GetComponent<Trap_Attack>().isGonnaDie = true;
                }
                if (this.usure <= 0)
                {
                    Destroy(this.child);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void DamageTrap(int value)
    {
        this.usure -= value;
    }

    public void UpgradeForInventory()
    {
        this.upgradeIndex += 1;
    }

}
