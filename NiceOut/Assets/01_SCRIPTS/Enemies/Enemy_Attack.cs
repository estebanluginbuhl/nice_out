using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    [SerializeField]
    private int damage;
    public float attackRadius, damageCooldown;
    private GameObject attackTarget;
    bool wantsToAttack;

    [SerializeField]
    LayerMask playerLayer = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wantsToAttack = GetComponent<Enemy_Movement>().pathFinding;
        if (damageCooldown < 1)
        {
            damageCooldown += Time.deltaTime;
        }
        if (wantsToAttack)
        {
            Collider[] playerTarget = Physics.OverlapSphere(transform.position + Vector3.up * 2.25f, attackRadius, playerLayer);
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
                if (damageCooldown >= 1)
                {
                    if (attackTarget != false)
                    {
                        Attack(attackTarget);
                    }
                }
            }
        }
    }

    void Attack(GameObject _target)
    {
        if (_target != null)
        {
            _target.GetComponent<StatsPlayer>().DamagePlayer(damage);
            damageCooldown = 0;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 2.25f, attackRadius);
    }
}
