using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trap_Inventory : MonoBehaviour
{

    public int nbTrapMax;

    public bool full = false;

    public Image[] slots;
    public GameObject[] trapsItem;

    public GameObject ui_InventoryPanel;
    public Image ui_SelectBox;
    public Image slotImage;
    public float offset; //ecartement en tre les images


    public int nbUsedSlots;
    public int selectedSlotIndex;

    // Start is called before the first frame update
    void Start()
    {
        slots = new Image[nbTrapMax];
        trapsItem = new GameObject[nbTrapMax];
        nbUsedSlots = 0;
        selectedSlotIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(trapsItem[selectedSlotIndex]);
        if(slots[0] != null)//retour position du selecteur
        {
            if (ui_SelectBox.rectTransform.position != slots[selectedSlotIndex].rectTransform.position) 
            {
                ui_SelectBox.rectTransform.position = slots[selectedSlotIndex].rectTransform.position;
            }
        }

        if (Input.GetButtonDown("Select Right"))
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
        if(selectedSlotIndex >= nbUsedSlots - 1)
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
            selectedSlotIndex = nbUsedSlots - 1;
        }
        else
        {
            selectedSlotIndex -= 1;
        }
    }

    public void UpdateInventory(GameObject trap)
    {
        nbUsedSlots += 1;

        Vector2 slotPos = slotImage.rectTransform.position;
        slotPos.x = slotImage.rectTransform.position.x + ((nbUsedSlots - 1) * (slotImage.rectTransform.rect.width + offset));

        slots[nbUsedSlots - 1] = Image.Instantiate(slotImage, slotPos, Quaternion.identity);
        slots[nbUsedSlots - 1].rectTransform.SetParent(ui_InventoryPanel.transform);
        slots[nbUsedSlots - 1].rectTransform.localPosition = slotPos;
        slots[nbUsedSlots - 1].sprite = trap.GetComponent<Traps>().uiImage;
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
