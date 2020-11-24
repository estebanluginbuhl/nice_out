using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Old_Shop : MonoBehaviour
{
    Inputs inputs;
    public Canvas ui_Manager;
    public GameObject player;
    public GameObject laCam;
    public GameObject shopPanel, addButton, upgradeButton, statsButton; //poput c'est le truc qui montre ou est le shop et l'input pour l'ouvrir
    public Image imageUpgrade, imageAdd, imageStats;
    public Text textStats;
    bool open, isOpened = false;

    public bool canShop;
    bool randomized;
    public GameObject[] allTraps;

    int upgradeTrapIndex;
    int addTrapIndex;
    int rndStat;
    int rndUpValue;
    public Vector2 pourcentageUpgradeStatsMinMax;
    int[] upgradeIndexes;
     int[] addedTraps;
    public int nbUpgradeMax = 2;
    int nbTrapAdded = 0;
    public Sprite health_Image;
    public Sprite Speed_Image;
    public Sprite Energy_Image;
    // Start is called before the first frame update

    private void Awake()
    {
        shopPanel.SetActive(false);

        inputs = new Inputs();
        inputs.Actions.Shop.started += ctx => canShop = true;
        inputs.Actions.Shop.canceled += ctx => canShop = false;
    }

    private void Start()
    {
        nbTrapAdded = 0;
        foreach (GameObject item in allTraps)
        {
            item.GetComponent<Traps>().upgradeIndex = 0;
        }
        upgradeIndexes = new int[ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax];
        addedTraps = new int[ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax];
        for (int i = 0; i < ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax; i++)
        {
            upgradeIndexes[i] = ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax;
            addedTraps[i] = 0;
        }
    }

    private void Update()
    {

        if(canShop == true)
        {
            if(randomized == false)
            {
                RandomizeLoot();
                ShopPanelOpenClose();
                randomized = true;
            }
        }
        else
        {
            if(randomized == true)
            {
                randomized = false;
            }
        }


    }

    public void ShopPanelOpenClose()
    {
        if (isOpened == false)
        {
            player.GetComponent<Switch_Mode>().SetPause();
            shopPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpened = true;
        }
        else
        {
            player.GetComponent<Switch_Mode>().SetPause();
            shopPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpened = false;
        }
    }

    public void RandomizeLoot()
    {
        addButton.SetActive(true);
        upgradeButton.SetActive(true);
        statsButton.SetActive(true);
        Random rnd = new Random();
        
        if(nbTrapAdded < ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax)
        {
            addTrapIndex = Random.Range(0, ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax); //index du gameobject a ajouter
            if (addedTraps[addTrapIndex] == 1)
            {
                while (addedTraps[addTrapIndex] == 1)
                {
                    addTrapIndex = Random.Range(0, ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax);
                }
            } 
        }
        else
        {
            addButton.SetActive(false);
        }// check si tous l'inventaire est pas plein

        if (allTraps[addTrapIndex] != null)
        {
            imageAdd.sprite = allTraps[addTrapIndex].GetComponent<Traps>().ui_Image[0];
        }

        upgradeTrapIndex = Random.Range(0, ui_Manager.GetComponent<Trap_Inventory>().nbUsedSlots); //index du gameobject a upgrade   

        if (upgradeIndexes[upgradeTrapIndex] == nbUpgradeMax) //check si toute les upgrade sont pas deja faite
        {
            //Debug.Log("peut pas ou plus upgrade");
            bool isUpgradable = false;
            for (int i = 0; i < ui_Manager.GetComponent<Trap_Inventory>().nbUsedSlots; i++)
            {
                if (upgradeIndexes[i] != nbUpgradeMax)
                {
                    isUpgradable = true;
                }
            }//verif si il reste des upgrade à faire

            if (isUpgradable == true)
            {
                Debug.Log("upgradable");
                while (upgradeIndexes[upgradeTrapIndex] == nbUpgradeMax)
                {
                    Debug.Log("upgrade index " + upgradeIndexes[upgradeTrapIndex]);

                    upgradeTrapIndex = Random.Range(0, ui_Manager.GetComponent<Trap_Inventory>().nbUsedSlots);
                    Debug.Log(upgradeIndexes[upgradeTrapIndex] == nbUpgradeMax);
                }
            }
            else
            {
                upgradeButton.SetActive(false);
            }
        }
        if (ui_Manager.GetComponent<Trap_Inventory>().trapsItem[upgradeTrapIndex] != null)
        {
            if((upgradeIndexes[upgradeTrapIndex] + 1) <= 2)
            {
                imageUpgrade.sprite = ui_Manager.GetComponent<Trap_Inventory>().trapsItem[upgradeTrapIndex].GetComponent<Traps>().ui_Image[upgradeIndexes[upgradeTrapIndex] + 1];
            }

        }
        if(nbTrapAdded == 0)
        {
            upgradeButton.SetActive(false);
            statsButton.SetActive(false);
        }

        rndStat = Random.Range(1, 4);//Choix de la stat à améliorer
        rndUpValue = Mathf.RoundToInt(Random.Range(pourcentageUpgradeStatsMinMax.x, pourcentageUpgradeStatsMinMax.y)); //Pourcentage d'augmentation de la stat selectionné (pas de +1 pour le max du random pcq float = max inclu)

        if (rndStat == 1)
        {
            imageStats.sprite = health_Image;
            textStats.text = "+ " + rndUpValue + " HP";
        }
        if (rndStat == 2)
        {
            imageStats.sprite = Speed_Image;
            textStats.text = "+ " + rndUpValue + "% MS";

        }
        if (rndStat == 3)
        {
            imageStats.sprite = Energy_Image;
            textStats.text = "+ " + rndUpValue + " EN/s";
        }
    }

    public void AddTrap()  //AddRandomTrap;
    {
        ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[addTrapIndex]);
        upgradeIndexes[nbTrapAdded] = 0;
        addedTraps[addTrapIndex] = 1;
        ShopPanelOpenClose();
        canShop = false;
        nbTrapAdded += 1;
    }

    public void UpgradeTrap()
    {
        int _type = ui_Manager.GetComponent<Trap_Inventory>().trapsItem[upgradeTrapIndex].GetComponent<Traps>().trapType; //Getle type du piege a upgrade dans l'inventaire
        ui_Manager.GetComponent<Trap_Inventory>().trapsItem[upgradeTrapIndex].GetComponent<Traps>().UpgradeForInventory(); //Ameliore le piege de l'inventaire pour que le joueur pose des piege améliorer
        //ui_Manager.GetComponent<Trap_Inventory>().trapsItem[rndUpgradeTrapIndex].GetComponent<Traps>().UpgradeInventoryTrap();
        GameObject[] trapsToUpgrade = GameObject.FindGameObjectsWithTag("Trap");

        foreach (GameObject gmObj in trapsToUpgrade)
        {
            if (gmObj.GetComponent<Traps>().trapType == _type)
            {
                gmObj.GetComponent<Traps>().Upgrade();
            }
        }
        upgradeIndexes[upgradeTrapIndex] += 1;
        ui_Manager.GetComponent<Trap_Inventory>().UpgradeTrapInventory(upgradeTrapIndex, upgradeIndexes[upgradeTrapIndex]);
        ShopPanelOpenClose();
        canShop = false;
    }

    /* public void UpStats()
    {
        if(rndStat == 1)
        {
            player.GetComponent<StatsPlayer>().UpgradeHealth(rndUpValue);
        }
        if(rndStat == 2)
        {
            player.GetComponent<ThirdPersonControler>().UpgradeSpeed(rndUpValue);
        }
        if(rndStat == 3)
        {
            player.GetComponent<StatsPlayer>().UpgradeChargeSpeed(rndUpValue);
        }
        ShopPanelOpenClose();
        canShop = false;
    }*/

    public void AllTraps()
    {
        for (int i = 0; i < allTraps.Length; i++)
        {
            ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[i]);
        }
    } //temporaire

    private void OnEnable()
    {
        inputs.Actions.Enable();
    }
    private void OnDisable()
    {
        inputs.Actions.Disable();
    }
}
