using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public RectTransform healthbar;

    void UpdateHealthUI(int health, int maxHealth)
    {
        float healthPercent = (float)health / (float)maxHealth;
        healthbar.localScale = new Vector3(healthPercent, 1, 1);
    }

    public override void RemoveHealth(int damageTaken)
    {
        base.RemoveHealth(damageTaken);
        
        if(CurrentHealth <= 0)
        {
            //GameOver Logic
            SetHealth(0);
            Debug.Log("GameOver");
        }

        UpdateHealthUI(CurrentHealth, MaxHealth);
    }
}
