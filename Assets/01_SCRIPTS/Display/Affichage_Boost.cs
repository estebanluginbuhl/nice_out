using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Affichage_Boost : MonoBehaviour
{
    [SerializeField]
    Image boostImage;
    [SerializeField]
    GameObject boostDisplay;
    [SerializeField]
    TextMeshProUGUI boostTimeText;

    float fullCooldown;
    float cptCooldown;
    float cptText;

    bool displayBoost;

    private void Start()
    {
        boostDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (displayBoost)
        {
            if (cptCooldown < fullCooldown)
            {
                cptCooldown += Time.deltaTime;
            }
            if (cptText > 0)
            {
                cptText -= Time.deltaTime;
            }
            boostTimeText.text = Mathf.CeilToInt(cptText).ToString();
            boostImage.fillAmount = cptCooldown / fullCooldown;
        }
    }

    public void StartBoostDisplay(float _cd)
    {
        displayBoost = true;
        fullCooldown = _cd;
        cptCooldown = 0;
        cptText = _cd;
        boostDisplay.SetActive(true);
    }
    public void StopBoostDisplay()
    {
        displayBoost = false;
        boostDisplay.SetActive(false);
    }
}
