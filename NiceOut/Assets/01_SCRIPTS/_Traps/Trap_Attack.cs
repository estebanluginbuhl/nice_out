using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Attack : MonoBehaviour
{
    //Truc important
    public GameObject parentTrap;
    public int type;

    public float range;
    public int damages;
    [SerializeField]
    LayerMask ennemisMask = -1;

    public float cdAttack;
    float compteurCooldown;

    private void Update()
    {
        if (compteurCooldown <= 0)
        {
            Collider[] ennemis = Physics.OverlapSphere(transform.position, range, ennemisMask);

            if (ennemis.Length != 0)
            {
                foreach(Collider c in ennemis)
                {
                    c.GetComponent<StatEnm>().goodEnm(damages);
                }
                Attack();
            }
        }
        else
            compteurCooldown -= Time.deltaTime;
    }

    public void Attack()
    {
        //-1 munition;
        parentTrap.GetComponent<Traps>().usure -= 1;
        parentTrap.GetComponent<Traps>().UsurePercentage = parentTrap.GetComponent<Traps>().usure / parentTrap.GetComponent<Traps>().fullUsure[parentTrap.GetComponent<Traps>().upgradeIndex];
        compteurCooldown = cdAttack;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
