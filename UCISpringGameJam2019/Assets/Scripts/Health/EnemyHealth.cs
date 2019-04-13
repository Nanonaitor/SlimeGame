using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void TakeDamage(int damageTaken)
    {
        base.TakeDamage(damageTaken);

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
