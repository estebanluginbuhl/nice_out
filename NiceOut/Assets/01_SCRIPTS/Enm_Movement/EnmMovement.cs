using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class EnmMovement : MonoBehaviour
{
    Switch_Mode pauser;

    public bool pathFinding;
    [Header("status bad enm = 2")]
    [Header("status allie = 1")]
    [Header("status neutral = 0")]
    public int status;
    public Transform enmTransform;
    public LayerMask playerDetectionLayer;
    public float enmSpeed, targetTreshold, targetTresholdNeutral, gizmo1Radius, resetTransformPicker, maxWaitingTime;
    public Color gizmo1Color;

    [HideInInspector]
    public NavMeshAgent enmNavMesh;
    [HideInInspector]
    public Transform target;
    NavMeshHit navHit;
    GameObject player;
    GameObject statsPlayer;
    GameObject choosenObjective = null;
    private int nodeIndex = 0;
    [SerializeField]
    private float enmHealth;
    private Vector3 randomPosition;
    public bool start = true;

    Vector4 healthValues = new Vector4(0, 45, 55, 100);
    private GameObject[] firmeTarget;

    private float randomTransformPickerTimer, delayBeforeGo;

    [Header("Attaque")]
    [SerializeField]
    private int damage;
    public float attackRadius, damageCooldown;
    public Color attackRadiusDebugColor;
    private GameObject attackTarget;

    [Header("Controles")]
    //Trap Influence
    public bool isAttracted;
    public Transform attractTarget;

    public bool isSlowed, isFastened, isStunned, hasDot;
    public float modifiedSpeed;

    [Header("Affichage")]
    //Visuel
    [SerializeField]
    private Image healthImage;
    [SerializeField]
    private Gradient healthColor;
    [SerializeField]
    private TextMeshProUGUI stateText;
    [SerializeField]
    ParticleSystem convertBadEnemyParticle, convertGoodEnemyParticle, convertedToGood, convertedToBad, parfumed;
    [SerializeField]
    Material[] mats = new Material[3];
    float healthPercentage;

    void Start()
    {
        //pauser = player.GetComponent<Switch_Mode>();
        enmTransform = gameObject.transform;
        enmNavMesh = GetComponent<NavMeshAgent>();
        enmNavMesh.speed = enmSpeed;
        target = PathNode.nodeTransform[0];

        randomPosition = Random.insideUnitSphere * gizmo1Radius;

        UpdateEnemyState();

        healthPercentage = enmHealth / healthValues.w;
        healthImage.color = healthColor.Evaluate(healthPercentage);
        healthImage.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);
        statsPlayer = GameObject.Find("PFB_Player_Controller");
        Debug.Log(statsPlayer);
    }

    void FixedUpdate()
    {
        Collider[] PathFinderTrigger = Physics.OverlapSphere(enmTransform.position, gizmo1Radius, playerDetectionLayer);
        if (PathFinderTrigger.Length != 0)
        {
            player = PathFinderTrigger[0].gameObject;
            pathFinding = true;
        }
        else
        {
            pathFinding = false;
        }
    }

    void Update()
    {
        UpdateEnemyState();
        if (start == true && status == 0)
        {
            target.position = transform.position;
            start = false;
        }

        switch (status)
        {
            //neutral   status 0
            #region
            case 0:
                status = 0;

                target.position = transform.position;
                gameObject.layer = 15;
                ChangeStatus("Se Balade");
                randomTransformPickerTimer -= Time.deltaTime;
                delayBeforeGo -= Time.deltaTime;

                if (randomTransformPickerTimer <= 0 && delayBeforeGo <= 0)
                {
                    randomPosition = Random.insideUnitSphere * gizmo1Radius;
                    randomPosition.y = transform.position.y;

                    if (Vector3.Distance(transform.position, target.position) <= targetTresholdNeutral)
                    {
                        delayBeforeGo = Random.Range(1, maxWaitingTime);
                        randomTransformPickerTimer = resetTransformPicker;

                        if (NavMesh.SamplePosition(transform.position + randomPosition, out navHit, gizmo1Radius, NavMesh.AllAreas))
                        {
                            target.position = navHit.position;
                            enmNavMesh.destination = target.position;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                break;
            #endregion
            //allie     status 1
            #region
            case 1:
                status = 1;
                gameObject.layer = 0;
                ObjectiveSelection();
                ChangeStatus("Attaque les firmes");
                break;
            #endregion
            //bad enm   status 2
            #region
            case 2:
                status = 2;
                gameObject.layer = 13;

                if (damageCooldown < 1)
                {
                    damageCooldown += Time.deltaTime;
                }

                if (isAttracted == false)
                {
                    if (pathFinding == true)
                    {
                        ChangeStatus("Attaque le joueur");
                        enmNavMesh.destination = player.transform.position;
                        Collider[] playerTarget = Physics.OverlapSphere(transform.position + Vector3.up * 2.25f, attackRadius, playerDetectionLayer);

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
                    else if (pathFinding == false)
                    {
                        ChangeStatus("Attaque les alliés");
                        enmNavMesh.destination = target.position;

                        if (Vector3.Distance(transform.position, target.position) <= targetTreshold)
                        {
                            if (nodeIndex >= PathNode.nodeTransform.Length - 1)
                            {
                                return;
                            }
                            nodeIndex++;
                            target = PathNode.nodeTransform[nodeIndex];
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (attractTarget != null)
                    {
                        if (Vector3.Distance(transform.position, target.position) <= targetTreshold)
                        {
                            enmNavMesh.destination = attractTarget.position;
                        }
                    }
                    else
                    {
                        isAttracted = false;
                        return;
                    }
                }

                if (isSlowed || isFastened)
                {
                    enmNavMesh.speed = modifiedSpeed;
                }
                else if (enmNavMesh.speed != enmSpeed)
                {
                    enmNavMesh.speed = enmSpeed;
                }
                break;
            #endregion
            default:
                return;
        }
        /*if(pauser.realPause == true)
        {

        }*/
    }

    void ObjectiveSelection()
    {
        float minDistance = Mathf.Infinity;
        firmeTarget = GameObject.FindGameObjectsWithTag("Objectives");
        for (int i = 0; i < firmeTarget.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, firmeTarget[i].transform.position);
            if (dist < minDistance)
            {
                choosenObjective = firmeTarget[i];
                minDistance = dist;
            }
        }
        if (choosenObjective != null)
            enmNavMesh.destination = choosenObjective.transform.position;
    }

    public IEnumerator ModifieSpeed(float _ModifieTime, float _ModifiedSpeed, bool _stun)//Slow/Accélération/Stuns
    {
        isStunned = _stun;
        isSlowed = true;
        modifiedSpeed = _ModifiedSpeed;
        yield return new WaitForSecondsRealtime(_ModifieTime);
        isSlowed = false;
        isStunned = false;
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
                        if (c.GetComponent<EnmMovement>().hasDot == false)
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
                        target.GetComponent<EnmMovement>().StartCoroutine(DamagesOverTime(_damage, _duration, _range, _index));
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

    void Attack(GameObject _target)
    {
        if (_target != null)
        {
            _target.GetComponent<StatsPlayer>().DamagePlayer(damage);
            damageCooldown = 0;
        }
    }

    void UpdateEnemyState()
    {
        if (enmHealth < healthValues.y)//45 et 0 hostile
        {
            if (status != 2)
            {
                status = 2;
                GetComponentInChildren<MeshRenderer>().material = mats[0];
                convertedToBad.Play();
            }
        }
        else if (enmHealth > healthValues.z)//55 et 100 allie
        {
            if (status != 1)
            {
                status = 1;
                GetComponentInChildren<MeshRenderer>().material = mats[2];
                convertedToGood.Play();
                statsPlayer.GetComponent<StatsPlayer>().RincePlayer(50);//PrixDuMonstre
            }
        }
        else if (enmHealth >= healthValues.y && enmHealth <= healthValues.z)//entre 45 et 55 neutral
        {
            if (status != 0)
            {
                status = 0;
                GetComponentInChildren<MeshRenderer>().material = mats[1];
            }
        }
    }

    void ChangeStatus(string _statusText)
    {
        stateText.text = _statusText;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmo1Color;
        Gizmos.DrawWireSphere(enmTransform.position + Vector3.up * 2.25f, gizmo1Radius);

        Gizmos.color = attackRadiusDebugColor;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 2.25f, attackRadius);
    }
}