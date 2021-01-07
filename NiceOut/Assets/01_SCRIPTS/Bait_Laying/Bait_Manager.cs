using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class Bait_Manager : MonoBehaviour
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
    public LayerMask locationLayer;//Layer du sol
    public LayerMask player;//Layer du joueur
    [SerializeField]
    LayerMask cantTrapLayer;

    [Header("Trap Detector")]
    public float detectionRadius; //Variables sphere de detection
    public GameObject spherePosition;//Position de la sphere de selection de piège
    [HideInInspector]
    public GameObject selectedTrap = null;//Si vous etes devant un piège il sera selectionné et stocké dans cette variable
    GameObject inventorySelection;//Piège que vous voulez poser
    GameObject oldLocation;
    GameObject location;

    [Header("Preview Trap Placement")]
    public float timerAntiSpam;
    float delayAntiSpam;
    public GameObject previewTrap;
    public Vector3 colliderCube;
    public Material[] mat;
    MeshFilter mshFlt;
    MeshRenderer mshRnd;
    bool detectCollision;
    bool isPreviewing;

    float trapOrientation; //orientation du piege a poser
    Vector3 floorInclinaison; //orientation du sol
    Vector3 trapPosition; //position au sol du piège
    float trapRotation; //Le jour oriente le piege comme il le souhaite
    public float rotatingSpeed; //Le jour oriente le piege comme il le souhaite

    private void Awake() //Detection input
    {
        delayAntiSpam = 1;

        inputs = new Inputs();

        inputs.Actions.Place.started += ctx => place = true;
        inputs.Actions.Place.canceled += ctx => place = false;

        if(rotatingRight == false)
        {
            inputs.Actions.RotateRight.started += ctx => rotatingRight = true;
        }

        if (rotatingLeft == false)
        {
            inputs.Actions.RotateLeft.started += ctx => rotatingLeft = true;
        }
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
                    //trapRotation += 1 * rotatingSpeed * Time.deltaTime;
                    trapRotation += 90;
                    if (trapRotation > 360)
                    {
                        //trapRotation = 1 + (1 * rotatingSpeed * Time.deltaTime);
                        trapRotation = 90;
                    }
                    rotatingRight = false;
                }
                if (rotatingLeft)
                {
                    //trapRotation -= 1 * rotatingSpeed * Time.deltaTime;
                    trapRotation -= 90;
                    if (trapRotation < 0)
                    {
                        //trapRotation = 360 - (1 * rotatingSpeed * Time.deltaTime);
                        trapRotation = 270;
                    }
                    rotatingLeft = false;
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
                    inventorySelection = ui_Manager.GetComponent<Bait_Inventory>().trapsItem[ui_Manager.GetComponent<Bait_Inventory>().selectedSlotIndex];//Selection du piege dans l'inventaire

                    if (inventorySelection != null)
                    {
                        StartAllPreview();
                        //Forsee
                        Baits trapStats = inventorySelection.GetComponent<Baits>();
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
                        if (Physics.Raycast(spherePosition.transform.position, Vector3.down, out hit, Mathf.Infinity, locationLayer))
                        {
                            location = hit.collider.gameObject;
                            location.GetComponent<Bait_Location_State>().SetLocationState(true);
                            if (oldLocation != location)
                            {
                                if (oldLocation != null)
                                {
                                    oldLocation.GetComponent<Bait_Location_State>().SetLocationState(false);
                                }
                                oldLocation = location;
                            }
                            //floorInclinaison = Quaternion.FromToRotation(Vector3.up, hit.normal).eulerAngles;
                            floorInclinaison = location.transform.rotation.eulerAngles;
                            //trapOrientation = transform.localEulerAngles.y + trapRotation;
                            //trapPosition = hit.point;
                            trapPosition = location.transform.position;

                            if (selectedTrap == null)
                            {
                                delayAntiSpam += Time.deltaTime;
                                //appel du placement du pieges
                                if (detectCollision == false && delayAntiSpam >= timerAntiSpam)
                                {
                                    if (place)
                                    {
                                        PlaceTrap(inventorySelection, ui_Manager.GetComponent<Bait_Inventory>().selectedSlotIndex, location);
                                        location.GetComponent<Bait_Location_State>().SetOccupation(true);
                                        place = false;
                                        delayAntiSpam = 0;
                                    }
                                }
                            }
                            previewTrap.transform.rotation = Quaternion.Euler(Vector3.zero);
                            previewTrap.transform.Rotate(floorInclinaison);
                            //previewTrap.transform.Rotate(new Vector3(0, 1, 0), trapOrientation, Space.Self);
                            previewTrap.transform.Rotate(new Vector3(0, 1, 0), trapRotation, Space.Self);
                            previewTrap.transform.position = trapPosition;
                        }
                        else
                        {
                            StopAllPreview();
                        }
                    }
                }
                else
                {
                    StopAllPreview();
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

    public void PlaceTrap(GameObject _inventorySelection, int _selectedSlot, GameObject _location)//Methode de placement du piège selectionné
    {
        if (ui_Manager.GetComponent<Bait_Inventory>().nbTrapsInSlot[_selectedSlot] >= 1)//vérifie que le joueur a assez de stcok pour poser le piège
        {
            GameObject bait = GameObject.Instantiate(_inventorySelection, trapPosition, Quaternion.identity);
            bait.transform.Rotate(floorInclinaison);
            //bait.transform.Rotate(new Vector3(0, 1, 0), trapOrientation, Space.Self);
            bait.transform.Rotate(new Vector3(0, 1, 0), trapRotation, Space.Self);
            bait.GetComponent<Baits>().player = this.gameObject;
            bait.GetComponent<Baits>().location = location;
            ui_Manager.GetComponent<Bait_Inventory>().RemoveTraps(_selectedSlot);
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
            Baits trapStats = inventorySelection.GetComponent<Baits>();
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
    void StopAllPreview()
    {
        if (isPreviewing)
        {
            if (oldLocation != null)
            {
                oldLocation.GetComponent<Bait_Location_State>().SetLocationState(false);
                oldLocation = null;
            }
            if (mshFlt)
            {
                mshFlt.gameObject.SetActive(false);
                mshFlt.mesh = null;
            }
            isPreviewing = false;
        }
    }    
    void StartAllPreview()
    {
        if (isPreviewing == false)
        {
            if (mshFlt)
            {
                mshFlt.gameObject.SetActive(true);
            }
            isPreviewing = true;
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
