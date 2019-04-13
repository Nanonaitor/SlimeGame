using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	[SerializeField] private int health;
	[SerializeField] private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
		health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void TakeDamage(int damageTaken)
	{
		health -= damageTaken;
		if (health <= 0)
		{
			Destroy(gameObject);
			// rip
		}
	}
}
