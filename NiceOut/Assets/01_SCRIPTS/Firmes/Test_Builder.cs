using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Builder : MonoBehaviour
{
    Wave_Manager waveManger;
    int nbFirmes;
    GameObject[] allHouses;
    public GameObject[] allFirmes;
    public GameObject pfb_Shop;
    GameObject[] modifiedHouses;
    
    int nbFirmesDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        waveManger = GetComponent<Wave_Manager>();
    }

    public void ReplaceHousesByFirmes(int _howManyFirmes)
    {
        nbFirmes = waveManger.nbFirmesOnMap;
        nbFirmesDestroyed = 0;
        allHouses = GameObject.FindGameObjectsWithTag("Maisons");
        //Cree les tableaux en fonctions du nb de firmes necéssaire
        modifiedHouses = new GameObject[_howManyFirmes];

        //Selectionne et modif les maisons
        for (int i = 0; i < _howManyFirmes; i++)
        {
            int randomFirmeIndex = Random.Range(0, allFirmes.Length - 1);

            int houseIndex = Random.Range(0, allHouses.Length - 1);
            if (allHouses[houseIndex].tag.Equals("ModifiedHouse"))
            {
                while (allHouses[houseIndex].tag.Equals("ModifiedHouse"))
                {
                    houseIndex = Random.Range(0, allHouses.Length - 1);
                }
            }
            modifiedHouses[i] = allHouses[houseIndex];
            modifiedHouses[i].tag = "ModifiedHouse";
            modifiedHouses[i].SetActive(false);
            GameObject firmeToInstanciate = allFirmes[randomFirmeIndex];
            GameObject firmeInstance = Instantiate(firmeToInstanciate, modifiedHouses[i].transform.position, modifiedHouses[i].transform.rotation);
            firmeInstance.GetComponent<Firme_Stats>().InitializeFirmeTest(2, i, waveManger, this);
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
            Destroy(modifiedHouses[_index]);
        }
    }
}
