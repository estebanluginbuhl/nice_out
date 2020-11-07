using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    public int health = 20;
    public int gold = 35;

    private void Update()
    {
        if(health <= 0)
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

    public void DamagePlayer(int damages)
    {
        health -= damages;
    }

    public void Death()
    {
        Debug.Log("death");
        Application.Quit();
    }
}
