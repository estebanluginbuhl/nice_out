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
    public int gold = 35;

    //Invincibility
    bool isInvincible = false;

    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI goldValue;
    public Image healthBar;

    private void Start()
    {
        goldValue.text = gold.ToString();
        healthValue.text = health.ToString();
        healthPercentage = health / maxHealth;
        healthBar.rectTransform.localScale = new Vector3(healthPercentage, healthBar.rectTransform.localScale.y, healthBar.rectTransform.localScale.z);
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

    public void UpdateGold()
    {
        goldValue.text = gold.ToString();
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


    public void Death()
    {
        Debug.Log("death");
        Application.Quit();
    }
}
