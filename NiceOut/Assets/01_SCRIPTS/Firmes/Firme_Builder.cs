using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firme_Builder : MonoBehaviour
{
    Wave_Manager waveManager;
    int nbFirmes;
    GameObject[] allHouses;
    public GameObject[] allFirmes;//Prefabs de firmes
    [SerializeField]
    Vector3[] howManyFirmesPerWaves;//x = cb de petite firmes, y = cb de moyenne firmes et z = cb de grande firmes
    [SerializeField]
    int[] typesOfFirmesPerWave;
    int indexTypeArray = 0;

    public GameObject pfb_Shop;
    GameObject[] modifiedHouses;
    
    int nbFirmesDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        waveManager = GetComponent<Wave_Manager>();
    }

    public void ReplaceHousesByFirmes(int _waveIndex)
    {
        nbFirmes = waveManager.nbFirmesOnMap;
        nbFirmesDestroyed = 0;
        allHouses = GameObject.FindGameObjectsWithTag("Maisons");
        //Cree les tableaux en fonctions du nb de firmes necéssaire
        int nbSmallFirmes = Mathf.RoundToInt(howManyFirmesPerWaves[_waveIndex].x);
        int nbMediumFirmes = Mathf.RoundToInt(howManyFirmesPerWaves[_waveIndex].y);
        int nbBigFirmes = Mathf.RoundToInt(howManyFirmesPerWaves[_waveIndex].z);

        int howManyFirms = Mathf.RoundToInt(nbSmallFirmes + nbMediumFirmes + nbBigFirmes); //nb de firmes a spawn 
        modifiedHouses = new GameObject[howManyFirms];

        int maxType;//Max type de piège debloquable pour pas avoir les boss en vague 1 quoi

        if (_waveIndex == waveManager.nbMaxWaves - 1)//Si on est a la derniere wave pour spawn le boss
        {
            maxType = waveManager.fullyUpgraded.Length + 1;
            Debug.Log(maxType);
        }
        else
        {
            maxType = waveManager.fullyUpgraded.Length;
        }

        int indexModifiedHouses = 0;

        //Selectionne et modif les maisons et PETITE firmes
        if(nbSmallFirmes > 0)
        {
            for (int x = 0; x < nbSmallFirmes; x++)
            {
                /*int randomFirmeType = Random.Range(3, maxType);//pas en dessous de 3 pcq type des pièges de bases
                if (waveManager.fullyUpgraded[randomFirmeType] == true)
                {
                    while (waveManager.fullyUpgraded[randomFirmeType] == true)
                    {
                        randomFirmeType = Random.Range(3, maxType);
                    }
                }*/

                int houseIndex = Random.Range(0, allHouses.Length - 1);
                if (allHouses[houseIndex].tag.Equals("ModifiedHouse"))
                {
                    while (allHouses[houseIndex].tag.Equals("ModifiedHouse"))
                    {
                        houseIndex = Random.Range(0, allHouses.Length - 1);
                    }
                }
                
                modifiedHouses[indexModifiedHouses] = allHouses[houseIndex];
                modifiedHouses[indexModifiedHouses].tag = "ModifiedHouse";
                modifiedHouses[indexModifiedHouses].SetActive(false);
                GameObject firmeToInstanciate = allFirmes[0];//small firme
                GameObject firmeInstance = Instantiate(firmeToInstanciate, modifiedHouses[indexModifiedHouses].transform.position, modifiedHouses[indexModifiedHouses].transform.rotation);
                //firmeInstance.GetComponent<Firme_Stats>().InitializeFirmeTest(randomFirmeType, indexModifiedHouses, waveManager, this);
                firmeInstance.GetComponent<Firme_Stats>().InitializeFirmeTest(typesOfFirmesPerWave[indexTypeArray], indexModifiedHouses, waveManager, this);
                indexTypeArray += 1;
                indexModifiedHouses += 1;
            }
        }
        //Selectionne et modif les maisons et MOYENNE firmes
        if (nbMediumFirmes > 0)
        {  
            for (int y = 0; y < nbMediumFirmes; y++)
            {
                /*int randomFirmeType = Random.Range(3, maxType);//pas en dessous de 3 pcq type des pièges de bases
                if (waveManager.fullyUpgraded[randomFirmeType] == true)
                {
                    while (waveManager.fullyUpgraded[randomFirmeType] == true)
                    {
                        randomFirmeType = Random.Range(3, maxType);
                    }
                }*/

                int houseIndex = Random.Range(0, allHouses.Length - 1);
                if (allHouses[houseIndex].tag.Equals("ModifiedHouse"))
                {
                    while (allHouses[houseIndex].tag.Equals("ModifiedHouse"))
                    {
                        houseIndex = Random.Range(0, allHouses.Length - 1);
                    }
                }

                modifiedHouses[indexModifiedHouses] = allHouses[houseIndex];
                modifiedHouses[indexModifiedHouses].tag = "ModifiedHouse";
                modifiedHouses[indexModifiedHouses].SetActive(false);
                GameObject firmeToInstanciate = allFirmes[1];//small firme
                GameObject firmeInstance = Instantiate(firmeToInstanciate, modifiedHouses[indexModifiedHouses].transform.position, modifiedHouses[indexModifiedHouses].transform.rotation);
                //firmeInstance.GetComponent<Firme_Stats>().InitializeFirmeTest(randomFirmeType, indexModifiedHouses, waveManager, this);
                firmeInstance.GetComponent<Firme_Stats>().InitializeFirmeTest(typesOfFirmesPerWave[indexTypeArray], indexModifiedHouses, waveManager, this);
                indexTypeArray += 1;
                indexModifiedHouses += 1;
            }
        }
        //Selectionne et modif les maisons et GRANDE firmes,
        if(nbBigFirmes > 0)
        {
            for (int z = 0; z < nbBigFirmes; z++)
            {
                /*int randomFirmeType = Random.Range(3, maxType);//pas en dessous de 3 pcq type des pièges de bases
                if (waveManager.fullyUpgraded[randomFirmeType] == true)
                {
                    while (waveManager.fullyUpgraded[randomFirmeType] == true)
                    {
                        randomFirmeType = Random.Range(3, maxType);
                    }
                }*/

                int houseIndex = Random.Range(0, allHouses.Length - 1);
                if (allHouses[houseIndex].tag.Equals("ModifiedHouse"))
                {
                    while (allHouses[houseIndex].tag.Equals("ModifiedHouse"))
                    {
                        houseIndex = Random.Range(0, allHouses.Length - 1);
                    }
                }

                modifiedHouses[indexModifiedHouses] = allHouses[houseIndex];
                modifiedHouses[indexModifiedHouses].tag = "ModifiedHouse";
                modifiedHouses[indexModifiedHouses].SetActive(false);
                GameObject firmeToInstanciate = allFirmes[2];//big firme
                GameObject firmeInstance = Instantiate(firmeToInstanciate, modifiedHouses[indexModifiedHouses].transform.position, modifiedHouses[indexModifiedHouses].transform.rotation);
                //firmeInstance.GetComponent<Firme_Stats>().InitializeFirmeTest(randomFirmeType, indexModifiedHouses, waveManager, this);
                firmeInstance.GetComponent<Firme_Stats>().InitializeFirmeTest(typesOfFirmesPerWave[indexTypeArray], indexModifiedHouses, waveManager, this);
                indexTypeArray += 1;
                indexModifiedHouses += 1;
            }

        }
    }

    public void RecallModifiedHouses(int _index)
    {
        nbFirmesDestroyed += 1;
        if(nbFirmesDestroyed < nbFirmes)
        {
            modifiedHouses[_index].SetActive(true);
            modifiedHouses[_index].tag = "Maisons";
            modifiedHouses[_index] = null;
        }
        else
        {
            GameObject shop = Instantiate(pfb_Shop, modifiedHouses[_index].transform.position, modifiedHouses[_index].transform.rotation);
            shop.GetComponent<Shop_Feedback>().player = GetComponent<Reward>().player;
            Destroy(modifiedHouses[_index]);
        }
    }
}
