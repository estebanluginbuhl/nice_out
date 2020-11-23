using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flaque_Petrole : MonoBehaviour
{
    public float damage, gizmosRadius, getHealth, damageCooldown;
    public Color gizmosColor;
    public LayerMask trapDetectionLayer, playerDetectionLayer;

    private Collider[] trapTarget, playerTarget;

    void Update()
    {
        trapTarget = Physics.OverlapSphere(transform.position, gizmosRadius, trapDetectionLayer);
        playerTarget = Physics.OverlapSphere(transform.position, gizmosRadius, playerDetectionLayer);

        if (trapTarget == null && playerTarget == null)
        {
            return;
        }
        else
        {
            Damage();
            damageCooldown += Time.deltaTime;
        }
    }

    void Damage()
    {
        if (damageCooldown >= 1)
        {
            damageCooldown = 0;
            if (trapTarget.Length != 0)
            {
                Debug.Log("damage trap");
                trapTarget[0].GetComponent<Traps>().DamageTrap(Mathf.RoundToInt(damage));
            }
            else if (playerTarget.Length != 0)
            {
                Debug.Log("damage player");
                playerTarget[0].GetComponent<StatsPlayer>().health -= damage;
            }
            else return;
        }
        return;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, gizmosRadius);
    }
}