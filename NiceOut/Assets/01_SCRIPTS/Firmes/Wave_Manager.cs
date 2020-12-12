using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Manager : MonoBehaviour
{
    //Les firmes on un int "type" comme les pièges allant de 0 à 4 comme les pièges
    // Aussi il faut que tu fasse le choix des differente firme de la wave aprés le wavestart, demande quand t'en est la je texpliquerai pk
    Reward reward;//Utilises shop.canShop = true quand la vague est terminée pour que le joueur puisse recevoir un piege ou une upgrade
    //BuildingManager builder;
    Test_Builder builder;
    public int waveIndex; //Numéro de la vague
    public int nbMaxWaves; //Nombre de vagues
    public bool initializeWave; //Début de la vague, les batiments sont choisis et remplacés, tout se remet a l'etat initial
    public bool play = false;
    public int nbEntity;
    public int[] nbMaxEntity;//Nb d'ennemis par vagues, dépend de waveIndex du coup
    public int[] nbMaxFirmes;//Nb de firme par vagues, dépend de waveIndex du coup
    public int nbFirmesOnMap; //nb de batiments de firmes sur la map. Meme si il y a plusieur fois la meme firme chaque batiment compte pour 1.
    int lootIndex = 0; //En gros c'est l'index du tableau de loot, a chaque batiment de la wave détruit ca monte de 1. On peut aussi s'en servir pour verif si tous les batiments on était détruit.Va de 0 à 7.
    public int[] lootType; //Stock dans l'ordre les index des firmes détruire;
    public bool[] fullyUpgraded; //Pour le choix des type d'entreprise : en gros quand ta desja toute les upgrade de ce trap faut pas que l'entreprise repop. l'index des cases du tableau correspond au type de l'entreprise.

    void Start()
    {
        nbFirmesOnMap = nbMaxFirmes[waveIndex];
        reward = GetComponent<Reward>();
        //builder = GetComponent<BuildingManager>();
        builder = GetComponent<Test_Builder>();
        fullyUpgraded = new bool[reward.ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax];
    }

    void Update()
    {
        if (initializeWave)
        {
            StartWave();
        }
        if(play == true)
        {
            if(initializeWave = false && lootIndex == nbFirmesOnMap - 1)
            {
                EndWave();
            }
        }

    }
    void StartWave()
    {
        lootIndex = 0;
        nbFirmesOnMap = nbMaxFirmes[waveIndex]; //nombre de firme sur la map = le nb pour cette wave en particulier
        lootType = new int[nbFirmesOnMap]; //A chaque nouvelle wave le tableau reset en fonction du nb de firmes de la wave;
        builder.ReplaceHousesByFirmes(nbFirmesOnMap);
        initializeWave = false;
    }
    void EndWave()
    {
        waveIndex += 1;
        play = false;
        reward.rewardTime = true;//Ouvre le reward panel
        //Enleve les entités de la map
        GameObject[] entities1 = GameObject.FindGameObjectsWithTag("enemyTarget");
        GameObject[] entities2 = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject g1 in entities1)
        {
            Destroy(g1);
        }
        foreach(GameObject g2 in entities2)
        {
            Destroy(g2);
        }
    }

    public void AddLootType(int destroyedFirmeType) //Quand un batiment de firme est detruit, il active cette fonction en rentrant son type.
    {
        lootType[lootIndex] = destroyedFirmeType;
        lootIndex += 1;
    }

    public void AddRemoveEntity(bool _which) //true = ajouter, false = remove
    {
        if(_which == true)
        {
            nbEntity += 1;
        }
        else
        {
            nbEntity -= 1;
        }
    }
}
