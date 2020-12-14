using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Little_Turn : MonoBehaviour
{
    [SerializeField]
    Vector2 minMaxRot = new Vector2(-120, -180);
    public Transform turnPoint;
    public float angle, speed;
    // Start is called before the first frame update
    bool inOut;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inOut)
        {
            angle -= Time.deltaTime * speed;
            transform.RotateAround(turnPoint.position, Vector3.up, Time.deltaTime * -speed);
        }
        else
        {
            angle += Time.deltaTime * speed;
            transform.RotateAround(turnPoint.position, Vector3.up, Time.deltaTime * speed);
        }

        if(angle <= minMaxRot.y)
        {
            if (inOut == true)
                inOut = false;
        }

        if(angle > minMaxRot.x)
        {
            if(inOut == false)
                inOut = true;
        }
    }
}
