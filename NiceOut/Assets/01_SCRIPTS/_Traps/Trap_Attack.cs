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
    public int damages;
    public float cooldown;
    float cptDuration;
    public bool isGonnaDie;

    [SerializeField]
    LayerMask ennemisMask = -1;

    Transform target = null;
    bool stillInRange;
    GameObject attackTarget = null;
    private void Update()
    {
        if (type == 0)
        {
            AttaquePorteurDePresse();
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
            AttaqueTapisRoulant();
        }
        if (type == 7)
        {
            AttaqueStandCommerce();
        }
    }

    void AttaquePorteurDePresse()
    {
        float attackRange = 2f;
        NavMeshAgent nav = GetComponent<NavMeshAgent>();

        Collider[] ennemis = Physics.OverlapSphere(parentTrap.transform.position, rangeSphere, ennemisMask);

        if (ennemis.Length != 0)
        {
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

                if(Vector3.Distance(target.position, transform.position) <= attackRange)
                {
                    target.GetComponent<StatEnm>().badEnm(damages);
                }

            }
        }
    }

    void AttaquePanneauPublicitaire()
    {
        Collider[] ennemis = Physics.OverlapBox(Vector3.up * rangeBox.y /2 + Vector3.forward * rangeBox.z / 2, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemis.Length != 0)
        {
            foreach (Collider c in ennemis)
            {
                if(c.GetComponent<EnmMovement>().hostile == true)
                c.GetComponentInParent<EnmMovement>().isAttracted = true;
                c.GetComponentInParent<EnmMovement>().attractTarget = parentTrap.transform;
                Debug.Log("attiré");
            }
        }
        if(isGonnaDie == true)
        {
            foreach (Collider c in ennemis)
            {
                c.GetComponentInParent<EnmMovement>().isAttracted = false;
            }
        }
    }

    void AttaqueBacAFruit()
    {
        float slowSpeed = 2f;
        float oldSpeed = 0f;
        float offsetForward = 0.2f;
        Collider[] ennemis = Physics.OverlapBox(Vector3.forward * offsetForward + Vector3.up * rangeBox.y / 2, rangeBox / 2, transform.rotation, ennemisMask);
        Debug.Log("bacafruit");
        if (ennemis.Length != 0)
        {
            Debug.Log("heho");
            foreach (Collider c in ennemis)
            {
                oldSpeed = c.GetComponentInParent<EnmMovement>().enmSpeed;
                c.GetComponentInParent<EnmMovement>().enmSpeed = slowSpeed;
                Debug.Log("slowed");
            }
        }
        //de-slow les ennemis aussi mdr, fin deja faudrait arriver a les slow
    }

    void AttaqueParfum()
    {

    }

    void AttaqueAntenneBrouilleur()
    {

    }

    void AttaqueBar()
    {
        Collider[] ennemis = Physics.OverlapSphere(transform.position, rangeSphere, ennemisMask);

        if (ennemis.Length != 0)
        {
            if(cptDuration == 0)
            {
                foreach (Collider c in ennemis)
                {
                    c.GetComponent<StatEnm>().goodEnm(damages);
                }
                StartCoroutine(Cooldown(this.cooldown));
            }
        }
    }

    void AttaqueTapisRoulant()
    {

    }
    
    void AttaqueStandCommerce()
    {

    }

    IEnumerator  Cooldown(float _duration)
    {
        yield return new WaitForSecondsRealtime(_duration);
        cptDuration = 0;
    }

    private void OnDrawGizmos()
    {
        float offsetForward = 0.2f;
        Gizmos.color = Color.black;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.forward * offsetForward + Vector3.up * rangeBox.y / 2, rangeBox);
        Gizmos.DrawWireSphere(transform.position, rangeSphere);
    }
}