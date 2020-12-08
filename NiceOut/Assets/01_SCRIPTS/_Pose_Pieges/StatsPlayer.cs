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
    public int gold;
    public int chargeSpeed;
    public int tempsMort;

    //Invincibility
    bool isInvincible = false;

    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI goldValue_Text;
    public TextMeshProUGUI chargeSpeed_Text;
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
        if(leSwitch.GetPause() == false)
        {
            if (leSwitch.mort == false)
            {
                if (compteurTempsRecharge <= 0)
                {
                    gold += chargeSpeed;
                    compteurTempsRecharge = 1;
                }
                else
                {
                    compteurTempsRecharge -= Time.deltaTime;
                }
            }
        }
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
        chargeSpeed_Text.text = "+ " + chargeSpeed.ToString() + "En/s";
    }
    public void UpdateHealth()
    {
        healthValue.text = health.ToString();
        healthBar.rectTransform.localScale = new Vector3(healthPercentage, healthBar.rectTransform.localScale.y, healthBar.rectTransform.localScale.z);
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
    }
}
