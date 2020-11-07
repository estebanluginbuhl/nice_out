using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traps : MonoBehaviour // detail d'achat et d'upgrade des pieges
{
    GameObject player, shop;
    GameObject child;
    public Sprite uiImage;
    public float[] offsetPositions;
    public int trapType; //Le type de piège sert à verif si on l'a deja dans l'inventaire
    public int[] costs;
    public int[] sellCosts;
    public int upgradeIndex = 0;
    public GameObject[] trapAndUpgrades;
    public bool canUpgrade;
    public int life;
    public GameObject emplacement;

    private void Start()
    {
        upgradeIndex += 1;
        player = GameObject.Find("ThirdPersonController");
        shop = GameObject.Find("Shop");
        child = GameObject.Instantiate(trapAndUpgrades[0], transform.position + Vector3.up * offsetPositions[0], Quaternion.identity);
    }

    private void Update()
    {
        //Check for upgrade
        if(shop.GetComponent<Upgrade_Gestion>().CheckForUpgrade(trapType, upgradeIndex) == true)
        {
            if (player.GetComponent<StatsPlayer>().gold >= costs[upgradeIndex])
            {
                if (canUpgrade != true)
                {
                    canUpgrade = true;
                }
            }
            else
            {
                if (canUpgrade != false)
                {
                    canUpgrade = false;
                }
            }
        }
    }

    public void Upgrade()//Upgrade le piège
    {
        if (canUpgrade)
        {
            Destroy(child);
            child = GameObject.Instantiate(trapAndUpgrades[upgradeIndex], transform.position + Vector3.up * offsetPositions[upgradeIndex], Quaternion.identity);
            upgradeIndex += 1;
        }
        else
        {
            return;
        }
    }

    public void DamageTrap(int value)
    {
        life -= value;
        if(life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
