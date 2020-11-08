using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emplacement_Material_Change : MonoBehaviour
{
    public Material[] materials;//0 = inactif, 1 = acitf, 2 = selected, 3 = occupied , 4 = occupied Selected
    GameObject player;//joueur
    BoxCollider cld;//collider de l'emplacement
    MeshRenderer rnd;//collider de l'emplacement
    public bool isOccupied = false;//if there is already a trap on it
    public GameObject placedTrap;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ThirdPersonController");
        cld = GetComponent<BoxCollider>();
        rnd = GetComponent<MeshRenderer>();
        ChangeMat(0);
    }

    private void Update()
    {
        if (player.GetComponent<Switch_Mode>().mode)
        {
            if (cld.isTrigger == true)
            {
                cld.isTrigger = false;

                if (isOccupied)
                {
                    ChangeMat(3);
                    cld.isTrigger = true;
                }
                else
                {
                    ChangeMat(1);
                }
            }
        }
        else
        {
            ChangeMat(0);
        }
    }

    public void ChangeMat(int i)
    {
        if (player.GetComponent<Switch_Mode>().mode)
        {
            rnd.material = materials[i];
        }
        else
        {
            GetComponent<MeshRenderer>().material = materials[0];
            cld.isTrigger = true;
        }
    }
}
