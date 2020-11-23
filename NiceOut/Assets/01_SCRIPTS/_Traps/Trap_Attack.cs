using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Attack : MonoBehaviour
{
    //Truc important
    public GameObject parentTrap;
    public int type;

    private void Update()
    {
        
    }

    public void Attack()
    {
        //-1 munition;
        parentTrap.GetComponent<Traps>().ammo -= 1;
        parentTrap.GetComponent<Traps>().ammoPercentage = parentTrap.GetComponent<Traps>().ammo / parentTrap.GetComponent<Traps>().fullAmmo[parentTrap.GetComponent<Traps>().upgradeIndex];
    }
}
