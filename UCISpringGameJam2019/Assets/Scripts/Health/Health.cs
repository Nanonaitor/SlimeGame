﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
	[SerializeField] private int health;
	[SerializeField] private int maxHealth;

    void Start()
    {
		health = maxHealth;
    }

	public void TakeDamage(int damageTaken)
	{
		health -= damageTaken;
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
}