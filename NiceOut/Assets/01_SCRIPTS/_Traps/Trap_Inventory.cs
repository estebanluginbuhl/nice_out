using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Trap_Inventory : MonoBehaviour
{

    Inputs inputs;
    public int nbTrapMax;

    public bool full = false;

    public Image[] slots;
    public GameObject[] trapsItem;

    public Image ui_InventoryPanel;
    public Image ui_SelectBox;
    public Image slotImage;
    public float offsetX; //ecartement entre les images
    public float offsetY; //hauteur images


    public int nbUsedSlots;
    public int selectedSlotIndex;

    // Start is called before the first frame update
    void Awake()
    {
        slots = new Image[nbTrapMax];
        trapsItem = new GameObject[nbTrapMax];
        nbUsedSlots = 0;
        selectedSlotIndex = 0;

        inputs = new Inputs();

        inputs.Actions.SelectLeft.performed += ctx => SelectLeft();
        inputs.Actions.SelectRight.started += ctx => SelectRight();
    }

    // Update is called once per frame
    void Update()
    {
        if (slots[0] != null)//retour position du selecteur
        {
            if (ui_SelectBox.rectTransform.position != slots[selectedSlotIndex].rectTransform.position)
            {
                ui_SelectBox.rectTransform.position = slots[selectedSlotIndex].rectTransform.position;
            }
        }
    }

    void SelectRight()//Selectionner l'item de droite
    {
        if (selectedSlotIndex >= nbUsedSlots - 1)
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

        float l = slotImage.rectTransform.rect.width; //largeur d'un slot

        ui_InventoryPanel.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (offsetX * (nbUsedSlots + 1)) + (l * nbUsedSlots)); //Set la largeur du panel inventaire en fonction du nb de slots

        float inventoryWidth = ui_InventoryPanel.rectTransform.rect.width; //Get la largeur du panel inventaire

        slotPos.y = offsetY;

        slots[nbUsedSlots - 1] = Image.Instantiate(slotImage, slotPos, Quaternion.identity);
        slots[nbUsedSlots - 1].rectTransform.SetParent(ui_InventoryPanel.transform);
        slots[nbUsedSlots - 1].rectTransform.localScale = Vector3.one;
        slots[nbUsedSlots - 1].sprite = trap.GetComponent<Traps>().uiImage;
        trapsItem[nbUsedSlots - 1] = trap;

        for(int i = 0; i < nbUsedSlots; i++)
        {
            slotPos.x = (-inventoryWidth / 2) + ((offsetX + (l / 2)) + ((offsetX + l) * i));
            slots[i].rectTransform.localPosition = slotPos;
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