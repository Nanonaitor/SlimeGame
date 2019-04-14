using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public ItemSpawner itemSpawner;
    public Animator anim;

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
        itemSpawner.SpawnItem(transform.position, Quaternion.identity);
        ReturnToPool();
    }
}
