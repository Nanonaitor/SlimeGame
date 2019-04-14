using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public ItemSpawner itemSpawner;
    public Animator anim;

    public PooledObject deathBoi;

    public override void RemoveHealth(int damageTaken)
    {
        base.RemoveHealth(damageTaken);

        anim.SetTrigger("HurtFront");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
		if (itemSpawner != null)
		{
			itemSpawner.SpawnItem(transform.position, Quaternion.identity);
		}

        var deathbody = deathBoi.GetPooledInstance<PooledObject>();
        deathbody.transform.position = transform.position;
        deathbody.transform.rotation = transform.rotation;

        ReturnToPool();
    }
}
