using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Artisan_Stats : MonoBehaviour
{
    [SerializeField]
    Image healthBar;
    float artisanHealth;
    public float artisanMaxHealth;
    // Update is called once per frame
    void Start()
    {
        artisanHealth = artisanMaxHealth;
    }
    void Update()
    {
        healthBar.fillAmount = artisanHealth / artisanMaxHealth;
    }

    public void DamageArtisan(int _damages)
    {
        artisanHealth -= _damages;
    }

    public void UpgradeArtisanHealth(float _upgradePercentage)
    {
        artisanHealth = artisanHealth * _upgradePercentage;
        artisanMaxHealth = artisanMaxHealth * _upgradePercentage;
    }
}
