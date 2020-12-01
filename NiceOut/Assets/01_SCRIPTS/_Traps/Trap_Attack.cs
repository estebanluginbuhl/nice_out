using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void TakeMunition()
    {
        //-1 munition;
        parentTrap.GetComponent<Traps>().usure -= 1;
        parentTrap.GetComponent<Traps>().UsurePercentage = parentTrap.GetComponent<Traps>().usure / parentTrap.GetComponent<Traps>().fullUsure[parentTrap.GetComponent<Traps>().upgradeIndex];
    }

    void AttaquePorteurDePresse()
    {

    }

    void AttaquePanneauPublicitaire()
    {
        Collider[] ennemis = Physics.OverlapBox(Vector3.up * rangeBox.y /2 + Vector3.forward * rangeBox.z / 2, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemis.Length != 0)
        {
            foreach (Collider c in ennemis)
            {
                c.GetComponentInParent<EnmMovement>().isAttracted = true;
                c.GetComponentInParent<EnmMovement>().attractTarget = parentTrap.transform;
                Debug.Log("attiré");
            }
            StartCoroutine(Cooldown(this.cooldown));
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
        Collider[] ennemis = Physics.OverlapBox(Vector3.up * rangeBox.y / 2 + Vector3.forward * rangeBox.z / 2, rangeBox / 2, transform.rotation, ennemisMask);
        if (ennemis.Length != 0)
        {
            foreach (Collider c in ennemis)
            {
                oldSpeed = c.GetComponentInParent<EnmMovement>().enmSpeed;
                c.GetComponentInParent<EnmMovement>().enmSpeed = slowSpeed;
                Debug.Log("slowed");
            }
            StartCoroutine(Cooldown(this.cooldown));
        }
        if (isGonnaDie == true)
        {
            foreach (Collider c in ennemis)
            {
                c.GetComponentInParent<EnmMovement>().enmSpeed = oldSpeed;
            }
        }
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
        Gizmos.color = Color.black;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.up * rangeBox.y / 2 + Vector3.forward * rangeBox.z / 2, rangeBox);
        Gizmos.DrawWireSphere(transform.position, rangeSphere);
    }
}