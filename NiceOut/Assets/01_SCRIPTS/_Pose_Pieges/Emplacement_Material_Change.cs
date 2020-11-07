using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emplacement_Material_Change : MonoBehaviour
{
    public Material[] materials;
    GameObject player;
    BoxCollider cld;
    bool matChanged = false;
    bool isOccupied;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ThirdPersonController");
        cld = GetComponent<BoxCollider>();
        ChangeMat(0);
    }

    private void Update()
    {
        if (player.GetComponent<Switch_Mode>().mode)
        {
            if (cld.isTrigger == true)
            {
                cld.isTrigger = false;
                ChangeMat(1);
            }
        }
    }

    public void ChangeMat(int i)
    {
        if (player.GetComponent<Switch_Mode>().mode)
        {

            GetComponent<MeshRenderer>().material = materials[i];

        }
        else
        {
            cld.isTrigger = true;
            GetComponent<MeshRenderer>().material = materials[0];
            matChanged = true;
        }
    }

    public void Unactivate()
    {
        cld.isTrigger = true;
    }

    public void Activate()
    {
        cld.isTrigger = false;
    }
}
