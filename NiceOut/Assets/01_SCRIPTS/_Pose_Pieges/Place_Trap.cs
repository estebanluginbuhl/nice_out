using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_Trap : MonoBehaviour
{
    public LayerMask emplacements;
    public float detectionRadius; //Variables sphere de detection
    public GameObject spherePosition;
    public GameObject selectedPlace = null;
    public GameObject oldSelection = null;
    public Vector3 desiredVelocity;

    public GameObject testTrap;

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
                        PlaceTrap(testTrap);
                    }
                    if (selectedPlace.tag.Equals("Trapped"))
                    {
                        UpgradeTrap();
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


        if(selectedPlace != null) //TEMP : colore l'emplacement selectionné
        {
            selectedPlace.GetComponent<Emplacement_Material_Change>().ChangeMat(2);
        }

        if(oldSelection != null) //TEMP : reset la couleur une fois l'emplacement deselectionné
        {
            if (oldSelection != selectedPlace || selectedPlace == null)
            {
                oldSelection.GetComponent<Emplacement_Material_Change>().ChangeMat(1);
            }
        }
    
    }

    public void UpgradeTrap() //Methode d'upgrade du piège selectionné
    {

    }
    public void PlaceTrap(GameObject selectedTrap)//Methode de placement du piège selectionné
    {
        if (GetComponent<StatsPlayer>().gold >= selectedTrap.GetComponent<Traps>().costs[0])
        {
            GetComponent<StatsPlayer>().PlayerBuy(selectedTrap.GetComponent<Traps>().costs[0]);
            GameObject.Instantiate(selectedTrap, selectedPlace.transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos() //Afficher la sphere de detection dans la scene
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition.transform.position, detectionRadius);
    }
}
