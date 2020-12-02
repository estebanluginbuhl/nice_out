﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Trap_Inventory : MonoBehaviour
{

    Inputs inputs;
    public int nbTrapMax;


    [HideInInspector]
    public Image[] slots;
    [HideInInspector]
    public TextMeshProUGUI[] number;
    [HideInInspector]
    public GameObject[] trapsItem;
    [HideInInspector]
    public int[] nbTrapsInSlot;
    [SerializeField]
    int nbStartTraps;

    public Image ui_InventoryPanel;
    public Image ui_SelectBox;
    public Image slotImage;
    public TextMeshProUGUI trapNumberText;
    public float offsetX; //ecartement entre les images
    public float offsetY; //hauteur images
    Vector3 scrolling;
    [HideInInspector]
    public int nbUsedSlots;
    [HideInInspector]
    public int selectedSlotIndex;

    // Start is called before the first frame update
    void Awake()
    {
        inputs = new Inputs();

        inputs.Actions.MouseScroll.performed += ctx => scrolling = ctx.ReadValue<Vector2>();
        inputs.Actions.MouseScroll.canceled += ctx => scrolling = Vector2.zero;

        slots = new Image[nbTrapMax];
        number = new TextMeshProUGUI[nbTrapMax];
        trapsItem = new GameObject[nbTrapMax];
        nbTrapsInSlot = new int[nbTrapMax];
        nbUsedSlots = 0;
        selectedSlotIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (scrolling.y < 0)
        {
            SelectLeft();
        }
        if (scrolling.y > 0)
        {
            SelectRight();
        }
    }

    void SelectRight()//Selectionner l'item de droite
    {
        selectedSlotIndex += 1;
        if ((selectedSlotIndex) > 4)
        {
            selectedSlotIndex = 0;
        }
        for (int i = 0; i < nbTrapMax + 1; i++)
        {
            if (slots[selectedSlotIndex] == null)
            {
                if ((selectedSlotIndex) > 4)
                {
                    selectedSlotIndex = 0;
                }
                else
                {
                    selectedSlotIndex += 1;
                    if ((selectedSlotIndex) > 4)
                    {
                        selectedSlotIndex = 0;
                    }
                }
            }
            else
            {
                if (nbUsedSlots != 0)//retour position du selecteur
                {
                    if ((selectedSlotIndex) > 4)
                    {
                        selectedSlotIndex = 0;
                    }
                    ui_SelectBox.rectTransform.position = slots[selectedSlotIndex].rectTransform.position;
                }
                break;
            }
        }
    }
    void SelectLeft()//Selectionner l'item de gauche    
    {
        selectedSlotIndex -= 1;
        if ((selectedSlotIndex) < 0)
        {
            selectedSlotIndex = 4;
        }
        for (int i = 0; i < nbTrapMax + 1; i++)
        {
            if (slots[selectedSlotIndex] == null)
            {
                if ((selectedSlotIndex) < 0)
                {
                    selectedSlotIndex = 4;
                }
                else
                {
                    selectedSlotIndex -= 1;
                    if ((selectedSlotIndex) < 0)
                    {
                        selectedSlotIndex = 4;
                    }
                }
            }
            else
            {
                if (nbUsedSlots != 0)//retour position du selecteur
                {
                    if ((selectedSlotIndex) < 0)
                    {
                        selectedSlotIndex = 4;
                    }
                    ui_SelectBox.rectTransform.position = slots[selectedSlotIndex].rectTransform.position;
                }
                break;
            }
        }
    }

    public void UpdateInventory(GameObject trap, int _Index)
    {
        nbUsedSlots += 1;
        Vector2 slotPos = slotImage.rectTransform.position;
        Vector2 numberPos = trapNumberText.rectTransform.position;

        float l = slotImage.rectTransform.rect.width; //largeur d'un slot

        ui_InventoryPanel.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (offsetX * (nbUsedSlots + 1)) + (l * nbUsedSlots)); //Set la largeur du panel inventaire en fonction du nb de slots

        float inventoryWidth = ui_InventoryPanel.rectTransform.rect.width; //Get la largeur du panel inventaire

        slotPos.y = offsetY;
        numberPos.y = offsetY - 80;

        slots[_Index] = Image.Instantiate(slotImage, slotPos, Quaternion.identity);
        slots[_Index].rectTransform.SetParent(ui_InventoryPanel.GetComponentInChildren<Transform>());
        slots[_Index].rectTransform.localScale = Vector3.one;
        slots[_Index].sprite = trap.GetComponent<Traps>().ui_Image[0];

        number[_Index] = TextMeshProUGUI.Instantiate(trapNumberText, numberPos, Quaternion.identity);
        number[_Index].rectTransform.SetParent(ui_InventoryPanel.GetComponentInChildren<Transform>());
        number[_Index].rectTransform.localScale = Vector3.one;
        number[_Index].text = nbStartTraps.ToString();

        trapsItem[_Index] = trap;
        nbTrapsInSlot[_Index] = nbStartTraps;

        int slotJump = 0;

        for (int i = 0; i < nbTrapMax; i++)
        {
            if(slots[i] != null)
            {
                slotPos.x = (-inventoryWidth / 2) + ((offsetX + (l / 2)) + ((offsetX + l) * (i - slotJump)));
                numberPos.x = (-inventoryWidth / 2) + ((offsetX + (l / 2)) + ((offsetX + l) * (i - slotJump)));
                slots[i].rectTransform.localPosition = slotPos;
                number[i].rectTransform.localPosition = numberPos;
            }
            else
            {
                slotJump += 1;
            }
        }
        selectedSlotIndex = _Index;
        SelectRight();
    }

    public void UpgradeTrapInventory(int _SlotIndex, int _UpIndex)
    {
        slots[_SlotIndex].sprite = trapsItem[_SlotIndex].GetComponent<Traps>().ui_Image[_UpIndex];
    }

    public void AddTraps(int _SlotIndex)
    {
        nbTrapsInSlot[_SlotIndex] += 1;
        number[_SlotIndex].text = nbTrapsInSlot[_SlotIndex].ToString();
    }
    public void RemoveTraps(int _SlotIndex)
    {
        nbTrapsInSlot[_SlotIndex] -= 1;
        number[_SlotIndex].text = nbTrapsInSlot[_SlotIndex].ToString();
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