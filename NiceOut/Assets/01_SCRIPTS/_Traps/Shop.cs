using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Inputs inputs;
    public Canvas ui_Manager;
    public GameObject player;
    public GameObject laCam;
    public GameObject shopPanel, indicateur; //poput c'est le truc qui montre ou est le shop et l'input pour l'ouvrir
    //int i = 0;

    bool open, isOpened, popup;









    public bool canShop;
    public int trapToAddNumber;
    public int[] trapUpgradeIndex;
    public GameObject trapToUpgrade;
    public GameObject[] allTraps;
    public float playerSpeed;
    public float playerLife;
    // Start is called before the first frame update

    private void Awake()
    {
        shopPanel.SetActive(false);
        indicateur.SetActive(false);
        inputs = new Inputs();
        inputs.Actions.Shop.performed += ctx => ShopPanelOpenClose();
    }


    private void Start()
    {
        AllTraps();
    }

    public void ShopPanelOpenClose()
    {
        if(popup == true)
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
    
    }

    public void AddTrap()
    {
        ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[this.trapToAddNumber]);
        ShopPanelOpenClose();
        canShop = false;
    }

    public void UpgradeTrap()
    {
        ShopPanelOpenClose();
        canShop = false;
    }

    public void UpStats()
    {

    }

    public void AllTraps()
    {
        for (int i = 0; i < allTraps.Length; i++)
        {
            ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[i]);
        }
        //ShopPanelOpenClose();
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
