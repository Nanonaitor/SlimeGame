using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] private WeaponData weaponData;
	public WeaponData BulletData { get => weaponData; set => weaponData = value; }

	private void Start()
	{
		weaponData = new WeaponData(weaponData);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CheckLayer("Player"))
		{
			other.GetComponentInChildren<Weapon>().AddItem(weaponData);
		}
		Destroy(gameObject);
	}
}
