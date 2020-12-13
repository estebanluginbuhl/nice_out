using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    [SerializeField]
    private int enemyDamages, allyDamages;
    public float enemyAttackRadius, allieAttackRadius, enemyDamageCooldown;
    private GameObject attackTarget;
    bool wantsToAttack;
    bool isDead;
    [SerializeField]
    LayerMask playerLayer = -1;
    [SerializeField]
    LayerMask allyLayer = -1;
    [SerializeField]
    LayerMask firmeLayer = -1;

    // Update is called once per frame
    void Update()
    {
        switch (GetComponent<Enemy_Stats>().status)
        {
            case 0:
                break;

            case 1:
                Collider[] allyTarget = Physics.OverlapSphere(transform.position + Vector3.up * 2.25f, allieAttackRadius, firmeLayer);
                if (allyTarget.Length == 0)
                {
                    return;
                }
                else
                {
                    if(isDead == false)
                    {
                        AllieAttack(allyTarget[0].gameObject);
                    }
                }
                break;

            case 2:
                wantsToAttack = GetComponent<Enemy_Movement>().pathFinding;
                if (wantsToAttack)//si l'ennemi foxus le joueur il attaque que le joueur 
                {
                    if (enemyDamageCooldown < 1)
                    {
                        enemyDamageCooldown += Time.deltaTime;
                    }
                    Collider[] enemyTarget = Physics.OverlapSphere(transform.position + Vector3.up * 2.25f, enemyAttackRadius, playerLayer);
                    if (enemyTarget.Length == 0)
                    {
                        return;
                    }
                    else
                    {
                        foreach (Collider c in enemyTarget)
                        {
                            attackTarget = c.gameObject;
                        }
                        if (enemyDamageCooldown >= 1)
                        {
                            if (attackTarget != false)
                            {
                                EnemyAttackPlayer(attackTarget);
                            }
                        }
                    }
                }
                else//sinon il attaque les entités allié 
                {
                    if (enemyDamageCooldown < 1)
                    {
                        enemyDamageCooldown += Time.deltaTime;
                    }
                    Collider[] enemyTarget = Physics.OverlapSphere(transform.position + Vector3.up * 2.25f, enemyAttackRadius, allyLayer);
                    if (enemyTarget.Length == 0)
                    {
                        return;
                    }
                    else
                    {
                        foreach (Collider c in enemyTarget)
                        {
                            attackTarget = c.gameObject;
                        }
                        if (enemyDamageCooldown >= 1)
                        {
                            if (attackTarget != false)
                            {
                                EnemyAttackAlly(attackTarget);
                            }
                        }
                    }
                }
                break;
        }
    }

    void EnemyAttackPlayer(GameObject _enemyTarget)
    {
        if (_enemyTarget != null)
        {
            _enemyTarget.GetComponent<Player_Stats>().DamagePlayer(enemyDamages);
            enemyDamageCooldown = 0;
        }
    }    
    void EnemyAttackAlly(GameObject _enemyTarget)
    {
        if (_enemyTarget != null)
        {
            _enemyTarget.GetComponent<Enemy_Stats>().DamageGoodEnemy(enemyDamages);
            enemyDamageCooldown = 0;
        }
    }

    void AllieAttack(GameObject _allieTarget)
    {
        if (_allieTarget != null)
        {
            _allieTarget.GetComponent<Firme_Stats>().DamageFirme(allyDamages);
            isDead = true;
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 2.25f, enemyAttackRadius);
    }
}
