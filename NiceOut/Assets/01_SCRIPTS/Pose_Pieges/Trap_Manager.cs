using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class Trap_Manager : MonoBehaviour
{
    Inputs inputs;
    bool place, rotatingRight, rotatingLeft;

    [Header("UI Elements")]
    public Canvas ui_Manager;//menus en gros
    public Image ui_Inventory;//inventaire
    public GameObject[] ui_Inputs_Infos;//inventaire
    public GameObject ui_trapDescription;//inventaire
    public GameObject ui_trapInfos;//inventaire
    bool inventoryActive;
    [SerializeField]
    Vector2 inventoryTransforms = new Vector2(0, -170);
    [SerializeField, Range(0f, 1f)]
    float smoothingOpening;

    bool opening = false;
    bool closing = false;

    [Header("LayerMasks")]
    public LayerMask trapped;//Layer de selection des pièges
    public LayerMask floor;//Layer du sol
    public LayerMask player;//Layer du joueur
    [SerializeField]
    LayerMask cantTrapLayer;

    [Header("Trap Detector")]
    public float detectionRadius; //Variables sphere de detection
    public GameObject spherePosition;//Position de la sphere de selection de piège
    [HideInInspector]
    public GameObject selectedTrap = null;//Si vous etes devant un piège il sera selectionné et stocké dans cette variable
    GameObject inventorySelection;//Piège que vous voulez poser

    [Header("Preview Trap Placement")]
    public GameObject previewTrap;
    public Vector3 colliderCube;
    public Material[] mat;
    MeshFilter mshFlt;
    MeshRenderer mshRnd;
    bool detectCollision;

    float trapOrientation; //orientation du piege a poser
    Vector3 floorInclinaison; //orientation du sol
    Vector3 trapPosition; //position au sol du piège
    float trapRotation; //Le jour oriente le piege comme il le souhaite
    public float rotatingSpeed; //Le jour oriente le piege comme il le souhaite

    private void Awake() //Detection input
    {
        inputs = new Inputs();

        inputs.Actions.Place.started += ctx => place = true;
        inputs.Actions.Place.canceled += ctx => place = false;

        inputs.Actions.RotateRight.started += ctx => rotatingRight = true;
        inputs.Actions.RotateRight.canceled += ctx => rotatingRight = false;
        inputs.Actions.RotateLeft.started += ctx => rotatingLeft = true;
        inputs.Actions.RotateLeft.canceled += ctx => rotatingLeft = false;
    }

    private void Start()
    {
        ui_Inventory.rectTransform.position = new Vector3(ui_Inventory.rectTransform.position.x, inventoryTransforms.y, ui_Inventory.rectTransform.position.z);
        foreach (GameObject g in ui_Inputs_Infos)
        {
            g.SetActive(false);
        }

        ui_trapDescription.SetActive(false);
        ui_trapInfos.SetActive(false);
        inventoryActive = false;

        mshFlt = previewTrap.GetComponent<MeshFilter>();
        mshRnd = previewTrap.GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if(GetComponent<Switch_Mode>().GetPause() == false)
        {
            if (GetComponent<Switch_Mode>().mort == false)
            {
                if (rotatingRight)
                {
                    trapRotation += 1 * rotatingSpeed * Time.deltaTime;
                    if (trapRotation >= 360)
                    {
                        trapRotation = 1 + (1 * rotatingSpeed * Time.deltaTime);
                    }
                }
                if (rotatingLeft)
                {
                    trapRotation -= 1 * rotatingSpeed * Time.deltaTime;
                    if (trapRotation <= 0)
                    {
                        trapRotation = 360 - (1 * rotatingSpeed * Time.deltaTime);
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
                    ActivateInventory();
                    inventorySelection = ui_Manager.GetComponent<Trap_Inventory>().trapsItem[ui_Manager.GetComponent<Trap_Inventory>().selectedSlotIndex];//Selection du piege dans l'inventaire

                    if (inventorySelection != null)
                    {
                        //Forsee
                        Traps trapStats = inventorySelection.GetComponent<Traps>();
                        mshFlt.mesh = trapStats.trapAndUpgrades[0].GetComponent<MeshFilter>().sharedMesh;
                        colliderCube = (trapStats.colliderSize) / 2;

                        Collider[] boxCollider = Physics.OverlapBox(previewTrap.transform.position + (Vector3.up * colliderCube.y + Vector3.up * trapStats.offsetPositions[0]), colliderCube, previewTrap.transform.rotation, cantTrapLayer);

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
                                        PlaceTrap(inventorySelection, ui_Manager.GetComponent<Trap_Inventory>().selectedSlotIndex);
                                        place = false;
                                    }
                                }
                            }
                        }
                        previewTrap.transform.rotation = Quaternion.Euler(Vector3.zero);
                        previewTrap.transform.Rotate(floorInclinaison);
                        previewTrap.transform.Rotate(new Vector3(0, 1, 0), trapOrientation, Space.Self);
                        previewTrap.transform.position = trapPosition;
                    }
                }
                else
                {
                    //Desactive le Preview
                    if (mshFlt.mesh)
                    {
                        mshFlt.mesh = null;
                    }
                    UnactivateInventory();
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
            }
        }
    }

    public void PlaceTrap(GameObject _inventorySelection, int _selectedSlot)//Methode de placement du piège selectionné
    {

        if (ui_Manager.GetComponent<Trap_Inventory>().nbTrapsInSlot[_selectedSlot] >= 1)//vérifie que le joueur a assez de stcok pour poser le piège
        {
            GameObject trap = GameObject.Instantiate(_inventorySelection, trapPosition, Quaternion.identity);
            trap.transform.Rotate(floorInclinaison);
            trap.transform.Rotate(new Vector3(0, 1, 0), trapOrientation, Space.Self);
            trap.GetComponent<Traps>().player = this.gameObject;
            ui_Manager.GetComponent<Trap_Inventory>().RemoveTraps(_selectedSlot);
            return;
        }
        else
            mshRnd.material = mat[1];
    }
    private void OnDrawGizmos() //Afficher la sphere de detection dans la scene
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition.transform.position, detectionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(spherePosition.transform.position, Vector3.down);
        if (inventorySelection == true)
        {
            Traps trapStats = inventorySelection.GetComponent<Traps>();
            Gizmos.color = Color.blue;
            Gizmos.matrix = previewTrap.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.up * colliderCube.y + Vector3.up * trapStats.offsetPositions[0], colliderCube * 2);
        }
    }

    void ActivateInventory()
    {
        if (inventoryActive == false)
        {
            Vector2 desiredPos = new Vector2(ui_Inventory.rectTransform.anchoredPosition.x, inventoryTransforms.x);
            ui_Inventory.rectTransform.anchoredPosition = desiredPos;
            foreach (GameObject g in ui_Inputs_Infos)
            {
                g.SetActive(true);
            }
            ui_trapDescription.SetActive(true);
            ui_trapInfos.SetActive(true);
            inventoryActive = true;
        }
    }
    void UnactivateInventory()
    {
        if (inventoryActive == true)
        {
            Vector2 desiredPos = new Vector2(ui_Inventory.rectTransform.anchoredPosition.x, inventoryTransforms.y);
            ui_Inventory.rectTransform.anchoredPosition = desiredPos;
            foreach (GameObject g in ui_Inputs_Infos)
            {
                g.SetActive(false);
            }
            ui_trapDescription.SetActive(false);
            ui_trapInfos.SetActive(false);
            inventoryActive = false;
        }
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
