using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Manager : MonoBehaviour
{
    public LayerMask emplacements;
    public float detectionRadius; //Variables sphere de detection
    public GameObject spherePosition;
    public GameObject selectedPlace = null;
    public GameObject oldSelection = null;
    public Vector3 desiredVelocity;
    public GameObject selectedTrap;

    void Update()
    {
        if (GetComponent<Switch_Mode>().mode)
        {
            float minDist = Mathf.Infinity;

            Collider[] selectedPlaces = Physics.OverlapSphere(spherePosition.transform.position, detectionRadius, emplacements); // sphere de detection d'emplacements

            oldSelection = selectedPlace;

            foreach (Collider c in selectedPlaces) //Selection de l'emplacement le plus proche
            {
                float dist = Vector3.Distance(c.gameObject.transform.position, transform.position);
                if(dist< minDist)
                {
                    selectedPlace = c.gameObject;
                    minDist = dist;
                }
            }

            if(selectedPlaces.Length == 0)
            {
                selectedPlace = null;
            }

            if (selectedPlace != null) //appel du placement et de l'amelioration des pieges
            {
                if (Input.GetButtonDown("Place"))
                {
                    if (selectedPlace.tag.Equals("FreeSpace"))
                    {
                        if (selectedPlace.GetComponent<Emplacement_Material_Change>().isOccupied == false)
                        {
                            PlaceTrap(selectedTrap);
                        }
                        else
                        {
                            UpgradeTrap();
                        }
                    }
                }
            }
        }
        else //deselectionne l'emplacment selectionné en mode combat
        {
            if(selectedPlace != null)
            {
                selectedPlace = null;
            }
        }


        if(selectedPlace != null) //colore l'emplacement selectionné
        {
            if (selectedPlace.GetComponent<Emplacement_Material_Change>().isOccupied)
            {
                selectedPlace.GetComponent<Emplacement_Material_Change>().ChangeMat(4);
            }
            else
            {
                selectedPlace.GetComponent<Emplacement_Material_Change>().ChangeMat(2);
            }
        }

        if(oldSelection != null) //reset la couleur une fois l'emplacement deselectionné
        {
            if (oldSelection != selectedPlace || selectedPlace == null)
            {
                if (oldSelection.GetComponent<Emplacement_Material_Change>().isOccupied == true)
                {
                    oldSelection.GetComponent<Emplacement_Material_Change>().ChangeMat(3);
                }
                else if(oldSelection.GetComponent<Emplacement_Material_Change>().isOccupied == false)
                {
                    oldSelection.GetComponent<Emplacement_Material_Change>().ChangeMat(1);
                }
            }
        }
    
    }

    
    public void PlaceTrap(GameObject selectedTrap)//Methode de placement du piège selectionné
    {
        if (GetComponent<StatsPlayer>().gold >= selectedTrap.GetComponent<Traps>().costs[0])//vérifie que le joueur a assez d'argent pour payer le piège
        {
            GetComponent<StatsPlayer>().PlayerBuy(selectedTrap.GetComponent<Traps>().costs[0]); //Paye le piège
            GameObject trap = Instantiate(selectedTrap, selectedPlace.transform.position, Quaternion.identity); //Instancie le piège
            selectedPlace.GetComponent<Emplacement_Material_Change>().placedTrap = trap;
            trap.GetComponent<Traps>().transform.SetParent(selectedPlace.transform); //le piège devient enfant de l'emplacement selectionné
            trap.GetComponent<Traps>().emplacement = selectedPlace.gameObject;
            selectedPlace.GetComponent<Emplacement_Material_Change>().isOccupied = true; // definie l'emplacement comme occupé
        }
    }

    public void UpgradeTrap() //Methode d'upgrade du piège selectionné
    {
        Traps trapStats = selectedPlace.GetComponent<Emplacement_Material_Change>().placedTrap.GetComponent<Traps>(); //get les stats du piege séléctionné

        if (trapStats.stopUpgrade == false)
        {
            if (GetComponent<StatsPlayer>().gold >= trapStats.costs[trapStats.upgradeIndex + 1])//Check l'argent du joueur
            {
                Destroy(trapStats.child); //Detruit l'ancienne upgrade du piège
                trapStats.child = GameObject.Instantiate(trapStats.trapAndUpgrades[trapStats.upgradeIndex + 1], trapStats.transformTrap + Vector3.up * trapStats.offsetPositions[trapStats.upgradeIndex + 1], Quaternion.identity);//instancie la nouvelle upgrade du piège
                GetComponent<StatsPlayer>().PlayerBuy(trapStats.costs[trapStats.upgradeIndex + 1]); //Paye l'upgrade

                if (trapStats.upgradeIndex + 1 >= trapStats.trapAndUpgrades.Length - 1)//Augmente l'index de l'upgrade du trap, puis quand on a atteint le niveau max, arrete les upgrades.
                {
                    trapStats.upgradeIndex += 1;
                    trapStats.stopUpgrade = true;
                }
                else
                {
                    trapStats.upgradeIndex += 1;
                }
            }
            else
            {
                Debug.Log("Bro ya plus d'argent dans les caisses la");
                return;
            }
        }
        else
        {
            Debug.Log("Bro ya plus rien à upgrade");
            return;
        }
    }

    public void SellTrap() //Vends ton piège
    {
        if(selectedPlace.GetComponent<Emplacement_Material_Change>().isOccupied == true)
        {
            Traps trapStats = selectedPlace.GetComponent<Emplacement_Material_Change>().placedTrap.GetComponent<Traps>(); //get les stats du piege séléctionné
            GetComponent<StatsPlayer>().gold += trapStats.sellCosts[trapStats.upgradeIndex]; //rembourse le joueur
            Destroy(trapStats.child);
            Destroy(trapStats.gameObject);//Detruit le piège
            selectedPlace.GetComponent<Emplacement_Material_Change>().isOccupied = false; // definie l'emplacement comme libre
        }
        else
        {
            return;
        }
    }

    private void OnDrawGizmos() //Afficher la sphere de detection dans la scene
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition.transform.position, detectionRadius);
    }
}
