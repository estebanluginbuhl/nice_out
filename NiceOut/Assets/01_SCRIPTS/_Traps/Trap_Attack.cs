using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Attack : MonoBehaviour
{
    //Truc important
    public GameObject parentTrap;
    public int type;

    public float range;
    public float damages;
    [SerializeField]
    LayerMask ennemisMask = -1;

    public float cdAttack;
    float compteurCooldown;

    private void Update()
    {
        if (compteurCooldown <= 0)
        {
            Collider[] ennemis = Physics.OverlapSphere(transform.position, range, ennemisMask);

            if (ennemis != null)
            {
                foreach(Collider c in ennemis)
                {
                   // c.GetComponent<>().goodEnm();
                }
            }
        }
        else
            compteurCooldown -= Time.deltaTime;
    }

    public void Attack()
    {
        //-1 munition;
        parentTrap.GetComponent<Traps>().ammo -= 1;
        parentTrap.GetComponent<Traps>().ammoPercentage = parentTrap.GetComponent<Traps>().ammo / parentTrap.GetComponent<Traps>().fullAmmo[parentTrap.GetComponent<Traps>().upgradeIndex];
        compteurCooldown = cdAttack;
    }
}
