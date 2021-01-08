using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEndWave : MonoBehaviour
{
    [SerializeField]
    LayerMask cameraCollisionLayer = -1;
    [SerializeField]
    LayerMask rayCollisionLayer = -1;
    public Camera endCam;
    public GameObject lastFirme;
    public float adjustSpeed;
    float endTimer;
    public float endTiming;
    bool startEnd;
    Vector3 camTranslation;
    float zPos = 0;
    float collisionRange;
    Wave_Manager waveManager;
    void Update()
    {
        if(startEnd == true)
        {
            endTimer -= Time.deltaTime;
            if(endTimer<= 0)
            {
                waveManager.EndWave();
            }
        }

        endCam.gameObject.transform.LookAt(transform.position);

        Vector3 camToFirme = transform.position - endCam.transform.position;
        float camDistance = camToFirme.magnitude;
        Vector3 camLookDir = camToFirme.normalized;

        bool rayCollides = Physics.Raycast(endCam.transform.position, camLookDir, camDistance, rayCollisionLayer);
        Collider[] camCollides = Physics.OverlapSphere(endCam.transform.position, collisionRange, cameraCollisionLayer);

        if(rayCollides || camCollides.Length > 0)
        {
            zPos += Time.deltaTime * adjustSpeed;
        }
        else
        {
            zPos -= Time.deltaTime * adjustSpeed;
        }

        camTranslation = endCam.gameObject.transform.position + new Vector3(0, 0, zPos);
        endCam.gameObject.transform.position = camTranslation;
    }

    public void StartEndWave(GameObject _firme, Wave_Manager _waveManager)
    {
        lastFirme = _firme;
        waveManager = _waveManager;
        transform.rotation = lastFirme.transform.rotation;
        transform.position = lastFirme.transform.position + (Vector3.up * (lastFirme.transform.localScale.y / 2));
        endTimer = endTiming;
        startEnd = true;
    }
}
