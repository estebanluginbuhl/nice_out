using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Entity_Stats : MonoBehaviour
{
    public int entityType;//correspon au type des traps et des firmes
    public Wave_Manager wavemanager;
    [Header("status bad enm = 2")]
    [Header("status allie = 1")]
    [Header("status neutral = 0")]
    public int status;

    GameObject player;
    [Header("Affichage")]
    //Visuel
    public GameObject smileyPrefab;
    [SerializeField]
    float distanceMaxPlayer;
    float distancePlayer;
    bool active = false;
    [SerializeField]
    private GameObject healthbar;
    [SerializeField]
    private Image healthImage;
    [SerializeField]
    GameObject minimap_Enemy_Color;    
    [SerializeField]
    GameObject minimap_Ally_Color;    
    [SerializeField]
    GameObject minimap_Neutral_Color;
    [SerializeField]
    private Gradient healthColor;
    [SerializeField]
    ParticleSystem convertBadEnemyParticle, convertGoodEnemyParticle, convertedToGood, convertedToBad, parfumed;
    [SerializeField]
    Material[] mats = new Material[3];

    [Header("Stats")]
    [SerializeField]
    private float enmHealth;
    Vector4 healthValues = new Vector4(0, 45, 55, 100);
    float healthPercentage;
    public bool hasDot = false;
    int damages;
    float range;
    int index;
    int duration;
    float cptCooldownBtweenContamination;
    GameObject temporaryTarget;
    LayerMask contaminationlayer;
    void Start()
    {
        player = GameObject.Find("PFB_Player_Controller");
        healthbar.SetActive(false);
        healthPercentage = enmHealth / healthValues.w;
        healthImage.color = healthColor.Evaluate(healthPercentage);
        healthImage.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);
        UpdateEnemyState();
    }

    void Update()
    {
        if(player != null)
        {
            distancePlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distancePlayer > distanceMaxPlayer)
            {
                if (active == true)
                {
                    healthbar.SetActive(false);
                    active = false;
                }
            }
            else
            {
                if (active == false)
                {
                    healthbar.SetActive(true);
                    active = true;
                }
            }
        }
        //Contamination
        if (duration > 0 && hasDot == true)
        {
            if(cptCooldownBtweenContamination <= 0)
            {
                Collider[] closeEnemies = Physics.OverlapSphere(transform.position, range, contaminationlayer);
                if (closeEnemies.Length != 0)
                {
                    if(temporaryTarget == null)
                    {
                        float minDist = Mathf.Infinity;
                        foreach (Collider c in closeEnemies)
                        {
                            float dist = Vector3.Distance(transform.position, c.transform.position);
                            if(c.gameObject.GetComponent<Entity_Stats>().hasDot == false)
                            {
                                if (dist < minDist)
                                {
                                    minDist = dist;
                                    temporaryTarget = c.gameObject;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        damages -= 1;
                        duration -= 1;
                        index -= 1;
                        if(damages == 0 || duration == 0 || index == 0)
                        {
                            duration = 0;
                            index = 0;
                            temporaryTarget = null;
                            hasDot = false;
                            parfumed.Stop();
                            return;
                        }
                        else
                        {
                            StartCoroutine(temporaryTarget.GetComponent<Entity_Stats>().Parfume(damages, duration, range, index, contaminationlayer));
                            cptCooldownBtweenContamination = 1;
                            temporaryTarget = null;
                        }
                    }
                }
            }
        }
    }

    public void DamageGoodEntity(int takenDamage, int _type)
    {
        if (smileyPrefab != null)//smiley
        {
            ChangeSmileyColor(1);
            ShowSmiley();
        }

        enmHealth -= takenDamage;
        entityType = _type;
        if (enmHealth <= healthValues.x)
        {
            enmHealth = healthValues.x;
        }
        healthPercentage = enmHealth / healthValues.w;
        healthImage.color = healthColor.Evaluate(healthPercentage);
        healthImage.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);
        convertGoodEnemyParticle.Play();
        UpdateEnemyState();
    }
    public void DamageBadEntity(int takenDamage)
    {
        if (smileyPrefab != null)//smiley
        {
            ChangeSmileyColor(2);
            ShowSmiley();
        }

        enmHealth += takenDamage;
        if (enmHealth >= healthValues.w)
        {
            enmHealth = healthValues.w;
        }
        healthPercentage = enmHealth / healthValues.w;
        healthImage.color = healthColor.Evaluate(healthPercentage);
        healthImage.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);
        convertBadEnemyParticle.Play();
        UpdateEnemyState();
    }
    void UpdateEnemyState()
    {
        if (enmHealth < healthValues.y)//45 et 0 hostile
        {
            if (status != 2)
            {
                wavemanager.AddRemoveEnemy(true);
                minimap_Neutral_Color.SetActive(false);
                minimap_Enemy_Color.SetActive(true);
                transform.gameObject.tag = "enemy";
                status = 2;
                gameObject.layer = 13;
                GetComponentInChildren<MeshRenderer>().material = mats[2];
                convertedToBad.Play();
            }
        }
        else if (enmHealth > healthValues.z)//55 et 100 allie
        {
            if (status != 1)
            {
                wavemanager.AddRemoveAlly(true);
                minimap_Neutral_Color.SetActive(false);
                minimap_Ally_Color.SetActive(true);
                transform.gameObject.tag = "enemyTarget";
                status = 1;
                gameObject.layer = 17;
                GetComponentInChildren<MeshRenderer>().material = mats[1];
                convertedToGood.Play();
                if(player != null)
                {
                    player.GetComponent<Player_Stats>().RincePlayer(50);//PrixDuMonstre
                }
            }
        }
        else if (enmHealth >= healthValues.y && enmHealth <= healthValues.z)//entre 45 et 55 neutral
        {
            if (status != 0)
            {
                wavemanager.AddRemoveAlly(false);
                wavemanager.AddRemoveEnemy(false);
                minimap_Neutral_Color.SetActive(true);
                minimap_Ally_Color.SetActive(false);
                minimap_Enemy_Color.SetActive(false);
                transform.gameObject.tag = "enemyTarget";
                status = 0;
                gameObject.layer = 15;
                GetComponentInChildren<MeshRenderer>().material = mats[0];
            }
        }
    }
    public void InitializeEntity(int _type, int _spawnHealth, Wave_Manager _waveManager)
    {
        entityType = _type;
        enmHealth = _spawnHealth;
        wavemanager = _waveManager;
        UpdateEnemyState();
    }

    void ChangeSmileyColor(int i)
    {
        if (i == 1)
        {
            smileyPrefab.GetComponentInChildren<RawImage>().color = Color.red;
        }
        else if (i == 2)
        {
            smileyPrefab.GetComponentInChildren<RawImage>().color = Color.green;
        }
        else return;
    }
    void ShowSmiley()
    {
        Instantiate(smileyPrefab, transform.position, Quaternion.identity, transform);
    }

    public IEnumerator Parfume(int _damage, int _duration, float _range, int _index, LayerMask _contaminationlayer)//DOT parfum
    {
        damages = _damage;
        duration = _duration;
        range = _range;
        index = _index;
        contaminationlayer = _contaminationlayer;
        hasDot = true;
        parfumed.Play();
        while (duration > 0 && index > 0)
        {
            DamageBadEntity(damages);
            yield return new WaitForSecondsRealtime(1);
            duration -= 1;
            if(duration < 1)
            {
                parfumed.Stop();
                hasDot = false;
            }
        }
    }
}
