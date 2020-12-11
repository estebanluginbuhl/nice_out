using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    //a conserver
    public GameObject firmePFB1, firmePFB2, firmePFB3, firmePFB4, firmePFB5, firmePFB6, shopPFB;
    GameObject selectedFirmePFB;
    GameObject[] maisons, alreadyDone, firme;
    Transform replaceFirme;
    int choosenHouses;

    //a retirer
    public int buildingPerWave;
    public bool selectHouse, callShop;

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
        if (callShop == true)
        {
            callShop = false;
            CallShop();
            return;
        }
    }

    //a conserver
    void GetHouses()//stock tous les prefabs tagués "Maisons" présents dans la scène, dans le tableau "maisons" et créer le tableau dans lequel seront stockés les batiments déjà changés !!!!!A appelé au début de chaque vague
    {
        maisons = null;
        alreadyDone = null;

        if (maisons == null && alreadyDone == null)
        {
            maisons = GameObject.FindGameObjectsWithTag("Maisons");
            alreadyDone = new GameObject[maisons.Length];
        }
    }

    //a conserver
    void SelectHouses(int howManyChoosen)//"howManyChoosen" défini par vague dans le WaveManager
    {
        for (int i = 0; i < howManyChoosen; i++)
        {
            choosenHouses = Random.Range(0, maisons.Length);
            ChangeHouses(choosenHouses);
        }
        return;
    }

    //a conserver
    //desactiver le GameObject pas supprimer pour pouvoir le remettre apres
    void ChangeHouses(int thisOneIsChanged)
    {
        //Random.st
        //selectedFirmePFB = 
        maisons[choosenHouses].gameObject.SetActive(false);
        if (alreadyDone[choosenHouses] == null)//verifie si le game object n'est pas selectionné
        {
            alreadyDone[choosenHouses] = maisons[choosenHouses];
            Instantiate(selectedFirmePFB, maisons[choosenHouses].transform.position, maisons[choosenHouses].transform.rotation);
        }
        else if (alreadyDone[choosenHouses] != null)//relance une selection si le game object est deja changé
        {
            SelectHouses(1);
        }
        return;
    }

    //a conserver
    //récupere le transform du dernier batiment detruit
    void CallShop()
    {
        firme = null;//reset le tableau
        firme = GameObject.FindGameObjectsWithTag("Firmes");
        if (firme.Length == 0)
        {
            return;
        }
        else if (firme.Length == 1)
        {
            replaceFirme = firme[0].transform;
        }
        else return;
    }

    //a conserver
    //change la firme par le shop
    void PlaceShop()
    {
        Instantiate(shopPFB, replaceFirme.position, replaceFirme.rotation);
    }
}