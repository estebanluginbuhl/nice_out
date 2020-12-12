using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    Wave_Manager waveManager;
    public GameObject[] firmePFB;
    public GameObject shopPFB;
    GameObject selectedFirmePFB;
    GameObject[] maisons, alreadyDone, firme;
    Transform replaceFirme;
    int choosenHouses, choosenThatWave, recallHouseIndex;
    int[] recallHouseBoard;

    //
    //a retirer
    public int buildingPerWave, houseIndex;
    public bool changeHouses, placeShop, recallHouse;

    void Start()
    {
        waveManager = GetComponent<Wave_Manager>();
    }
    void FixedUpdate()
    {
        if (changeHouses == true)
        {
            changeHouses = false;
            SelectHouses(buildingPerWave);
            return;
        }
        if (placeShop == true)
        {
            placeShop = false;
            CallShop();
            return;
        }
        if (recallHouse == true)
        {
            recallHouse = false;
            RecallHouse();
            return;
        }
    }
    //

    void GetHouses()//stock tous les prefabs tagués "Maisons" présents dans la scène, dans le tableau "maisons" et créer le tableau dans lequel seront stockés les batiments déjà changés !!!!!A appelé au début de chaque vague
    {
        maisons = null;
        alreadyDone = null;

        if (maisons == null && alreadyDone == null)
        {
            maisons = GameObject.FindGameObjectsWithTag("Maisons");
            alreadyDone = new GameObject[maisons.Length];
            recallHouseBoard = new int[maisons.Length];
        }
    }

    public void SelectHouses(int howManyChoosen)//"howManyChoosen" défini par vague dans le WaveManager
    {
        GetHouses();
        choosenThatWave = howManyChoosen;
        for (int i = 0; i < howManyChoosen; i++)
        {
            choosenHouses = Random.Range(0, maisons.Length);
            ChangeHouses();
        }
        return;
    }

    void ChangeHouses()//desactive le GameObject pas supprimer pour pouvoir le remettre apres
    {
        selectedFirmePFB = firmePFB[Random.Range(0, firmePFB.Length)];
        maisons[choosenHouses].gameObject.SetActive(false);
        if (alreadyDone[choosenHouses] == null)//verifie si le game object n'est déjà pas selectionné
        {
            alreadyDone[choosenHouses] = maisons[choosenHouses];
            Instantiate(selectedFirmePFB, maisons[choosenHouses].transform.position, maisons[choosenHouses].transform.rotation);
            recallHouseIndex += 1;
            recallHouseBoard[recallHouseIndex] = choosenHouses;
        }
        else if (alreadyDone[choosenHouses] != null)//relance une selection si le game object est deja changé
        {
            SelectHouses(1);
        }
        return;
    }

    public void CallShop()//récupere le transform du dernier batiment detruit
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
            PlaceShop();
        }
        else return;
    }

    void PlaceShop()//change la firme par le shop
    {
        replaceFirme.gameObject.SetActive(false);
        Instantiate(shopPFB, replaceFirme.position, replaceFirme.rotation);
    }

    public void RecallHouse()//rappel la maison sous laquelle se trouve le batiment de firme détruit
    {
        //ranger dans un tableau les maisons qui viennent d'être désactivé pour pouvoir les rappeler
        for (int i = 1; i <= choosenThatWave; i++)
        {
            Debug.Log("recall " + i);
            alreadyDone[recallHouseBoard[i]].gameObject.SetActive(true);
        }
        recallHouseIndex = 0;
        recallHouseBoard = null;
    }
}