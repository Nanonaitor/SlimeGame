using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void TakeDamage(int damageTaken)
    {
        base.TakeDamage(damageTaken);

        //Call TakeDamage event for UI here :D
    }
}
