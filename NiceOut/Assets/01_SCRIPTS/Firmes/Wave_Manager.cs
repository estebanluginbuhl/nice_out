using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class Wave_Manager : MonoBehaviour
{
    //Les firmes on un int "type" comme les pièges allant de 0 à 4 comme les pièges
    // Aussi il faut que tu fasse le choix des differente firme de la wave aprés le wavestart, demande quand t'en est la je texpliquerai pk
    Reward reward;//Utilises shop.canShop = true quand la vague est terminée pour que le joueur puisse recevoir un piege ou une upgrade
    public Switch_Mode mode;
    Firme_Builder builder;
    public int waveIndex; //Numéro de la vague
    public int nbMaxWaves; //Nombre de vagues
    public bool initializeWave; //Début de la vague, les batiments sont choisis et remplacés, tout se remet a l'etat initial
    public bool play = false;
    public int nbEntity;
    public int nbAlly;
    public int nbEnemy;
    public int[] nbMaxEntity;//Nb d'ennemis par vagues, dépend de waveIndex du coup
    public int[] nbLoseEnemy;//Nb d'ennemis qui cause la defaite
    public int[] nbBaseNeutralEntity;//Nb d'ennemis neutres de base par vagues
    public float radiusSpawnNeutrals;
    public GameObject neutralEntityPrefab;
    public int[] nbMaxFirmes;//Nb de firme par vagues, dépend de waveIndex du coup

    public int nbFirmesOnMap; //nb de batiments de firmes sur la map. Meme si il y a plusieur fois la meme firme chaque batiment compte pour 1.
    int lootIndex = 0; //En gros c'est l'index du tableau de loot, a chaque batiment de la wave détruit ca monte de 1. On peut aussi s'en servir pour verif si tous les batiments on était détruit.Va de 0 à 7.
    public int[] lootType; //Stock dans l'ordre les index des firmes détruire;
    public bool[] fullyUpgraded; //Pour le choix des type d'entreprise : en gros quand ta desja toute les upgrade de ce trap faut pas que l'entreprise repop. l'index des cases du tableau correspond au type de l'entreprise.
    [Header("Affichage")]
    public GameObject losePanel, winPanel;
    public Image enemyBar, allyBar;
    public TextMeshProUGUI waveText;
    void Start()
    {
        waveText.gameObject.SetActive(false);
        allyBar.fillAmount = 0;
        enemyBar.fillAmount = 0;
        nbFirmesOnMap = nbMaxFirmes[waveIndex];
        reward = GetComponent<Reward>();
        builder = GetComponent<Firme_Builder>();
        fullyUpgraded = new bool[reward.ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax];
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    void Update()
    {
        if (waveIndex < nbMaxWaves)
        {
            if (nbEnemy >= nbLoseEnemy[waveIndex])
            {
                mode.realPause = true;
                mode.pause = true;
                Time.timeScale = 0;
                losePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        if (initializeWave)
        {
            StartWave();
        }
        if(play == true)
        {
            if(initializeWave == false && lootIndex == nbFirmesOnMap)
            {
                EndWave();
            }
        }
    }
    void StartWave()
    {
        AddBaseNeutrals();
        StartCoroutine(DisplayWaveText());
        lootIndex = 0;
        nbFirmesOnMap = nbMaxFirmes[waveIndex]; //nombre de firme sur la map = le nb pour cette wave en particulier
        lootType = new int[nbFirmesOnMap]; //A chaque nouvelle wave le tableau reset en fonction du nb de firmes de la wave;
        builder.ReplaceHousesByFirmes(waveIndex);
        initializeWave = false;
    }
    void EndWave()
    {
        waveIndex += 1;
        if(waveIndex == nbMaxWaves)
        {
            mode.realPause = true;
            mode.pause = true;
            Time.timeScale = 0;
            winPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            //Enleve les entités de la map
            GameObject[] entities1 = GameObject.FindGameObjectsWithTag("enemyTarget");
            GameObject[] entities2 = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject g1 in entities1)
            {
                Destroy(g1);
            }
            foreach (GameObject g2 in entities2)
            {
                Destroy(g2);
            }
            nbEnemy = 0;
            nbAlly = 0;
            nbEntity = 0;
            play = false;
            reward.rewardTime = true;//Ouvre le reward panel
            Debug.Log("DestroyEntities");
        }
    }
    void AddBaseNeutrals()
    {
        for (int i = 0; i < nbBaseNeutralEntity[waveIndex]; i++)
        {
            Vector3 spawnPlace = Vector3.zero;
            // Get Random Point inside Sphere which position is center, radius is maxDistance
            Vector3 randomPos = Random.insideUnitSphere * radiusSpawnNeutrals + Vector3.zero;
            NavMeshHit hit; // NavMesh Sampling Info Container
                            // from randomPos find a nearest point on NavMesh surface in range of maxDistance
            if (NavMesh.SamplePosition(randomPos, out hit, radiusSpawnNeutrals, NavMesh.AllAreas))
            {
                spawnPlace = hit.position;
            }
            else
            {
                return;
            }

            GameObject newNeutral = Instantiate(neutralEntityPrefab, spawnPlace, Quaternion.identity);
            newNeutral.GetComponent<Enemy_Stats>().InitializeEntity(0, 50, this);
        }
    }
    public void AddLootType(int destroyedFirmeType) //Quand un batiment de firme est detruit, il active cette fonction en rentrant son type.
    {
        lootType[lootIndex] = destroyedFirmeType;
        lootIndex += 1;
    }
    public void AddRemoveEntity(bool _which) //true = ajouter, false = remove
    {
        if (_which == true)
        {
            nbEntity += 1;
        }
        else
        {
            nbEntity -= 1;
        }
    }
    public void AddRemoveEnemy(bool _which) //true = ajouter, false = remove
    {
        if(_which == true)
        {
            nbEnemy += 1;
        }
        else
        {
            nbEnemy -= 1;
        }

        float nbEnemiesForBar = nbEnemy;
        float nbMaxEnemiesForBar = nbMaxEntity[waveIndex];
        enemyBar.fillAmount = nbEnemiesForBar / nbMaxEnemiesForBar;
    }
    public void AddRemoveAlly(bool _which) //true = ajouter, false = remove
    {
        if(_which == true)
        {
            nbAlly += 1;
        }
        else
        {
            nbAlly -= 1;
        }
        float nbEAlliesForBar = nbAlly;
        float nbMaxAlliesForBar = nbMaxEntity[waveIndex];
        allyBar.fillAmount = nbEAlliesForBar / nbMaxAlliesForBar;
    }
    IEnumerator DisplayWaveText()
    {
        waveText.gameObject.SetActive(true);
        waveText.text = string.Format("WAVE {0}", (waveIndex + 1));
        yield return new WaitForSecondsRealtime(3);
        waveText.gameObject.SetActive(false);
    }
}
