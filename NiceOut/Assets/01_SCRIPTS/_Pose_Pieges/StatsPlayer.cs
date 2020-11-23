using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsPlayer : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthPercentage;
    public int energy;
    public int chargeSpeed;
    public int tempsMort;

    //Invincibility
    bool isInvincible = false;

    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI energyValue_Text;
    public TextMeshProUGUI chargeSpeed_Text;
    public Image healthBar;
    Switch_Mode leSwitch;
    float compteurTempsRecharge = 0f;

    private void Start()
    {
        energyValue_Text.text = energy.ToString();
        healthValue.text = health.ToString();
        healthPercentage = health / maxHealth;
        healthBar.rectTransform.localScale = new Vector3(healthPercentage, healthBar.rectTransform.localScale.y, healthBar.rectTransform.localScale.z);
        leSwitch = GetComponent<Switch_Mode>();
    }

    private void Update()
    {
        if(leSwitch.GetPause() == false)
        {
            if (leSwitch.mort == false)
            {
                if (compteurTempsRecharge <= 0)
                {
                    energy += chargeSpeed;
                    compteurTempsRecharge = 1;
                }
                else
                {
                    compteurTempsRecharge -= Time.deltaTime;
                }


                healthPercentage = health / maxHealth;
                UpdateHealth();
                UpdateEnergy();
                if (health <= 0)
                {
                    Death();
                }
            }
        }
    }

    public void RincePlayer(int monsterValue)
    {
        energy += monsterValue;
    }

    public void PlayerBuy(int cost)
    {
        energy -= cost;
    }

    public void Invincibility(bool _isInvincible)
    {
        isInvincible = _isInvincible;
    }

    public void DamagePlayer(int damages)
    {

        if(isInvincible == false)
        {
            health -= damages;
        }
        else
        {
            return; //Mettre un retour du dodge genre texte popup dodge
        }
    }

    public void UpdateEnergy()
    {
        energyValue_Text.text = energy.ToString();
        chargeSpeed_Text.text = "+ " + chargeSpeed.ToString() + "En/s";
    }
    public void UpdateHealth()
    {
        healthValue.text = health.ToString();
        healthBar.rectTransform.localScale = new Vector3(healthPercentage, healthBar.rectTransform.localScale.y, healthBar.rectTransform.localScale.z);
    }
    public void UpgradeHealth(int _LifeValue)
    {
        maxHealth += _LifeValue;
        health = Mathf.RoundToInt(maxHealth * healthPercentage);
    }
    public void UpgradeChargeSpeed(int value)
    {
        chargeSpeed += value;
    }

    public void Respawn()
    {
        health = maxHealth;
    }

    public void Death()
    {
        leSwitch.mort = true;
        leSwitch.cptMort = tempsMort;
    }
}
