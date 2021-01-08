using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySmiley : MonoBehaviour
{
    public float height = 2;
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
    }
}
