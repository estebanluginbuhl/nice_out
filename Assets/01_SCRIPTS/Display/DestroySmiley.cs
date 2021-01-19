using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySmiley : MonoBehaviour
{
    public float height = 2;
    public float randomizerTreshold = 0.5f;
    Vector3 offset;
    public float destroyTime = 1f;

    void Awake()
    {
        offset = new Vector3(0, height, 0);
    }

    void Start()
    {
        Destroy(gameObject, destroyTime);

        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-randomizerTreshold, randomizerTreshold), Random.Range(-randomizerTreshold, randomizerTreshold), Random.Range(-randomizerTreshold, randomizerTreshold));
    }
}
