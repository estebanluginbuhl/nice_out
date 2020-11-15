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
    public GameObject[] allTraps;
    bool open, isOpened, popup;
    public float distancePLayer;
    public float distancePlayerShop;
    // Start is called before the first frame update

    private void Awake()
    {
        shopPanel.SetActive(false);
        indicateur.SetActive(false);
        inputs = new Inputs();
        inputs.Actions.Shop.performed += ctx => ShopPanelOpenClose();
    }

    private void Update()
    {
        //Afficher qu'on est en range du shop et aussi l'input pour l'ouvrir
        distancePlayerShop = Vector3.Distance(transform.position, player.transform.position);
        if (distancePlayerShop <= distancePLayer)
        {
            if (popup == false)
            {
                indicateur.SetActive(true);
                popup = true;
            }

        }
        else
        {
            if (popup == true)
            {
                indicateur.SetActive(false);
                popup = false;
            }
        }

        if (indicateur != null)
        {
            Vector3 playerPosNoHeight = laCam.transform.position;
            indicateur.transform.LookAt(playerPosNoHeight);
        }
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

    public void AddTrap0()
    {
        ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[0]);
        ShopPanelOpenClose();
    }
    public void AddTrap1()
    {
        ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[1]);
        ShopPanelOpenClose();
    }
    public void AddTrap2()
    {
        ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[2]);
        ShopPanelOpenClose();
    }
    public void AddTrap3()
    {
        ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[3]);
        ShopPanelOpenClose();
    }
    public void AddTrap4()
    {
        ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[4]);
        ShopPanelOpenClose();
    }

    public void AllTraps()
    {
        for (int i = 0; i < 5; i++)
        {
            ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[i]);
        }
        ShopPanelOpenClose();
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
