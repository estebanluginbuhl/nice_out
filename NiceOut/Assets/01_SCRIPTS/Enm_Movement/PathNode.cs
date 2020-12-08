using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public static Transform[] nodeTransform;

    void Awake()
    {
        nodeTransform = new Transform[transform.childCount];
        for (int i = 0; i < nodeTransform.Length; i++)
        {
            nodeTransform[i] = transform.GetChild(i);
        }
    }
}