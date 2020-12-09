using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Trap_Preview_Timer : MonoBehaviour
{
    public Traps trap;
    [SerializeField]
    TextMeshProUGUI cooldown;
    [SerializeField]
    Image jauge;
    float percentage;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        percentage = (trap.cooldownCountdown / trap.cooldownSpawn[trap.upgradeIndex]);
        cooldown.text = Mathf.FloorToInt(trap.cooldownCountdown) + "s";
        jauge.fillAmount = percentage;
    }
}
