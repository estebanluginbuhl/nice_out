using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatEnm : MonoBehaviour
{
    public float damage, gizmosRadius, getHealth, damageCooldown, enmHealth;
    public Color gizmosColor;

    public LayerMask playerDetectionLayer;

    private Collider[] playerTarget;
    private GameObject attackTarget;

    void Update()
    {
        playerTarget = Physics.OverlapSphere(transform.position, gizmosRadius, playerDetectionLayer);

        if (playerTarget.Length == 0)
        {
            return;
        }
        else
        {
            foreach (Collider c in playerTarget)
            {
                attackTarget = c.gameObject;
            }
            Damage(attackTarget);
        }
    }

    void Damage(GameObject _target)
    {
        if (damageCooldown >= 1)
        {
            if (_target)
            {
                //Debug.Log("damage player");
                _target.GetComponent<StatsPlayer>().health -= damage;
                damageCooldown = 0;
            }
            else return;
        }
        else
        {
            damageCooldown += Time.deltaTime;
        }
    }

    public void goodEnm(int takenDamage)
    {
        enmHealth += takenDamage;
    }

    public void badEnm(int takenDamage)
    {
        enmHealth -= takenDamage;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, gizmosRadius);
    }
}