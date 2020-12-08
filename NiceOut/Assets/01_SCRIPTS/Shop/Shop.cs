using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Shop : MonoBehaviour
{
    Inputs inputs;

    public bool buyingTime = false;
    StatsPlayer stats;
    Switch_Mode switchMode;
    public Trap_Inventory inventory;
    public Text[] costTexts;
    public GameObject[] buttons;
    public GameObject shopPanel;
    int[] upgradeIndexes;

    [SerializeField]
    private float shopRange;
    public LayerMask shopLayer;
    GameObject[] oldShop;    
    private void Awake()
    {
        inputs = new Inputs();
        inputs.Actions.Shop.started += ctx => buyingTime = !buyingTime;

        stats = GetComponent<StatsPlayer>();
        switchMode = GetComponent<Switch_Mode>();
        shopPanel.SetActive(false);
        foreach(GameObject b in buttons)
        {
            b.SetActive(false);
        }
        upgradeIndexes = new int[inventory.nbTrapMax];

        GameObject[] detectShops = GameObject.FindGameObjectsWithTag("Shop");
        foreach (GameObject s in detectShops)
        {
            s.GetComponent<FeedbackShop>().player = this.gameObject;
        }
    }

    private void Update()
    {
        OpenShop();
    }

    public void OpenShop()
    {
        if (buyingTime && shopPanel.activeInHierarchy == false)
        {
            Collider[] shops = Physics.OverlapSphere(transform.position, shopRange, shopLayer);
            if (shops.Length != 0)
            {
                switchMode.SetShopping();
                shopPanel.SetActive(true);
            }
            else
            {
                buyingTime = false;
            }
        }
        if (buyingTime == false && shopPanel.activeInHierarchy == true)
        {
            switchMode.SetShopping();
            shopPanel.SetActive(false);
        }
    }

    public void AddShopTrap(int _Index)
    {
        buttons[_Index].SetActive(true);
        costTexts[_Index].text = inventory.trapsItem[_Index].GetComponent<Traps>().costs[0].ToString();
        buttons[_Index].GetComponent<Image>().sprite = inventory.trapsItem[_Index].GetComponent<Traps>().ui_Image[0];
    }

    public void UpgradeShopTrap(int _Index, int _upgradeIndex)
    {
        costTexts[_Index].text = inventory.trapsItem[_Index].GetComponent<Traps>().costs[_upgradeIndex].ToString();
        buttons[_Index].GetComponent<Image>().sprite = inventory.trapsItem[_Index].GetComponent<Traps>().ui_Image[_upgradeIndex];
    }

    public void BuyTrap(int _BuyIndex)
    {
        int cost = inventory.trapsItem[_BuyIndex].GetComponent<Traps>().costs[upgradeIndexes[_BuyIndex]];
        if (stats.gold >= cost)
        {
            stats.PlayerBuy(cost);
            inventory.AddTraps(_BuyIndex);
        }
    }

    private void OnEnable()
    {
        inputs.Actions.Enable();
    }
    private void OnDisable()
    {
        inputs.Actions.Disable();
    }
}

