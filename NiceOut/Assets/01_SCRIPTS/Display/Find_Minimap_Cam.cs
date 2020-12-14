using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find_Minimap_Cam : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("Minimap_Cam").GetComponent<Camera>();
    }
}
