using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trap_Inventory : MonoBehaviour
{
    public bool full = false;
    public bool[] checkEmpty;
    public GameObject[] slots;
    public GameObject[] trapsItem;

    public GameObject ui_InventoryPanel;

    public int selectedSlotIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Select Right"))
        {
            SelectRight();
        }
        if(Input.GetButtonDown("Select Left"))
        {
            SelectLeft();
        }
    }

    void SelectRight()//Selectionner l'item de droite
    {
        if(selectedSlotIndex >= slots.Length)
        {
            selectedSlotIndex = 0;
        }
        else
        {
            selectedSlotIndex += 1;
        }
    }
    void SelectLeft()//Selectionner l'item de gauche    
    {
        if (selectedSlotIndex <= 0)
        {
            selectedSlotIndex = slots.Length - 1;
        }
        else
        {
            selectedSlotIndex -= 1;
        }
    }







  
    
    
    
    
    
    
    
    
    
    /*
    public void AddObjectToStuff(GameObject piegeToAdd)
    {
        if (full == false)
        {
            if (upgradeIndex > 0)
            {
                for(int i = 0; i < Slots.Length; i++)
                {
                    if (Slots[i].GetComponent<Traps>().type == piegeToAdd.GetComponent<Trap_Control>().type)
                    {
                        if(Slots[i].GetComponent<Trap_Control>().upgradeIndex < upgradeIndex)
                        {
                            Slots[i] = piegeToAdd;
                            break;
                        }
                        else
                        {
                            Debug.Log("Deja upgrade.");
                        }
                    }
                    else
                    {
                        Debug.Log("Vous n'avez aucun piège qui puisse béféficier de cette upgrade.");
                    }
                }
            }
            else
            {
                for (int i = 0; i < checkEmpty.Length; i++)
                {
                    if(checkEmpty[i] == false)
                    {
                        checkEmpty[i] = true;
                        Slots[i] = piegeToAdd;
                        break;
                    }
                    if(i == checkEmpty.Length - 1)
                    {
                        if (checkEmpty[i] == false)
                        {
                            checkEmpty[i] = true;
                            Slots[i] = piegeToAdd;
                            full = true;
                            break;
                        }
                    }
                }
            }
        }

    }*/
}
