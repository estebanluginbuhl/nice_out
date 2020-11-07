using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Inventory : MonoBehaviour
{
    public bool full = false;
    public bool[] checkEmpty;
    public GameObject[] Slots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void AddObjectToStuff(GameObject piegeToAdd)
    {
        if (full == false)
        {
            if (upgradeIndex > 0)
            {
                for(int i = 0; i < Slots.Length; i++)
                {
                    if (Slots[i].GetComponent<Traps>().type == piegeToAdd.GetComponent<Trap_Control>().type)
                    {
                        if(Slots[i].GetComponent<Trap_Control>().upgradeIndex < upgradeIndex)
                        {
                            Slots[i] = piegeToAdd;
                            break;
                        }
                        else
                        {
                            Debug.Log("Deja upgrade.");
                        }
                    }
                    else
                    {
                        Debug.Log("Vous n'avez aucun piège qui puisse béféficier de cette upgrade.");
                    }
                }
            }
            else
            {
                for (int i = 0; i < checkEmpty.Length; i++)
                {
                    if(checkEmpty[i] == false)
                    {
                        checkEmpty[i] = true;
                        Slots[i] = piegeToAdd;
                        break;
                    }
                    if(i == checkEmpty.Length - 1)
                    {
                        if (checkEmpty[i] == false)
                        {
                            checkEmpty[i] = true;
                            Slots[i] = piegeToAdd;
                            full = true;
                            break;
                        }
                    }
                }
            }
        }

    }*/
}
