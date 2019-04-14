using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[Header("Weapon Stats")]
	[SerializeField] private int damage;
	[SerializeField] private float attackSpeed;
	[SerializeField] private float bulletSpeed;
	[Header("Fractal")]
	[SerializeField] private int splitNum;
	[SerializeField] private float splitDelay;
	[SerializeField] private int splitLives;
	[SerializeField] private float splitAngle;
	[Header("Piercing")]
	[SerializeField] private bool canPierce;
	[Header("Bouncing")]
	[SerializeField] private bool canBounce;
	[Header("LifeLeach")]
	[SerializeField] private Health healthTarget;
	[SerializeField] private int leachAmount;
	[Header("EXXUUPLOSION")]
	[SerializeField] private bool canExplode;
	[SerializeField] private float explosionRadius;
	[Header("Homing")]
	[SerializeField] private bool canHome;
	[SerializeField] private Transform homingTarget;
	[SerializeField] private float homingRadius;
	[SerializeField] private float homingStrength;
	[Header("Spiral")]
	[SerializeField] private float spiralStrength;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("trigger");
		if (other.CheckLayer("Player"))
		{
			other.GetComponentInChildren<Weapon>().CanBounce = true;
		}
		Destroy(gameObject);
	}
}
