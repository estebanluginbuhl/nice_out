using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Canvas ui_Manager;
    GameObject player;
    int i = 0;
    public GameObject[] allTraps;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ThirdPersonController");
        if (i < allTraps.Length)
        {
            ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[i]);
            i += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(i < allTraps.Length)
            {
                ui_Manager.GetComponent<Trap_Inventory>().UpdateInventory(allTraps[i]);
                i += 1;
            }
        }
    }
}
