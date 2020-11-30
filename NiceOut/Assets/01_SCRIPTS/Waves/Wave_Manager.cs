using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Manager : MonoBehaviour
{
    //Les firmes on un int "type" comme les pièges allant de 0 à 4 comme les pièges
    // Aussi il faut que tu fasse le choix des differente firme de la wave aprés le wavestart, demande quand t'en est la je texpliquerai pk
    public UI_Shop shop;//Utilises shop.canShop = true quand la vague est terminée pour que le joueur puisse recevoir un piege ou une upgrade

    public int nbWaves;
    public bool waveStarted;
    public int nbFirmesOnMap; //nb de batiments de firmes sur la map par wave. Meme si il y a plusieur fois la meme firme chaque batiment compte pour 1
    int lootIndex = 0; //En gros c'est l'index du tableau de loot, a chaque batiment de la wave détruit ca monte de 1. On peut aussi s'en servir pour verif si tous les batiments on était détruit.Va de 0 à 4.
    public int[] lootType; //Stock dans l'ordre les index des firmes détruire;
    public bool[] fullyUpgraded; //Pour le choix des type d'entreprise : en gros quand ta desja toute les upgrade de ce trap faut pas que l'entreprise repop. l'index des cases du tableau correspond au type de l'entreprise.

    void Start()
    {
        lootType = new int[nbFirmesOnMap]; //A chaque nouvelle wave le tableau reset en fonction du nb de firmes de la wave;
        fullyUpgraded = new bool[nbFirmesOnMap];
    }

    void Update()
    {
        /*if (waveStarted)//reactive quand t'aura fait le systeme de batiments de firme sur la map et de wave c'est pour creer le tableau qui stock les batiment cassés dans l'ordre, faut le refaire a chaque fois en fonction du nombre de batiments
        {
            lootIndex = 0;
            lootType = new int[nbFirmesOnMap]; //A chaque nouvelle wave le tableau reset en fonction du nb de firmes de la wave;
            fullyUpgraded = new bool[nbFirmesOnMap];
            waveStarted = false;
        }*/

    }

    void AddLootType(int destroyedFirmeType) //Quand un batiment de firme est detruit, il active cette fonction en rentrant son type.
    {
        lootType[lootIndex] = destroyedFirmeType;
        lootIndex += 1;
    }
}
