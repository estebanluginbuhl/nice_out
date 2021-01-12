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
    public Transform lastFirme;
    public float adjustSpeed;
    Vector3 camTranslation;
    float zPos = 0;
    public float collisionRange;
    Wave_Manager waveManager;
    void Update()
    {
        Vector3 firmeFocusPoint = transform.position + (Vector3.up * 5);

        endCam.gameObject.transform.LookAt(firmeFocusPoint);

        /*Vector3 camToFirme = firmeFocusPoint - endCam.transform.position;
        float camDistance = camToFirme.magnitude;
        Vector3 camLookDir = camToFirme.normalized;

        bool rayCollides = Physics.Raycast(endCam.transform.position, camLookDir, camDistance, rayCollisionLayer);
        Collider[] camCollides = Physics.OverlapSphere(endCam.transform.position, collisionRange, cameraCollisionLayer);

        if(rayCollides)
        {
            zPos += Time.deltaTime * adjustSpeed;
        }
        else
        {
            zPos -= Time.deltaTime * adjustSpeed;
        }

        if (camCollides.Length > 0)
        {

        }

        camTranslation = endCam.gameObject.transform.position + camLookDir * zPos;
        endCam.gameObject.transform.position = camTranslation;*/
    }

    public void StartEndWave(Transform _firme, Wave_Manager _waveManager)
    {
        lastFirme = _firme.transform;
        waveManager = _waveManager;

        Quaternion wantedCamRotation = Quaternion.Euler(lastFirme.transform.eulerAngles.x, lastFirme.transform.eulerAngles.y - 90, lastFirme.transform.eulerAngles.z);
        transform.rotation = wantedCamRotation;
        transform.position = lastFirme.transform.position + (Vector3.up * 5);
    }

    public void DestroyEndCam()
    {
        Destroy(this.gameObject);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endCam.transform.position, collisionRange);
    }
}
