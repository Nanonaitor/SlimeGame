using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
	void RemoveHealth(int damageTaken);
	void AddHealth(int damageTaken);
	void SetHealth(int newHealth);
}

public static class DamageHelper
{
	public static void ApplyDamage(this GameObject receiver, int damage)
	{
		var receivers = receiver.GetComponents<IDamageable>();
		if (receivers != null)
			for (int i = 0; i < receivers.Length; i++)
				receivers[i].RemoveHealth(damage);
	}
}