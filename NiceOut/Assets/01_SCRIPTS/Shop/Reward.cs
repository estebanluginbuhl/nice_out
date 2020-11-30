using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Reward : MonoBehaviour
{
    Inputs inputs;

    [Header("References")]
    [SerializeField]
    GameObject player;
    Wave_Manager waveManager;
    [SerializeField]
    Shop shop;

    [Header("Traps")]
    public GameObject[] allTraps; //tous les traps possibles
    public int nbUpgradeMax = 2;

    bool mustUpgrade;
    int rewardTrapIndex;
    int[] upgradeIndexes; //Stock les numero d'amélioration de chaque pièges
    int[] addedTraps; //Stock les pièges deja obtenuent.
    int nbTrapAdded = 0;

    //UI
    [Header("UI Elements")]
    public Canvas ui_Manager;
    public GameObject uiRewardPanel;
    public Image uiRewardImage;
    public TextMeshProUGUI uiRewardText;

    public bool rewardTime;
    bool open, isOpened = false;
    bool lootSelected;


    // Start is called before the first frame update

    private void Awake()
    {
        uiRewardPanel.SetActive(false);
        waveManager = GetComponent<Wave_Manager>();

        inputs = new Inputs();
        inputs.Actions.Reward.started += ctx => rewardTime = true;
        inputs.Actions.Reward.canceled += ctx => rewardTime = false;
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

        if (rewardTime == true)
        {
            if (lootSelected == false)
            {
                RewardSelection();
                RewardPanelOpenClose();
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

    public void RewardPanelOpenClose()
    {
        if (player.GetComponent<Switch_Mode>().realPause == false)
        {
            if (isOpened == false)
            {
                player.GetComponent<Switch_Mode>().SetShopping();
                uiRewardPanel.SetActive(true);
                isOpened = true;
            }
            else
            {
                player.GetComponent<Switch_Mode>().SetShopping();
                uiRewardPanel.SetActive(false);
                isOpened = false;
            }
        }
    }

    public void RewardSelection()
    {
        rewardTrapIndex = waveManager.lootType[waveManager.nbFirmesOnMap - 1];//Engros c'est le type de batiment et piège sélectionné au final. 
        for (int i = 2; i < waveManager.nbFirmesOnMap + 2; i++)
        {
            //Debug.Log(lootTrapIndex);
            //Debug.Log("turn " + i);
            if (nbTrapAdded < ui_Manager.GetComponent<Trap_Inventory>().nbTrapMax)////Si tosu les pièges n'ont pas déjà étaient obtenuent par le joueur
            {
                //Debug.Log("Pas tous les pièges");

                if (addedTraps[rewardTrapIndex] == 1)//Si le joueur possede deja le piège de cette firme
                {
                    //Debug.Log("déjà ajouté");
                    if (upgradeIndexes[rewardTrapIndex] < nbUpgradeMax) //check si toute les upgrade sont pas deja faite
                    {
                        //Debug.Log("peut s'upgrade");
                        mustUpgrade = true;
                        uiRewardText.text = "New Upgrade";
                        break;
                    }
                    else
                    {
                        //Debug.Log("ne peut pas s'upgrade");
                        waveManager.fullyUpgraded[rewardTrapIndex] = true;
                        rewardTrapIndex = waveManager.lootType[waveManager.nbFirmesOnMap - i];
                    }
                }
                else
                {
                    //Debug.Log("pas en core ajouté");
                    mustUpgrade = false;
                    uiRewardText.text = "New Trap";
                    break;
                }
            }
            else
            {
                //Debug.Log("deja tous les pièges");
                if (upgradeIndexes[rewardTrapIndex] < nbUpgradeMax) //check si toute les upgrade sont pas deja faite
                {
                    //Debug.Log("peut s'upgrade");
                    mustUpgrade = true;
                    uiRewardText.text = "New Upgrade";
                    break;
                }
                else
                {
                    //Debug.Log("ne peut pas s'upgrade");
                    waveManager.fullyUpgraded[rewardTrapIndex] = true;
                    rewardTrapIndex = waveManager.lootType[waveManager.nbFirmesOnMap - i];
                }
            }
        }

        if (mustUpgrade)
        {
            uiRewardImage.sprite = allTraps[rewardTrapIndex].GetComponent<Traps>().ui_Image[upgradeIndexes[rewardTrapIndex] + 1];
        }
        else
        {
            uiRewardImage.sprite = allTraps[rewardTrapIndex].GetComponent<Traps>().ui_Image[0];
        }
    }
    public void AddReward()
    {
        if (mustUpgrade)//UpgradeTrap
        {
            int _type = ui_Manager.GetComponent<Trap_Inventory>().trapsItem[rewardTrapIndex].GetComponent<Traps>().trapType; //Getle type du piege a upgrade dans l'inventaire
            ui_Manager.GetComponent<Trap_Inventory>().trapsItem[rewardTrapIndex].GetComponent<Traps>().UpgradeForInventory(); //Ameliore le piege de l'inventaire pour que le joueur pose des piege améliorés

            upgradeIndexes[rewardTrapIndex] += 1;
            if (upgradeIndexes[rewardTrapIndex] == 2)
            {
                waveManager.fullyUpgraded[rewardTrapIndex] = true;
            }
            ui_Manager.GetComponent<Trap_Inventory>().UpgradeTrapInventory(rewardTrapIndex, upgradeIndexes[rewardTrapIndex]);
            shop.UpgradeShopTrap(rewardTrapIndex, upgradeIndexes[rewardTrapIndex]);
            RewardPanelOpenClose();
            rewardTime = false;
        }
        else//AddTrap
        {
            ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[rewardTrapIndex], rewardTrapIndex);
            shop.AddShopTrap(rewardTrapIndex);
            upgradeIndexes[rewardTrapIndex] = 0;
            addedTraps[rewardTrapIndex] = 1;
            RewardPanelOpenClose();
            rewardTime = false;
            nbTrapAdded += 1;
        }
        waveManager.waveStarted = true;
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
