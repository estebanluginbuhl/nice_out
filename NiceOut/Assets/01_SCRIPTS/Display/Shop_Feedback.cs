using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Feedback : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    GameObject shopTexts;
    [SerializeField]
    GameObject shopOpenInfo;
    Animator anm;
    // Update is called once per frame
    private void Start()
    {
        anm = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if(player!= null)
        {
            anm.SetFloat("DistancePlayer", Vector3.Distance(player.transform.position, transform.position));
            if (Vector3.Distance(player.transform.position, transform.position) < 8)
            {
                shopOpenInfo.SetActive(true);
            }
            else
            {
                shopOpenInfo.SetActive(false);
            }
        }
        else
        {
            anm.SetFloat("DistancePlayer", 10);
        }    }
}
