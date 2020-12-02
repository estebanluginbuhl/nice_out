using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Traps : MonoBehaviour // detail d'achat et d'upgrade des pieges
{
    public int trapType;//Le type de piège sert à verif si on l'a deja dans l'inventaire

    //vie et munitions
    public float[] fullUsure;
    public float usure;
    public float UsurePercentage;

    public GameObject player;
    public GameObject child;
    public Vector3 colliderSize;
    [HideInInspector]
    public Vector3 transformTrap;
    [HideInInspector]
    public Vector3 rotationTrap;
    public int[] costs;
    public int[] sellCosts;
    public GameObject[] trapAndUpgrades;
    public float[] offsetPositions;
    public int upgradeIndex = 0; //Numero d'upgrade
    public bool stopUpgrade = false; //Quand toute les upgrades ont etait faite, s'adapte au nombre d'upgrade tout seul pas besoin de toucher le code

    public Canvas ui_healthBar;
    public float ui_hbHeight;
    public Sprite[] ui_Image;


    BoxCollider box;

    private void Start()
    {
        box = this.GetComponent<BoxCollider>();
        if(box != null)
        {
            box.size = colliderSize;
            if(this.trapType == 2)
            {
                box.isTrigger = true;
            }
        }
        this.usure = this.fullUsure[this.upgradeIndex];
        this.UsurePercentage = usure / fullUsure[this.upgradeIndex];
        
        this.rotationTrap = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
        this.child = GameObject.Instantiate(this.trapAndUpgrades[upgradeIndex], this.transform.position, Quaternion.Euler(this.rotationTrap));
        this.child.GetComponent<Trap_Attack>().parentTrap = this.gameObject;
        this.child.GetComponent<Trap_Attack>().type = this.trapType;

        Canvas hb = Instantiate(ui_healthBar, transform.position + Vector3.up * ui_hbHeight, Quaternion.identity);
        hb.GetComponent<Trap_Stats>().trap = this;
        hb.GetComponent<Trap_Stats>().player = this.player;
        hb.transform.SetParent(child.transform);
    }

    private void Update()
    {
        this.usure -= Time.deltaTime;

        this.UsurePercentage = usure / fullUsure[this.upgradeIndex];

        if (this.upgradeIndex > this.trapAndUpgrades.Length - 1)
        {
            this.upgradeIndex = this.trapAndUpgrades.Length - 1;
        }

        if (this.usure <= 1f)
        {
            child.GetComponent<Trap_Attack>().isGonnaDie = true;
        }
        if (this.usure <= 0)
        {
            Destroy(this.child);
            Destroy(this.gameObject);
        }
    }

    public void DamageTrap(int value)
    {
        this.usure -= value;
    }
    /*
    public void Upgrade()
    {
        Destroy(this.child);
        this.upgradeIndex += 1;
        this.child = GameObject.Instantiate(this.trapAndUpgrades[upgradeIndex], this.transform.position, Quaternion.Euler(this.rotationTrap));
        this.child.GetComponent<Trap_Attack>().parentTrap = this.gameObject;
        this.child.GetComponent<Trap_Attack>().type = this.trapType;
        this.child.transform.SetParent(this.transform);
        this.usure = Mathf.RoundToInt(this.fullUsure[upgradeIndex] * this.UsurePercentage);
    }*/
    public void UpgradeForInventory()
    {
        this.upgradeIndex += 1;
    }

}
