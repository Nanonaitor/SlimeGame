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

    public virtual void TakeDamage(int damageTaken)
	{
		health -= damageTaken;
	}

    public virtual void SetHealth(int newHealth)
    {
        health = newHealth;
    }
}
