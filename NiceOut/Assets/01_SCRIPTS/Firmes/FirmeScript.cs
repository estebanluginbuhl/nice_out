using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirmeScript : MonoBehaviour
{
    public float health, cooldown, reset;

    void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        Update();

        if (cooldown >= 0)
        {
            health -= damage;
            cooldown = reset;
        }
    }
}