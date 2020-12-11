using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Enemy_Stats : MonoBehaviour
{
    [Header("status bad enm = 2")]
    [Header("status allie = 1")]
    [Header("status neutral = 0")]
    public int status;

    GameObject player;
    bool pause;

    [Header("Affichage")]
    //Visuel
    [SerializeField]
    float distanceMaxPlayer;
    float distancePlayer;
    bool active = false;
    [SerializeField]
    private GameObject healthbar;
    [SerializeField]
    private Image healthImage;
    [SerializeField]
    private Gradient healthColor;
    [SerializeField]
    ParticleSystem convertBadEnemyParticle, convertGoodEnemyParticle, convertedToGood, convertedToBad, parfumed;
    [SerializeField]
    Material[] mats = new Material[3];

    [Header("Stats")]
    [SerializeField]
    private float enmHealth;
    Vector4 healthValues = new Vector4(0, 45, 55, 100);
    float healthPercentage;
    public bool hasDot = false;


    void Start()
    {
        healthbar.SetActive(false);
        healthPercentage = enmHealth / healthValues.w;
        healthImage.color = healthColor.Evaluate(healthPercentage);
        healthImage.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);

        UpdateEnemyState();
    }

    private void Update()
    {
        if(player == null && GetComponent<Enemy_Movement>().pauser != null) 
        {
            pause = GetComponent<Enemy_Movement>().pauser.GetPause();
            player = GetComponent<Enemy_Movement>().pauser.gameObject;
        }
        if(player != null)
        {
            distancePlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distancePlayer > distanceMaxPlayer)
            {
                if (active == true)
                {
                    healthbar.SetActive(false);
                    active = false;
                }
            }
            else
            {
                if (active == false)
                {
                    healthbar.SetActive(true);
                    active = true;
                }
            }
        }
    }

    public void DamageGoodEnemy(int takenDamage)
    {
        enmHealth -= takenDamage;
        if (enmHealth <= healthValues.x)
        {
            enmHealth = healthValues.x;
        }
        healthPercentage = enmHealth / healthValues.w;
        healthImage.color = healthColor.Evaluate(healthPercentage);
        healthImage.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);
        convertGoodEnemyParticle.Play();
        UpdateEnemyState();
    }

    public void DamageBadEnemy(int takenDamage)
    {
        enmHealth += takenDamage;
        if (enmHealth >= healthValues.w)
        {
            enmHealth = healthValues.w;
        }
        healthPercentage = enmHealth / healthValues.w;
        healthImage.color = healthColor.Evaluate(healthPercentage);
        healthImage.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);
        convertBadEnemyParticle.Play();
        UpdateEnemyState();
    }

    void UpdateEnemyState()
    {
        if (enmHealth < healthValues.y)//45 et 0 hostile
        {
            if (status != 2)
            {
                status = 2;
                gameObject.layer = 13;
                GetComponentInChildren<MeshRenderer>().material = mats[2];
                convertedToBad.Play();
            }
        }
        else if (enmHealth > healthValues.z)//55 et 100 allie
        {
            if (status != 1)
            {
                status = 1;
                gameObject.layer = 17;
                GetComponentInChildren<MeshRenderer>().material = mats[1];
                convertedToGood.Play();
                player.GetComponent<StatsPlayer>().RincePlayer(50);//PrixDuMonstre
            }
        }
        else if (enmHealth >= healthValues.y && enmHealth <= healthValues.z)//entre 45 et 55 neutral
        {
            if (status != 0)
            {
                status = 0;
                gameObject.layer = 15;
                GetComponentInChildren<MeshRenderer>().material = mats[0];
            }
        }
    }

    public IEnumerator DamagesOverTime(int _damage, int _duration, float _range, int _index)//DOT parfum
    {
        Debug.Log(_index);
        if (_index != 0)
        {
            parfumed.Play();
            hasDot = true;
            if (_duration > 0)
            {
                if (status == 2 || status == 0)
                {
                    float minDist = Mathf.Infinity;
                    GameObject target = null;

                    Collider[] transferTarget = Physics.OverlapSphere(transform.position + Vector3.up * 2.25f, _range, 13);
                    foreach (Collider c in transferTarget)
                    {
                        if (c.GetComponent<Enemy_Stats>().hasDot == false)
                        {
                            float dist = Vector3.Distance(transform.position, c.transform.position);
                            if (dist <= minDist)
                            {
                                target = c.gameObject;
                                minDist = dist;
                            }
                        }

                    }
                    if (target != null)
                    {
                        _index -= 1;
                        _duration -= 1;
                        _damage -= 1;
                        if (_damage < 1)
                        {
                            _damage = 1;
                        }
                        if (_duration < 1)
                        {
                            _duration = 1;
                        }
                        if (_index < 1)
                        {
                            _duration = 0;
                        }
                        target.GetComponent<Enemy_Movement>().StartCoroutine(DamagesOverTime(_damage, _duration, _range, _index));
                        target = null;
                    }

                    yield return new WaitForSecondsRealtime(1);
                    DamageBadEnemy(_damage);
                    _duration -= 1;
                }
                else
                {
                    _duration = 0;
                }
                Debug.Log(_duration);
            }
            else
            {
                hasDot = false;
                parfumed.Stop();
            }
        }
        else
        {
            hasDot = false;
            parfumed.Stop();
        }
    }
}
