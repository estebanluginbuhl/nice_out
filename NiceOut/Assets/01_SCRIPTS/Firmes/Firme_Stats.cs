using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Firme_Stats : MonoBehaviour
{
    int firmeType;
    int firmeIndex;
    Wave_Manager waveManager;
    Test_Builder buildManager;
    //BuildingManager buildManager;
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
        if(health/healthMax >= 0 && firme_Health_Bar != null)
        {
            firme_Health_Bar.fillAmount = health / healthMax;
        }
    }
    
    public void DamageFirme(int _damages)//fait des  dégats à ce batiment de firme
    {
        health -= _damages;
        waveManager.AddRemoveEntity(false);
        if(health <= 0)
        {
            DestroyFirme();
        }
    }

    void DestroyFirme()//detruit ce batiment de firme
    {
        waveManager.AddLootType(firmeType);
        buildManager.RecallModifiedHouses(firmeIndex);
        Destroy(this.gameObject);
       // buildManager.CallShop();
       // buildManager.RecallHouse();//Faut le rechangé en maison pas le detruire
    }

    /*public void InitializeFirme(int _type, int _index, Wave_Manager _waveManager, BuildingManager _buildingManager)//Methode à invoquer à 100% pour chaqsue nouveaux batiments de firmes
    {
        waveManager = _waveManager;
        firmeType = _type;
        firmeIndex = _index;
        buildManager = _buildingManager;
        GetComponent<Entity_Spawner>().waveManager = _waveManager;
    }*/
    public void InitializeFirmeTest(int _type, int _index, Wave_Manager _waveManager, Test_Builder _testBuilder)//Methode à invoquer à 100% pour chaqsue nouveaux batiments de firmes
    {
        waveManager = _waveManager;
        firmeType = _type;
        firmeIndex = _index;
        buildManager = _testBuilder;
        GetComponent<Entity_Spawner>().waveManager = _waveManager;
    }
}
