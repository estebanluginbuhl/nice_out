using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bait_Stats : MonoBehaviour
{
    public GameObject player;
    public Baits trap;
    public GameObject barOlder;
    public Image ui_UsureBar;
    public float distancePlayer;
    bool isClose;
    bool isActive;

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            float distanceTrapPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distancePlayer > distanceTrapPlayer)
            {
                if (isClose == false)
                {
                    isClose = true;
                }
            }
            else
            {
                if (isClose == true)
                {
                    isClose = false;
                }
            }
        }
        if (isClose)
        {
            if(isActive == false)
            {
                barOlder.SetActive(true);
                isActive = true;
            }
            else
            {
                ui_UsureBar.transform.localScale = new Vector3(trap.UsurePercentage, ui_UsureBar.transform.localScale.y, ui_UsureBar.transform.localScale.z);
            }
        }
        else
        {
            if (isActive == true)
            {
                barOlder.SetActive(false);
                isActive = false;
            }
        }

    }
}
