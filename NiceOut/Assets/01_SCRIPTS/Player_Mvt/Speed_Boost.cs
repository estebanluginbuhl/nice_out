using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Boost : MonoBehaviour
{
    
    public float boostValue;
    public float boostTime;
    [SerializeField]
    float respawnTime;
    bool spawned = false;
    float cptRespawn;
    GameObject child;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        spawned = true;
    }

    private void Update()
    {
        if (cptRespawn > 0)
        {
            cptRespawn -= Time.deltaTime;
        }
        else
        {
            if(spawned == false)
            {
                Respawn();
            }
        }
    }

    public void StartRespawn()
    {
        spawned = false;
        cptRespawn = respawnTime;
        child.SetActive(false);
    }
    void Respawn()
    {
        spawned = true;
        child.SetActive(true);
    }
}
