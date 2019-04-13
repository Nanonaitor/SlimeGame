using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
	[SerializeField] private int health;
	[SerializeField] private int maxHealth;

    public int CurrentHealth { get => health; set => health = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    void Start()
    {
		health = maxHealth;
    }

    public virtual void RemoveHealth(int damageTaken)
	{
		health -= damageTaken;
		if (health < 0)
			health = 0;
	}

	public virtual void AddHealth(int damageTaken)
	{
		health += damageTaken;
		if (health > maxHealth)
			health = maxHealth;
	}

	public virtual void SetHealth(int newHealth)
    {
        health = newHealth;
    }
}
