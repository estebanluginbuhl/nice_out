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
    MeshRenderer child;

    private void Start()
    {
        child = GetComponentInChildren<MeshRenderer>();
        oldValue = boostValue;
        oldTime = boostTime;
    }

    public void StartRespawn()
    {
        StartCoroutine(BoostRespawn());
    }

     IEnumerator BoostRespawn()
    {
        child.enabled = false;
        boostValue = 0;
        boostTime = 0;

        yield return new WaitForSeconds(respawnTime);

        child.enabled = true;
        boostValue = oldValue;
        boostTime = oldTime;
    }
}
