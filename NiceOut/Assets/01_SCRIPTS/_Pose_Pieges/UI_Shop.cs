using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    Inputs inputs;
    public Canvas ui_Manager;
    public GameObject player;
    public GameObject laCam;
    public GameObject uiShopPanel, uiLootButton;
    public Image uiLootImage;
    bool open, isOpened = false;

    public bool canShop;
    bool lootSelected;

    public GameObject[] allTraps; //tous les traps possibles

    public Wave_Handler waveHandler;
    int lootTrapIndex;

    int[] upgradeIndexes; //Stock les numero d'amélioration de chaque pièges
    int[] addedTraps; //Stock les pièges deja obtenuent.

    public int nbUpgradeMax = 2; 
    int nbTrapAdded = 0;
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
            upgradeIndexes[i] = ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax;
            addedTraps[i] = 0;
        }
    }

    private void Update()
    {

        if(canShop == true)
        {
            if(lootSelected == false)
            {
                LootChoice();
                ShopPanelOpenClose();
                lootSelected = true;
            }
        }
        else
        {
            if(lootSelected == true)
            {
                lootSelected = false;
            }
        }


    }

    public void ShopPanelOpenClose()
    {
        if (isOpened == false)
        {
            player.GetComponent<Switch_Mode>().SetPause();
            uiShopPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpened = true;
        }
        else
        {
            player.GetComponent<Switch_Mode>().SetPause();
            uiShopPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpened = false;
        }
    }

    public void LootChoice()
    {
        lootTrapIndex = waveHandler.enemisType;







       /* if (nbTrapAdded < ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax)
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

        lootTrapIndex = Random.Range(0, ui_Manager.GetComponent<Trap_Inventory>().nbUsedSlots); //index du gameobject a upgrade   

        if (upgradeIndexes[lootTrapIndex] == nbUpgradeMax) //check si toute les upgrade sont pas deja faite
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
                while (upgradeIndexes[lootTrapIndex] == nbUpgradeMax)
                {
                    Debug.Log("upgrade index " + upgradeIndexes[lootTrapIndex]);

                    lootTrapIndex = Random.Range(0, ui_Manager.GetComponent<Trap_Inventory>().nbUsedSlots);
                    Debug.Log(upgradeIndexes[lootTrapIndex] == nbUpgradeMax);
                }
            }

        }
        if (ui_Manager.GetComponent<Trap_Inventory>().trapsItem[lootTrapIndex] != null)
        {
            if ((upgradeIndexes[lootTrapIndex] + 1) <= 2)
            {
                uiLootImage.sprite = ui_Manager.GetComponent<Trap_Inventory>().trapsItem[lootTrapIndex].GetComponent<Traps>().ui_Image[upgradeIndexes[lootTrapIndex] + 1];
            }

        }*/
    }

    public void AddTrap()  //AddRandomTrap;
    {
        ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[lootTrapIndex]);
        upgradeIndexes[nbTrapAdded] = 0;
        addedTraps[lootTrapIndex] = 1;
        ShopPanelOpenClose();
        canShop = false;
        nbTrapAdded += 1;
    }

    public void UpgradeTrap()
    {
        int _type = ui_Manager.GetComponent<Trap_Inventory>().trapsItem[lootTrapIndex].GetComponent<Traps>().trapType; //Getle type du piege a upgrade dans l'inventaire
        ui_Manager.GetComponent<Trap_Inventory>().trapsItem[lootTrapIndex].GetComponent<Traps>().UpgradeForInventory(); //Ameliore le piege de l'inventaire pour que le joueur pose des piege améliorer
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
        ui_Manager.GetComponent<Trap_Inventory>().UpgradeTrapInventory(lootTrapIndex, upgradeIndexes[lootTrapIndex]);
        ShopPanelOpenClose();
        canShop = false;
    }

    public void AddLoot()
    {

    }

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
