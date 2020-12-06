using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatEnm : MonoBehaviour
{
    public float gizmosRadius, damageCooldown;
    public int damage;
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
            if(damageCooldown >= 1)
            {
                Damage(attackTarget);
            }
        }
        if (damageCooldown < 1)
        {
            damageCooldown += Time.deltaTime;
        }
    }

    void Damage(GameObject _target)
    {
        if (_target != null)
        {
            _target.GetComponent<StatsPlayer>().DamagePlayer(damage);
            damageCooldown = 0;
        }
        else return;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, gizmosRadius);
    }
}