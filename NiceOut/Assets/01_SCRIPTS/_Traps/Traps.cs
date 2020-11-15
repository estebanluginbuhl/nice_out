using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Traps : MonoBehaviour // detail d'achat et d'upgrade des pieges
{
    public int trapType;//Le type de piège sert à verif si on l'a deja dans l'inventaire

    //vie et munitions
    public int[] fullLife, fullAmmo;
    public int life, ammo;
    public float lifePercentage, ammoPercentage;

    public GameObject child;
    public Vector3 colliderSize;

    public Vector3 transformTrap;
    public Vector3 rotationTrap;
    public int costs;
    public int[] sellCosts;
    public GameObject[] trapAndUpgrades;
    public float[] offsetPositions;
    public int upgradeIndex = 0; //Numero d'upgrade
    public bool stopUpgrade = false; //Quand toute les upgrades ont etait faite, s'adapte au nombre d'upgrade tout seul pas besoin de toucher le code

    public Sprite uiImage;
    BoxCollider box;

    private void Start()
    {
        box = this.GetComponent<BoxCollider>();
        if(box != null)
        {
            box.size = colliderSize;
        }
        this.life = this.fullLife[0];
        this.ammo = this.fullAmmo[0];
        this.lifePercentage = life / fullLife[0];
        this.lifePercentage = ammo / fullAmmo[0];

        this.upgradeIndex = 0;

        this.rotationTrap = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
        this.child = GameObject.Instantiate(this.trapAndUpgrades[upgradeIndex ], this.transform.position, Quaternion.Euler(this.rotationTrap));
        this.child.GetComponent<Trap_Attack>().parentTrap = this.gameObject;
        this.child.GetComponent<Trap_Attack>().type = this.trapType;
    }

    private void Update()
    {
        if(this.upgradeIndex > this.trapAndUpgrades.Length - 1)
        {
            this.upgradeIndex = this.trapAndUpgrades.Length - 1;
        }
    }

    public void DamageTrap(int value)
    {
        this.life -= value;
        this.lifePercentage = life / fullLife[this.upgradeIndex - 1];

        if (this.life <= 0)
        {
            Destroy(this.child);
            Destroy(this);
        }
    }

    public void Upgrade()
    {
        Destroy(this.child);
        this.upgradeIndex += 1;
        this.child = GameObject.Instantiate(this.trapAndUpgrades[upgradeIndex], this.transform.position + Vector3.up * offsetPositions[upgradeIndex], Quaternion.Euler(this.rotationTrap));
        this.child.GetComponent<Trap_Attack>().parentTrap = this.gameObject;
        this.child.GetComponent<Trap_Attack>().type = this.trapType;
        this.life = Mathf.RoundToInt(this.fullLife[upgradeIndex] * this.lifePercentage);
        this.ammo = Mathf.RoundToInt(this.fullAmmo[upgradeIndex] * this.ammoPercentage);
    }
}
