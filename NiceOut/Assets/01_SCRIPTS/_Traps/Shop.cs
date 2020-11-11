using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Inputs inputs;
    public Canvas ui_Manager;
    GameObject player;
    int i = 0;
    public GameObject[] allTraps;

    public float distancePLayer;
    public GameObject indicateur;
    // Start is called before the first frame update

    private void Awake()
    {
        inputs = new Inputs();
        inputs.Actions.Add.started += ctx => AddTrap();
    }

    void Start()
    {
        player = GameObject.Find("ThirdPersonController");
        if (i < allTraps.Length)
        {
            ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[i]);
            i += 1;
        }
    }

    void AddTrap()
    {
        if (i < allTraps.Length)
        {
            ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[i]);
            i += 1;
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
