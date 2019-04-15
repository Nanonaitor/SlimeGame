using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public RectTransform healthbar;
    public Animator anim;

    void UpdateHealthUI(int health, int maxHealth)
    {
        float healthPercent = (float)health / (float)maxHealth;
        healthbar.localScale = new Vector3(healthPercent, 1, 1);
    }

    public override void RemoveHealth(int damageTaken)
    {
        base.RemoveHealth(damageTaken);

        anim.SetTrigger("HurtFront");
        
        if(CurrentHealth <= 0)
        {
            GameOver();
        }

        UpdateHealthUI(CurrentHealth, MaxHealth);
    }

	public override void AddHealth(int damageTaken)
	{
		base.AddHealth(damageTaken);
		UpdateHealthUI(CurrentHealth, MaxHealth);
	}

    void GameOver()
    {
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        foreach (var item in enemies)
        {
            item.Die();
        }

        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        spawner.canSpawn = false;

        SceneManager.LoadScene("GameOverScreen");
    }
}
