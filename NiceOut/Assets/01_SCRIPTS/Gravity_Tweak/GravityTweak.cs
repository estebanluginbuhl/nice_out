using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTweak : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector3(0f, -75f, 0f));
    }
}