using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Trap_Manager : MonoBehaviour
{
    Inputs inputs;
    public LayerMask trapped;//Layer de selection des pièges
    public LayerMask floor;//Layer du sol
    public LayerMask player;//Layer du joueur
    public float detectionRadius; //Variables sphere de detection
    public GameObject spherePosition;//Position de la sphere de selection de piège
    public GameObject selectedTrap = null;//Si vous etes devant un piège il sera selectionné et stocké dans cette variable
    public GameObject oldSelection = null;//le dernier piège selectionné est stocké dans cette variable
    public GameObject inventorySelection;//Piège que vous voulez poser
    public Canvas ui_Manager;//menus en gros
    public GameObject ui_Inventory;//inventaire
    bool inventoryActive;

    public GameObject forseeTrap;

    public float trapOrientation; //orientation du piege a poser
    public Vector3 floorInclinaison; //orientation du sol
    public Vector3 trapPosition; //position au sol du piège
    public float trapRotation; //Le jour oriente le piege comme il le souhaite

    bool sell, refill, place, fix, rotating;

    private void Awake() //Detection input
    {
        inputs = new Inputs();

        inputs.Actions.Sell.started += ctx => sell = true;
        inputs.Actions.Sell.canceled += ctx => sell = false;
        inputs.Actions.Place.started += ctx => place = true;
        inputs.Actions.Place.canceled += ctx => place = false;
        inputs.Actions.Fix.started += ctx => fix = true;
        inputs.Actions.Fix.canceled += ctx => fix = false;
        inputs.Actions.Refill.started += ctx => refill = true;
        inputs.Actions.Refill.canceled += ctx => refill = false;
        inputs.Actions.Rotate.started += ctx => rotating = true;
        inputs.Actions.Rotate.canceled += ctx => rotating = false;
    }

    private void Start()
    {
        ui_Inventory.SetActive(false);
        inventoryActive = false;
    }

    void Update()
    {
      
        if(rotating)
        {
            trapRotation += 1;
            if(trapRotation >= 360)
            {
                trapRotation = 1;
            }
        }

        //Selection de pièges
        float minDist = Mathf.Infinity;

        Collider[] selectedTraps = Physics.OverlapSphere(spherePosition.transform.position, detectionRadius, trapped); // sphere de detection de piège

        oldSelection = selectedTrap;

        foreach (Collider c in selectedTraps) //Selection de l'emplacement le plus proche
        {
            float dist = Vector3.Distance(c.gameObject.transform.position, transform.position);
            if (dist < minDist)
            {
                selectedTrap = c.gameObject;
                minDist = dist;
            }
        }

        if (GetComponent<Switch_Mode>().mode) //Mode de placement de pièges
        {
            if (inventoryActive == false)
            {
                ui_Inventory.SetActive(true);
                inventoryActive = true;
            }

            inventorySelection = ui_Manager.GetComponent<Trap_Inventory>().trapsItem[ui_Manager.GetComponent<Trap_Inventory>().selectedSlotIndex];//Selection du piege dans l'inventaire

            if (selectedTraps.Length == 0)
            {
                selectedTrap = null;
            }

            if (inventorySelection != null) //appel du placement et de l'amelioration des pieges
            {
                RaycastHit hit;
                if (Physics.Raycast(spherePosition.transform.position, Vector3.down, out hit, Mathf.Infinity, floor))
                {
                    floorInclinaison = Quaternion.FromToRotation(Vector3.up, hit.normal).eulerAngles;
                    trapOrientation = transform.localEulerAngles.y + trapRotation;
                    trapPosition = hit.point;
                    if(selectedTrap == null)
                    {
                        if (forseeTrap.GetComponent<Trap_Forsee>().detectCollision == false)
                        {
                            if (place)
                            {
                                PlaceTrap(inventorySelection);
                                place = false;
                            }
                        }
                    }
                }
            }
        }
        else //Ferme l'inventaire
        {
            if (inventoryActive == true)
            {
                ui_Inventory.SetActive(false);
                inventoryActive = false;
            }
        }

        if (selectedTrap != null)
        {
            if (refill)
            {
                RefillTrap();
                refill = false;
            }

            if (fix)
            {
                FixTrap();
                fix = false;
            }

            if (sell)
            {
                SellTrap();
                sell = false;
            }
        }
    }

    public void PlaceTrap(GameObject _inventorySelection)//Methode de placement du piège selectionné
    {
        Traps trapStats = _inventorySelection.GetComponent<Traps>();
        if (GetComponent<StatsPlayer>().gold >= trapStats.costs)//vérifie que le joueur a assez d'argent pour payer le piège
        {
            GameObject billy = GameObject.Instantiate(_inventorySelection, trapPosition, Quaternion.identity);
            billy.transform.Rotate(floorInclinaison);
            billy.transform.Rotate(new Vector3(0, 1, 0), trapOrientation, Space.Self);
            GetComponent<StatsPlayer>().gold -= trapStats.costs;
        }
    }

    public void RefillTrap() //Methode d'upgrade du piège selectionné
    {
        Traps trapStats = selectedTrap.GetComponent<Traps>(); //get les stats du piege séléctionné
        if (GetComponent<StatsPlayer>().gold >= Mathf.RoundToInt(trapStats.ammoPercentage * trapStats.costs))
        {

        }
        else
        {
            return;
        }
    }

    public void SellTrap() //Vends ton piège
    {
        Traps trapStats = selectedTrap.GetComponent<Traps>();
    }

    public void FixTrap() //Vends ton piège
    {
        Traps trapStats = selectedTrap.GetComponent<Traps>();
        if (GetComponent<StatsPlayer>().gold >= Mathf.RoundToInt(trapStats.lifePercentage * trapStats.costs))
        {

        }
        else
        {
            return;
        }
    }

    private void OnDrawGizmos() //Afficher la sphere de detection dans la scene
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition.transform.position, detectionRadius);
        Gizmos.DrawRay(spherePosition.transform.position, Vector3.down);
    }

    //Pas touche c'est pour les inputs
    private void OnEnable()
    {
        inputs.Actions.Enable();
    }
    private void OnDisable()
    {
        inputs.Actions.Disable();
    }
}
