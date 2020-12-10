using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Boost : MonoBehaviour
{
    public float boostValue;
    public float boostTime;
    public float respawnTime;

    float oldValue;
    float oldTime;
    GameObject child;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        oldValue = boostValue;
        oldTime = boostTime;
    }

    public void StartRespawn()
    {
        StartCoroutine(BoostRespawn());
    }

     IEnumerator BoostRespawn()
    {
        child.SetActive(false);
        boostValue = 0;
        boostTime = 0;

        yield return new WaitForSeconds(respawnTime);

        child.SetActive(true);
        boostValue = oldValue;
        boostTime = oldTime;
    }
}
