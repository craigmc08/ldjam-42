using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHealth : MonoBehaviour {
    float maxHealth = 50f;
    float health = 50f;

    public float passiveHeal = 0f;

    public bool Damage(float amount)
    {
        health -= Mathf.Abs(amount);

        // I don't really like how this looks, replace with particle system?
        //GameController.damageIndicator.DisplayDamage(transform.position, (int) amount);

        if (Health <= 0)
        {
            Destroy(gameObject);
            return true;
        } else
        {
            return false;
        }
    }
    public void Heal(float amount)
    {
        health += Mathf.Abs(amount);
        CapHealth();

        // Display heal indicator
        // Same way as damage indicator, i guess
    }

    void Update()
    {
        health += passiveHeal * Time.deltaTime;
        CapHealth();
    }

    void CapHealth()
    {
        if (health > maxHealth) health = maxHealth;
    }

    public int Health
    {
        get
        {
            return (int) health;
        }
    }
    public float HealthPercent
    {
        get
        {
            return health / maxHealth;
        }
    }
}
