using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Traps : MonoBehaviour // detail d'achat et d'upgrade des pieges
{
    public int trapType; //Le type de piège sert à verif si on l'a deja dans l'inventaire
    public int[] life;
    public GameObject child;
    public Vector3 transformTrap;
    public int[] costs;
    public int[] sellCosts;
    public GameObject[] trapAndUpgrades;
    public float[] offsetPositions;
    public int upgradeIndex = 0; //Numero d'upgrade
    public bool stopUpgrade = false; //Quand toute les upgrades ont etait faite, s'adapte au nombre d'upgrade tout seul pas besoin de toucher le code

    public GameObject emplacement;
    public Sprite uiImage;

    private void Start()
    {
        this.upgradeIndex = 0;
        this.child = GameObject.Instantiate(this.trapAndUpgrades[0], this.transform.position + Vector3.up * offsetPositions[0], Quaternion.identity);
    }

    private void Update()
    {
        if(this.upgradeIndex > this.trapAndUpgrades.Length -1)
        {
            this.upgradeIndex = this.trapAndUpgrades.Length - 1;
        }
        transformTrap = transform.position;
    }

    public void DamageTrap(int value)
    {
        this.life[this.upgradeIndex] -= value;
        if(this.life[this.upgradeIndex] <= 0)
        {
            this.emplacement.GetComponent<Emplacement_Material_Change>().isOccupied = false;
            this.emplacement.GetComponent<Emplacement_Material_Change>().ChangeMat(1);
            Destroy(this.child);
            Destroy(this);
        }
    }
}
