﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Firme_Stats : MonoBehaviour
{
    int firmeType;
    Wave_Manager waveManager;

    [SerializeField]
    Image firme_Health_Bar;
    float health;
    [SerializeField]
    float healthMax; //Vie de base de ce batiment de firme


    // Start is called before the first frame update
    void Start()
    {
        waveManager = GameObject.Find("PFB_Game_Manager").GetComponent<Wave_Manager>();//temporaire pour les tests
        health = healthMax;
    }

    // Update is called once per frame
    void Update() //update la barre de vie
    {
        firme_Health_Bar.fillAmount = health / healthMax;
    }
    
    public void DamageFirme(int _damages)//fait des  dégats à ce batiment de firme
    {
        health -= _damages;
        waveManager.AddRemoveEntity(false);
    }

    void Destroy()//detruit ce batiment de firme
    {
        waveManager.AddLootType(firmeType);
        Destroy(this.gameObject);
    }

    void InitializeFirme(int _type, Wave_Manager _waveManager)//Methode à invoquer à 100% pour chaqsue nouveaux batiments de firmes
    {
        waveManager = _waveManager;
        firmeType = _type;

        GetComponent<Entity_Spawner>().waveManager = _waveManager;
    }
}
