using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlecheDirectionFirme : MonoBehaviour
{
    GameObject choosenObjective;
    public float maxDist;
    public float minDist;
    public Image fleche;
    public GameObject centre;
    public GameObject player;
    public GameObject billy;
    float currentdist;
    // Start is called before the first frame update
    void Start()
    {
        fleche.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float minDistance = Mathf.Infinity;
        GameObject[] firmeTarget = GameObject.FindGameObjectsWithTag("Objectives");
        for (int i = 0; i < firmeTarget.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, firmeTarget[i].transform.position);
            if (dist < minDistance)
            {
                choosenObjective = firmeTarget[i];
                minDistance = dist;
            }
        }

        if (choosenObjective != null)
        {
            currentdist = Vector3.Distance(player.transform.position, choosenObjective.transform.position);

            if (currentdist <= maxDist && currentdist >= minDist)
            {

                fleche.gameObject.SetActive(true);
                billy.transform.LookAt(choosenObjective.transform.position);

                float rotY = billy.transform.localEulerAngles.y;
                centre.transform.rotation = Quaternion.Euler(0, 0, -rotY);
                fleche.transform.localScale = new Vector3((currentdist / maxDist), (currentdist / maxDist), (currentdist / maxDist));
            }
            else
            {
                fleche.gameObject.SetActive(false);
            }
        }

    }
}
