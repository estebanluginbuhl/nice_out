using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    //a conserver
    GameObject[] maisons, alreadyDone;
    int choosenHouses;

    //a retirer
    public int buildingPerWave;
    public bool selectHouse;

    //a conserver
    void Awake()
    {
        GetHouses();
    }

    //a retirer
    void FixedUpdate()
    {
        if (selectHouse == true)
        {
            selectHouse = false;
            SelectHouses(buildingPerWave);
            return;
        }
    }

    //a conserver
    void GetHouses()//stock tous les prefabs tagués "Maisons" présents dans la scène, dans le tableau "maisons" et créer le tableau dans lequel seront stockés les batiments déjà changés
    {
        maisons = GameObject.FindGameObjectsWithTag("Maisons");
        alreadyDone = new GameObject[maisons.Length];
    }

    //a conserver
    void SelectHouses(int howManyChoosen)//"howManyChoosen" défini par par vague dans le WaveManager
    {
        for (int i = 0; i < howManyChoosen; i++)
        {
            choosenHouses = Random.Range(0, maisons.Length);
            ChangeHouses(choosenHouses);
        }
        return;
    }

    //a conserver
    //desactiver le GO pas supprimer pour pouvoir le remettre apres
    void ChangeHouses(int thisOneIsChanged)
    {
        maisons[choosenHouses].gameObject.SetActive(false);
        if (alreadyDone[choosenHouses] == null)//verifie si le game object n'est pas selectionné
        {
            alreadyDone[choosenHouses] = maisons[choosenHouses];
        }
        else if (alreadyDone[choosenHouses] != null)//relance une selection si le game object est deja changé
        {
            SelectHouses(1);
        }
        return;
    }
}