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
                if (target.GetComponentInParent<EnmMovement>().hostile == false)
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
                        target.GetComponentInParent<StatEnm>().badEnm(damages);
                        StartCoroutine(Cooldown(cooldown));
                    }
                }
            }
        }
    }//Valide

    void AttaquePanneauPublicitaire()
    {
        Collider[] ennemis = Physics.OverlapBox(Vector3.up * rangeBox.y /2 + Vector3.forward * offsetForward, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemis.Length != 0)
        {
            foreach (Collider c in ennemis)
            {
                if (isGonnaDie == false)
                {
                    if (c.GetComponentInParent<EnmMovement>().hostile == true)
                    {
                        c.GetComponentInParent<EnmMovement>().isAttracted = true;
                        c.GetComponentInParent<EnmMovement>().attractTarget = this.parentTrap.transform;
                    }
                    else
                    {
                        c.GetComponentInParent<EnmMovement>().isAttracted = false;
                        c.GetComponentInParent<EnmMovement>().attractTarget = null;
                    }
                }
                else
                {
                    c.GetComponentInParent<EnmMovement>().attractTarget = null;
                    c.GetComponentInParent<EnmMovement>().isAttracted = false;
                }
            }
        }
    }//Valide

    void AttaqueBacAFruit()
    {
        float slowSpeed = 2f;
        Collider[] ennemis = Physics.OverlapBox(Vector3.forward * offsetForward + Vector3.up * rangeBox.y / 2, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemis.Length != 0)
        {
            foreach (Collider c in ennemis)
            {
                StartCoroutine(c.GetComponentInParent<EnmMovement>().Slow(cooldown ,slowSpeed));
                Debug.Log("Slowed");
            }
        }
        //de-slow les ennemis aussi
    }

    void AttaqueParfum()
    {

    }

    void AttaqueAntenneBrouilleur()
    {
        Collider[] ennemis = Physics.OverlapSphere(transform.position, rangeSphere, ennemisMask);

        if (ennemis.Length != 0)
        {
            foreach (Collider c in ennemis)
            {
                //Stun
            }
        }
    }

    void AttaqueBar()
    {
        Collider[] ennemis = Physics.OverlapSphere(transform.position, rangeSphere, ennemisMask);

        if (ennemis.Length != 0)
        {
            if(cptCooldown == 0)
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
        float slowSpeed = 2f;
        float fastSpeed = 10f;

        //Devant : accélérer
        Collider[] ennemisToEject = Physics.OverlapBox(Vector3.forward * offsetForward + Vector3.up * rangeBox.y / 2, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemisToEject.Length != 0)
        {
            foreach (Collider e in ennemisToEject)
            {
                if (e.GetComponentInParent<EnmMovement>().isSlowed == false)
                {
                    StartCoroutine(e.GetComponentInParent<EnmMovement>().Fasten(cooldown, fastSpeed));
                    Debug.Log("Fastened");
                }
            }
        }
        //Deriere : ralentir
        Collider[] ennemisToSlow = Physics.OverlapBox(Vector3.back * offsetForward + Vector3.up * rangeBox.y / 2, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemisToSlow.Length != 0)
        {
            foreach (Collider s in ennemisToSlow)
            {
                if (s.GetComponentInParent<EnmMovement>().isFastened == false)
                {
                    StartCoroutine(s.GetComponentInParent<EnmMovement>().Slow(cooldown, slowSpeed));
                    Debug.Log("Slowed");
                }
            }
        }

        Collider[] ennemis = Physics.OverlapBox(Vector3.back * offsetForward + Vector3.up * rangeBox.y / 2, new Vector3(rangeBox.x / 2, rangeBox.y / 2, rangeBox.z), transform.rotation, ennemisMask);
        if (ennemis.Length != 0)
        {
            foreach (Collider c in ennemis)
            {
                if (c.GetComponentInParent<EnmMovement>().isSlowed)
                {
                    Debug.Log("continue Slowed");
                    StartCoroutine(c.GetComponentInParent<EnmMovement>().Slow(cooldown, slowSpeed));
                }
                else if (c.GetComponentInParent<EnmMovement>().isFastened)
                {
                    Debug.Log("continue Fasten");
                    StartCoroutine(c.GetComponentInParent<EnmMovement>().Fasten(cooldown, fastSpeed));
                }
                else
                {
                    return;
                }
            }
        }
    }
    
    void AttaqueStandCommerce()
    {
        float attaqueRange = 1f;
        Mesh msh = GetComponent<MeshFilter>().mesh;
        Collider[] ennemis = Physics.OverlapBox(Vector3.up * rangeBox.y / 2 + Vector3.forward * offsetForward, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemis.Length != 0)
        {
            foreach (Collider c in ennemis)
            {
                if (isGonnaDie == false)
                {
                    if (c.GetComponentInParent<EnmMovement>().hostile == true)
                    {
                        c.GetComponentInParent<EnmMovement>().isAttracted = true;
                        c.GetComponentInParent<EnmMovement>().attractTarget = this.parentTrap.transform;
                        if (cptCooldown == 0)
                        {
                            if (Vector3.Distance(c.transform.position, transform.position) <= attaqueRange)
                            {
                                c.GetComponent<StatEnm>().goodEnm(damages);
                            }
                            StartCoroutine(Cooldown(this.cooldown));
                        }
                    }
                    else
                    {
                        c.GetComponentInParent<EnmMovement>().isAttracted = false;
                        c.GetComponentInParent<EnmMovement>().attractTarget = null;
                    }
                }
                else
                {
                    c.GetComponentInParent<EnmMovement>().attractTarget = null;
                    c.GetComponentInParent<EnmMovement>().isAttracted = false;
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
            Gizmos.matrix = transform.localToWorldMatrix;
        }
        if (type == 4)
        {
            Gizmos.color = Color.black;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(transform.position, rangeSphere);
        }
        if (type == 5)
        {
            Gizmos.color = Color.black;
            Gizmos.matrix = transform.localToWorldMatrix;
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