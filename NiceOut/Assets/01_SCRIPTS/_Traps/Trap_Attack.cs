using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trap_Attack : MonoBehaviour
{
    [Header("Paramametre Générale")]
    //Truc important
    public GameObject parentTrap;
    public int type;

    public float rangeSphere;
    public Vector3 rangeBox;
    public float offsetForward;
    public int damages;
    public float cooldown;
    float cptCooldown;
    public bool isGonnaDie;

    [SerializeField]
    LayerMask ennemisMask = -1;

    Transform target = null;
    bool stillInRange;
    GameObject attackTarget = null;
    Collider[] alreadySlowed;

    public bool canAttack = false;
    private void Start()
    {
        /*//Debugage SAAAAAAAAAAAAAAAAAAAAAAAAALLLLLLLLLLLLLLLLLLLLLLLLLLLLLLE MAMEN
        GameObject test = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector3 forwardVector = transform.rotation * Vector3.forward;
        test.transform.position = transform.position + Vector3.up * rangeBox.y / 2 + forwardVector * offsetForward;
        test.transform.rotation = transform.rotation;
        test.transform.localScale = rangeBox;*/
    }
    private void Update()
    {
        if(parentTrap != null && parentTrap.GetComponent<Traps>().isPaused == false)
        {
            if (canAttack == true)
            {
                switch (type)
                {
                    case 0:
                        AttaquePorteurDePresse();
                        break;
                    case 1:
                        AttaquePanneauPublicitaire();
                        break;
                    case 2:
                        AttaqueBacAFruit();
                        break;
                    case 3:
                        AttaqueParfum();
                        break;
                    case 4:
                        AttaqueAntenneBrouilleur();
                        break;
                    case 5:
                        AttaqueBar();
                        break;
                    case 6:
                        Debug.Log("pas ouf gros");
                        //AttaqueTapisRoulant();
                        break;
                    case 7:
                        AttaqueStandCommerce();
                        break;
                    default: return;
                }
                if (type == 0)
                {

                }
                if (type == 1)
                {
                    AttaquePanneauPublicitaire();
                }
                if (type == 2)
                {
                    AttaqueBacAFruit();
                }
                if (type == 3)
                {
                    AttaqueParfum();
                }
                if (type == 4)
                {
                    AttaqueAntenneBrouilleur();
                }
                if (type == 5)
                {
                    AttaqueBar();
                }
                if (type == 6)
                {
                    //AttaqueTapisRoulant();
                }
                if (type == 7)
                {
                    AttaqueStandCommerce();
                }
            }
        }
    }

    void AttaquePorteurDePresse()//Valide
    {
        float attackRange = 2f;
        NavMeshAgent nav = GetComponent<NavMeshAgent>();

        Collider[] ennemis = Physics.OverlapSphere(parentTrap.transform.position, rangeSphere, ennemisMask);

        if (ennemis.Length != 0)
        {
            Debug.Log(ennemis[0]);
            if(target == null)
            {
                float minDist = Mathf.Infinity;
                foreach (Collider c in ennemis)
                {
                    float dist = Vector3.Distance(parentTrap.transform.position, c.transform.position);

                    if (dist < minDist)
                    {
                        minDist = dist;
                        target = c.transform;
                    }
                }
            }
            else
            {
                if (target.GetComponent<EnmMovement>().hostile == false)
                {
                    target = null;
                    return;
                }

                stillInRange = false;
                for (int i = 0; i < ennemis.Length; i++)
                {
                    if(ennemis[i].transform == target)
                    {
                        stillInRange = true;
                    }
                }

                if(stillInRange == false)
                {
                    target = null;
                    return;
                }

                nav.destination = target.position;

                if(cptCooldown == 0)
                {
                    if (Vector3.Distance(target.position, transform.position) <= attackRange)
                    {
                        target.GetComponent<EnmMovement>().DamageBadEnemy(damages);
                        StartCoroutine(Cooldown(cooldown));
                    }
                }
            }
        }
    }

    void AttaquePanneauPublicitaire()
    {
        Vector3 forwardVector = transform.rotation * Vector3.forward;
        Collider[] ennemis = Physics.OverlapBox(parentTrap.transform.position + Vector3.up * rangeBox.y /2 + forwardVector * offsetForward, rangeBox / 2, transform.rotation, ennemisMask);

        if (ennemis.Length != 0)
        {
            Debug.Log("test");
            foreach (Collider c in ennemis)
            {
                if (isGonnaDie == false)
                {
                    c.GetComponent<EnmMovement>().isAttracted = true;
                    c.GetComponent<EnmMovement>().attractTarget = parentTrap.transform;
                }
                else
                {
                    c.GetComponent<EnmMovement>().attractTarget = null;
                    c.GetComponent<EnmMovement>().isAttracted = false;
                }
            }
        }
    }

    void AttaqueBacAFruit()
    {
        Vector3 forwardVector = transform.rotation * Vector3.forward;
        Collider[] ennemis = Physics.OverlapBox(parentTrap.transform.position + forwardVector * offsetForward + Vector3.up * rangeBox.y / 2, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemis.Length != 0)
        {
            foreach (Collider c in ennemis)
            {
                if (c.GetComponent<EnmMovement>().isSlowed == false)
                {
                    StartCoroutine(c.GetComponent<EnmMovement>().ModifieSpeed(cooldown, damages, false));
                }
                else
                {
                    return;
                }
            }
        }
    }

    void AttaqueParfum()
    {
        int dotDuration = Mathf.RoundToInt(cooldown);
        int nbTransmission = dotDuration - 2;
        float rangeTransmission = 3f;
        Collider[] units = Physics.OverlapSphere(transform.position, rangeSphere, ennemisMask);

        if (units.Length != 0)
        {
            if (cptCooldown == 0)
            {
                if(units[0].GetComponent<EnmMovement>().hasDot == true)
                {
                    foreach(Collider c in units)
                    {
                        if (c.GetComponent<EnmMovement>().hasDot == false)
                        {
                            target = c.transform;
                            break;
                        }
                    }
                }
                else
                {
                    target = units[0].transform;
                }
                if (target != null)
                {
                    StartCoroutine(target.GetComponent<EnmMovement>().DamagesOverTime(damages, dotDuration, rangeTransmission, nbTransmission));
                    StartCoroutine(Cooldown(cooldown));
                    target = null;
                }
            }
        }
    }

    void AttaqueAntenneBrouilleur()//Valide
    {
        float slowSpeed = 0f;

        Collider[] units = Physics.OverlapSphere(transform.position, rangeSphere, ennemisMask);

        if (units.Length != 0)
        {
            if(cptCooldown == 0)
            {
                foreach (Collider c in units)
                {
                    StartCoroutine(c.GetComponent<EnmMovement>().ModifieSpeed(damages, slowSpeed, true));
                    StartCoroutine(Cooldown(cooldown));
                }
            }
        }
    }

    void AttaqueBar()//Valide
    {
        Collider[] ennemis = Physics.OverlapSphere(transform.position, rangeSphere, ennemisMask);

        if (ennemis.Length != 0)
        {
            if(cptCooldown == 0)
            {
                foreach (Collider c in ennemis)
                {
                    c.GetComponent<EnmMovement>().DamageBadEnemy(damages);
                }
                StartCoroutine(Cooldown(this.cooldown));
            }
        }
    }

    /*void AttaqueTapisRoulant()
    {
        //Devant : accélérer
        Vector3 forwardVector = transform.rotation * Vector3.forward;
        Vector3 backwardVector = transform.rotation * Vector3.backward;

        Collider[] ennemisToEject = Physics.OverlapBox(parentTrap.transform.position + forwardVector * offsetForward + Vector3.up * rangeBox.y / 2, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemisToEject.Length != 0)
        {
            foreach (Collider e in ennemisToEject)
            {
                if (e.GetComponent<EnmMovement>().isSlowed == false)
                {
                    e.GetComponent<EnmMovement>().isFastened = true;
                    e.GetComponent<EnmMovement>().modifiedSpeed = cooldown;//cooldown doit etre egale a la vitesse de l'ennemi fastened pour ce piege
                    Debug.Log("Fastened");
                }
            }
        }
        //Deriere : ralentir
        Collider[] ennemisToSlow = Physics.OverlapBox(parentTrap.transform.position + backwardVector * offsetForward + Vector3.up * rangeBox.y / 2, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemisToSlow.Length != 0)
        {
            foreach (Collider s in ennemisToSlow)
            {
                if (s.GetComponent<EnmMovement>().isFastened == false)
                {
                    s.GetComponent<EnmMovement>().isSlowed = true;
                    s.GetComponent<EnmMovement>().modifiedSpeed = damages;//damages doit etre egale a la vitesse de l'ennemi slow pour ce piege
                    Debug.Log("Slowed");
                }
            }
        }
    }*/


    void AttaqueStandCommerce()
    {
        float attaqueRange = 2f;
        Vector3 forwardVector = transform.rotation * Vector3.forward;
        Collider[] ennemis = Physics.OverlapBox(Vector3.up * rangeBox.y / 2 + forwardVector * offsetForward, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemis.Length != 0)
        {
            Debug.Log("stand");
            foreach (Collider c in ennemis)
            {
                if (isGonnaDie == false)
                {
                    c.GetComponent<EnmMovement>().isAttracted = true;
                    c.GetComponent<EnmMovement>().attractTarget = this.parentTrap.transform;
                    if (cptCooldown == 0)
                    {
                        if (Vector3.Distance(c.transform.position, transform.position) <= attaqueRange)
                        {
                            c.GetComponent<EnmMovement>().DamageBadEnemy(damages);
                        }
                        StartCoroutine(Cooldown(this.cooldown));
                    }
                }
                else
                {
                    c.GetComponent<EnmMovement>().attractTarget = null;
                    c.GetComponent<EnmMovement>().isAttracted = false;
                }
            }
        }
    }

    IEnumerator  Cooldown(float _duration)
    {
        cptCooldown = 1;
        yield return new WaitForSecondsRealtime(_duration);
        cptCooldown = 0;
    }

    private void OnDrawGizmos()
    {
        if (parentTrap != null)
        {
            if (type == 0)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(parentTrap.transform.position, rangeSphere);
            }
            if (type == 1)
            {
                Gizmos.color = Color.black;
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawWireCube(Vector3.up * rangeBox.y / 2 + Vector3.forward * offsetForward, rangeBox);
            }
            if (type == 2)
            {
                Gizmos.color = Color.black;
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawWireCube(Vector3.up * rangeBox.y / 2 + Vector3.forward * offsetForward, rangeBox);
            }
            if (type == 3)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(transform.position, rangeSphere);
            }
            if (type == 4)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(transform.position, rangeSphere);
            }
            if (type == 5)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(transform.position, rangeSphere);
            }
            if (type == 6)
            {
                Gizmos.matrix = transform.localToWorldMatrix;

                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(Vector3.up * rangeBox.y / 2 + Vector3.forward * offsetForward, rangeBox);
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(Vector3.up * rangeBox.y / 2 + Vector3.back * offsetForward, rangeBox);
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(Vector3.up * rangeBox.y / 2, new Vector3(rangeBox.x, rangeBox.y, rangeBox.z * 2));
            }
            if (type == 7)
            {
                Gizmos.color = Color.black;
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawWireCube(Vector3.up * rangeBox.y / 2 + Vector3.forward * offsetForward, rangeBox);
            }
        }
    }
}