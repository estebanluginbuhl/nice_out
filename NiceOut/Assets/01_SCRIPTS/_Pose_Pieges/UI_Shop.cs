using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    Inputs inputs;

    [Header("References")]
    public GameObject player;
    public GameObject laCam;
    public Wave_Manager waveHandler;

    [Header("Traps")]
    public GameObject[] allTraps; //tous les traps possibles
    public int nbUpgradeMax = 2;

    bool mustUpgrade;
    int lootTrapIndex;
    int[] upgradeIndexes; //Stock les numero d'amélioration de chaque pièges
    int[] addedTraps; //Stock les pièges deja obtenuent.
    int nbTrapAdded = 0;

    //UI
    [Header("UI Elements")]
    public Canvas ui_Manager;
    public GameObject uiShopPanel;
    public Image uiLootImage;
    public TextMeshProUGUI uiLootText;

    public bool canShop;
    bool open, isOpened = false;
    bool lootSelected;


    // Start is called before the first frame update

    private void Awake()
    {
        uiShopPanel.SetActive(false);

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
            upgradeIndexes[i] = 0;
            addedTraps[i] = 0;
        }
    }

    private void Update()
    {

        if(canShop == true)
        {
            if (lootSelected == false)
            {
                LootChoice();
                ShopPanelOpenClose();
                lootSelected = true;
            }
        }
        else
        {
            if (lootSelected == true)
            {
                lootSelected = false;
            }
        }
    }

    public void ShopPanelOpenClose()
    {
        if (player.GetComponent<Switch_Mode>().realPause == false)
        {
            if (isOpened == false)
            {
                player.GetComponent<Switch_Mode>().SetShopping();
                uiShopPanel.SetActive(true);
                isOpened = true;
            }
            else
            {
                player.GetComponent<Switch_Mode>().SetShopping();
                uiShopPanel.SetActive(false);
                isOpened = false;
            }
        }
    }

    public void LootChoice()
    {
        lootTrapIndex = waveHandler.lootType[waveHandler.nbFirmesOnMap - 1];//Engros c'est le type de batiment et piège sélectionné au final. 
        for (int i = 2; i < waveHandler.nbFirmesOnMap + 2; i++)
        {
            //Debug.Log(lootTrapIndex);
            //Debug.Log("turn " + i);
            if (nbTrapAdded < ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax)////Si tosu les pièges n'ont pas déjà étaient obtenuent par le joueur
            {
                //Debug.Log("Pas tous les pièges");

                if (addedTraps[lootTrapIndex] == 1)//Si le joueur possede deja le piège de cette firme
                {
                    //Debug.Log("déjà ajouté");
                    if (upgradeIndexes[lootTrapIndex] < nbUpgradeMax) //check si toute les upgrade sont pas deja faite
                    {
                        //Debug.Log("peut s'upgrade");
                        mustUpgrade = true;
                        uiLootText.text = "New Upgrade";
                        break;
                    }
                    else
                    {
                        //Debug.Log("ne peut pas s'upgrade");
                        waveHandler.fullyUpgraded[lootTrapIndex] = true;
                        lootTrapIndex = waveHandler.lootType[waveHandler.nbFirmesOnMap - i];
                    }
                }
                else
                {
                    //Debug.Log("pas en core ajouté");
                    mustUpgrade = false;
                    uiLootText.text = "New Trap";
                    break;
                }
            }
            else
            {
                //Debug.Log("deja tous les pièges");
                if (upgradeIndexes[lootTrapIndex] < nbUpgradeMax) //check si toute les upgrade sont pas deja faite
                {
                    //Debug.Log("peut s'upgrade");
                    mustUpgrade = true;
                    uiLootText.text = "New Upgrade";
                    break;
                }
                else
                {
                    //Debug.Log("ne peut pas s'upgrade");
                    waveHandler.fullyUpgraded[lootTrapIndex] = true;
                    lootTrapIndex = waveHandler.lootType[waveHandler.nbFirmesOnMap - i];
                }
            }
        }

        if (mustUpgrade)
        {
            uiLootImage.sprite = allTraps[lootTrapIndex].GetComponent<Traps>().ui_Image[upgradeIndexes[lootTrapIndex] + 1];
        }
        else
        {
            uiLootImage.sprite = allTraps[lootTrapIndex].GetComponent<Traps>().ui_Image[0];
        }
    }
    public void AddLoot()
    {
        if (mustUpgrade)//UpgradeTrap
        {
            int _type = ui_Manager.GetComponent<Trap_Inventory>().trapsItem[lootTrapIndex].GetComponent<Traps>().trapType; //Getle type du piege a upgrade dans l'inventaire
            ui_Manager.GetComponent<Trap_Inventory>().trapsItem[lootTrapIndex].GetComponent<Traps>().UpgradeForInventory(); //Ameliore le piege de l'inventaire pour que le joueur pose des piege améliorés
                                                                                                                            //ui_Manager.GetComponent<Trap_Inventory>().trapsItem[rndUpgradeTrapIndex].GetComponent<Traps>().UpgradeInventoryTrap();
            GameObject[] trapsToUpgrade = GameObject.FindGameObjectsWithTag("Trap");

            foreach (GameObject gmObj in trapsToUpgrade)
            {
                if (gmObj.GetComponent<Traps>().trapType == _type)
                {
                    gmObj.GetComponent<Traps>().Upgrade();
                }
            }
            upgradeIndexes[lootTrapIndex] += 1;
            if (upgradeIndexes[lootTrapIndex] == 2)
            {
                waveHandler.fullyUpgraded[lootTrapIndex] = true;
            }
            ui_Manager.GetComponent<Trap_Inventory>().UpgradeTrapInventory(lootTrapIndex, upgradeIndexes[lootTrapIndex]);
            ShopPanelOpenClose();
            canShop = false;
        }
        else//AddTrap
        {
            ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[lootTrapIndex], lootTrapIndex);
            upgradeIndexes[lootTrapIndex] = 0;
            addedTraps[lootTrapIndex] = 1;
            ShopPanelOpenClose();
            canShop = false;
            nbTrapAdded += 1;
        }
        waveHandler.waveStarted = true;
    }

    public void AllTraps()
    {
        for (int i = 0; i < allTraps.Length; i++)
        {
            ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[i], i);
            upgradeIndexes[i] = 0;
            addedTraps[i] = 1;
            nbTrapAdded += 1;
        }
    } //temporaire, donne tous les traps au joueur

    private void OnEnable()
    {
        inputs.Actions.Enable();
    }
    private void OnDisable()
    {
        inputs.Actions.Disable();
    }
}
