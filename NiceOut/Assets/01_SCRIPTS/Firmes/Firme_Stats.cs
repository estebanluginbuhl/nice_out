using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Firme_Stats : MonoBehaviour
{
    public int firmeType;
    int firmeIndex;
    Wave_Manager waveManager;
    Firme_Builder buildManager;
    //BuildingManager buildManager;
    public MeshRenderer mshRdr;

    public Animator anm;

    [SerializeField]
    Image firme_Health_Bar;
    float health;
    [SerializeField]
    float healthMax; //Vie de base de ce batiment de firme
    public Material[] typeMats;

    public float destructionFeedbackTime;
    public float feedbackTimer;
    public float timeBeforeHouseRecall;
    public float houseRecallTimer;
    bool destruction;
    bool recall;
    // Start is called before the first frame update
    void Start()
    {
        anm = GetComponentInChildren<Animator>();
        waveManager = GameObject.Find("PFB_Game_Manager").GetComponent<Wave_Manager>();//temporaire pour les tests
        health = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(destruction == true)
        {
            if (feedbackTimer <= 0)
            {
                DestroyFirme();
                destruction = false;
            }
            else
            {
                feedbackTimer -= Time.deltaTime;
            }
        }

        if(recall == true)
        {
            if (houseRecallTimer <= 0)
            {
                buildManager.RecallModifiedHouses(firmeIndex);
                recall = false;
            }
            else
            {
                houseRecallTimer -= Time.deltaTime;
            }
        }
    }
    
    public void DamageFirme(int _damages)//fait des  dégats à ce batiment de firme
    {
        anm.SetTrigger("Damages");
        health -= _damages;
        firme_Health_Bar.fillAmount = health / healthMax;
        waveManager.AddRemoveEntity(false);
        if(health <= 0)
        {
            StartDestroyFirme();
        }
    }

    void StartDestroyFirme()//detruit ce batiment de firme
    {
        feedbackTimer = destructionFeedbackTime;
        houseRecallTimer = timeBeforeHouseRecall;
        destruction = true;
        recall= true;
        anm.SetBool("Destroy", true);
        waveManager.AddLootType(firmeType, this.gameObject.transform);
        gameObject.layer = 0;
    }


    public void DestroyFirme()//detruit ce batiment de firme
    {
        waveManager.CanEndWave();
        Destroy(this.gameObject);
    }

    public void InitializeFirmeTest(int _type, int _index, Wave_Manager _waveManager, Firme_Builder _testBuilder)//Methode à invoquer à 100% pour chaqsue nouveaux batiments de firmes
    {
        waveManager = _waveManager;
        firmeType = _type;
        firmeIndex = _index;
        buildManager = _testBuilder;
        GetComponent<Entity_Spawner>().waveManager = _waveManager;
        mshRdr.material = typeMats[_type];
    }
}
