using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Trap_Manager : MonoBehaviour
{
    Inputs inputs;
    bool sell, refill, place, fix, rotating;

    public Canvas ui_Manager;//menus en gros
    public GameObject ui_Inventory;//inventaire
    bool inventoryActive;

    public LayerMask trapped;//Layer de selection des pièges
    public LayerMask floor;//Layer du sol
    public LayerMask player;//Layer du joueur

    public float detectionRadius; //Variables sphere de detection
    public GameObject spherePosition;//Position de la sphere de selection de piège
    public GameObject selectedTrap = null;//Si vous etes devant un piège il sera selectionné et stocké dans cette variable
    GameObject inventorySelection;//Piège que vous voulez poser

    public GameObject forseeTrap;
    public Vector3 colliderCube;
    public Material[] mat;
    MeshFilter mshFlt;
    MeshRenderer mshRnd;
    bool detectCollision;

    float trapOrientation; //orientation du piege a poser
    Vector3 floorInclinaison; //orientation du sol
    Vector3 trapPosition; //position au sol du piège
    float trapRotation; //Le jour oriente le piege comme il le souhaite



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

        mshFlt = forseeTrap.GetComponent<MeshFilter>();
        mshRnd = forseeTrap.GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if(GetComponent<Switch_Mode>().GetPause() == false)
        {
            if (GetComponent<Switch_Mode>().mort == false)
            {
                if (rotating)
                {
                    trapRotation += 1;
                    if (trapRotation >= 360)
                    {
                        trapRotation = 1;
                    }
                }

                //Selection de pièges
                float minDist = Mathf.Infinity;

                Collider[] selectedTraps = Physics.OverlapSphere(spherePosition.transform.position, detectionRadius, trapped); // sphere de detection de piège

                foreach (Collider c in selectedTraps) //Selection de l'emplacement le plus proche
                {
                    float dist = Vector3.Distance(c.gameObject.transform.position, transform.position);
                    if (dist < minDist)
                    {
                        selectedTrap = c.gameObject;
                        minDist = dist;
                    }
                }
                //Déselectionne si le joueur n'est pas devant un piege
                if (selectedTraps.Length == 0)
                {
                    selectedTrap = null;
                }
                if (GetComponent<Switch_Mode>().mode) //Mode de placement de pièges
                {
                    //Active le panneau d'inventaire 
                    if (inventoryActive == false)
                    {
                        ui_Inventory.SetActive(true);
                        inventoryActive = true;
                    }

                    inventorySelection = ui_Manager.GetComponent<Trap_Inventory>().trapsItem[ui_Manager.GetComponent<Trap_Inventory>().selectedSlotIndex];//Selection du piege dans l'inventaire

                    if (inventorySelection != null)
                    {
                        //Forsee
                        Traps trapStats = inventorySelection.GetComponent<Traps>();
                        mshFlt.mesh = trapStats.trapAndUpgrades[0].GetComponent<MeshFilter>().sharedMesh;
                        colliderCube = (trapStats.colliderSize) / 2;

                        Collider[] boxCollider = Physics.OverlapBox(forseeTrap.transform.position + Vector3.up * trapStats.offsetPositions[0], colliderCube, forseeTrap.transform.rotation, floor | trapped);

                        if (boxCollider.Length != 0)
                        {
                            if (detectCollision == false)
                            {
                                detectCollision = true;
                            }
                        }
                        else
                        {
                            if (detectCollision == true)
                            {
                                detectCollision = false;
                            }
                        }

                        RaycastHit hit;
                        if (Physics.Raycast(spherePosition.transform.position, Vector3.down, out hit, Mathf.Infinity, floor))
                        {
                            floorInclinaison = Quaternion.FromToRotation(Vector3.up, hit.normal).eulerAngles;
                            trapOrientation = transform.localEulerAngles.y + trapRotation;
                            trapPosition = hit.point;
                            if (selectedTrap == null)
                            {
                                //appel du placement du pieges
                                if (detectCollision == false)
                                {
                                    if (place)
                                    {
                                        PlaceTrap(inventorySelection);
                                        place = false;
                                    }
                                }
                            }
                        }
                        forseeTrap.transform.rotation = Quaternion.Euler(Vector3.zero);
                        forseeTrap.transform.Rotate(floorInclinaison);
                        forseeTrap.transform.Rotate(new Vector3(0, 1, 0), trapOrientation, Space.Self);
                        forseeTrap.transform.position = trapPosition;
                    }
                }
                else
                {
                    //Desactive le Forsee
                    if (mshFlt.mesh)
                    {
                        mshFlt.mesh = null;
                    }
                    //Ferme l'inventaire
                    if (inventoryActive == true)
                    {
                        ui_Inventory.SetActive(false);
                        inventoryActive = false;
                    }
                }

                //change la couleur du Forsee
                if (detectCollision == true)
                {
                    mshRnd.material = mat[1];
                }
                else
                {
                    mshRnd.material = mat[0];
                }

                //Gestion du piege selectionné
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
        }
    }

    public void PlaceTrap(GameObject _inventorySelection)//Methode de placement du piège selectionné
    {
        Traps trapStats = _inventorySelection.GetComponent<Traps>();
        if (GetComponent<StatsPlayer>().energy >= trapStats.costs[trapStats.upgradeIndex])//vérifie que le joueur a assez d'argent pour payer le piège
        {
            GameObject billy = GameObject.Instantiate(_inventorySelection, trapPosition, Quaternion.identity);
            billy.transform.Rotate(floorInclinaison);
            billy.transform.Rotate(new Vector3(0, 1, 0), trapOrientation, Space.Self);
            billy.GetComponent<Traps>().player = this.gameObject;
            GetComponent<StatsPlayer>().PlayerBuy(trapStats.costs[trapStats.upgradeIndex]);
            return;
        }
    }

    public void RefillTrap() //Methode d'upgrade du piège selectionné
    {
        Traps trapStats = selectedTrap.GetComponent<Traps>(); //get les stats du piege séléctionné

        if(trapStats.ammoPercentage < 1)
        {
            if (GetComponent<StatsPlayer>().energy >= Mathf.RoundToInt((1 - trapStats.ammoPercentage) * trapStats.costs[trapStats.upgradeIndex]))
            {
                Debug.Log("filled");
                GetComponent<StatsPlayer>().PlayerBuy(Mathf.RoundToInt((1 - trapStats.ammoPercentage) * trapStats.costs[trapStats.upgradeIndex]));
                trapStats.ammo = trapStats.fullAmmo[trapStats.upgradeIndex];
                return;
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    public void SellTrap() //Vends ton piège
    {
        Traps trapStats = selectedTrap.GetComponent<Traps>();
        GetComponent<StatsPlayer>().RincePlayer(trapStats.sellCosts[trapStats.upgradeIndex]);
        Destroy(trapStats.child.gameObject);
        Destroy(selectedTrap);
        Debug.Log("sold");
        return;
    }

    public void FixTrap() //Vends ton piège
    {
        Traps trapStats = selectedTrap.GetComponent<Traps>();
        if(trapStats.lifePercentage < 1)
        {
            if (GetComponent<StatsPlayer>().energy >= Mathf.RoundToInt((1 - trapStats.lifePercentage) * trapStats.costs[trapStats.upgradeIndex]))
            {
                Debug.Log("fixed");
                GetComponent<StatsPlayer>().PlayerBuy(Mathf.RoundToInt((1 - trapStats.lifePercentage) * trapStats.costs[trapStats.upgradeIndex]));
                trapStats.life = trapStats.fullLife[trapStats.upgradeIndex];
                return;
            }
            else
            {
                return;
            }
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
        Gizmos.color = Color.green;
        Gizmos.DrawRay(spherePosition.transform.position, Vector3.down);
        Gizmos.color = Color.blue;
        Gizmos.matrix = forseeTrap.transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.up * 1.6f, colliderCube * 2);
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
