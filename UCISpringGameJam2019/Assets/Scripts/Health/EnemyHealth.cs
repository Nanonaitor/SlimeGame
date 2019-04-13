using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void RemoveHealth(int damageTaken)
    {
        base.RemoveHealth(damageTaken);

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
