using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthPercentage;
    public int gold;
    public int goldPerRateAmount;
    public float goldRate;
    public float goldCpt;

    //Invincibility
    bool isInvincible = false;

    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI goldValue_Text;
    public Image healthBar;
    Switch_Mode leSwitch;
    float compteurTempsRecharge = 0f;
    [SerializeField]
    ParticleSystem getDamaged;

    Camera cam;
    private void Start()
    {
        getDamaged.Stop();
        goldValue_Text.text = gold.ToString();
        healthValue.text = health.ToString();
        healthPercentage = health / maxHealth;
        healthBar.rectTransform.localScale = new Vector3(healthPercentage, healthBar.rectTransform.localScale.y, healthBar.rectTransform.localScale.z);
        leSwitch = GetComponent<Switch_Mode>();
        cam = Camera.main;
    }

    private void Update()
    {
        healthPercentage = health / maxHealth;
        UpdateHealth();
        UpdateGold();
        if (health <= 0)
        {
            Death();
        }
        if (goldCpt <= 0)
        {
            RincePlayer(goldPerRateAmount);
            goldCpt = goldRate;
        }
        goldCpt -= Time.deltaTime;
    }

    public void RincePlayer(int monsterValue)
    {
        gold += monsterValue;
    }

    public void PlayerBuy(int cost)
    {
        gold -= cost;
    }

    public void Invincibility(bool _isInvincible)
    {
        isInvincible = _isInvincible;
    }

    public void DamagePlayer(int _takenDamages) 
    {
        if(isInvincible == false)
        {
            health -= _takenDamages;
            getDamaged.Play();
            cam.GetComponent<Camera_Controller>().shake = true;
        }
        else
        {
            return; //Mettre un retour du dodge genre texte popup dodge
        }
    }

    public void UpdateGold()
    {
        goldValue_Text.text = gold.ToString();
    }
    public void UpdateHealth()
    {
        if(health >= 0)
        {
            healthValue.text = health.ToString();
            healthBar.rectTransform.localScale = new Vector3(healthPercentage, healthBar.rectTransform.localScale.y, healthBar.rectTransform.localScale.z);
        }
    }

    public void Respawn()
    {
        health = maxHealth;
    }

    public void Death()
    {
        leSwitch.ui_DeathPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        leSwitch.mort = true;
        leSwitch.realPause = true;
        leSwitch.pause = true;
    }
}
