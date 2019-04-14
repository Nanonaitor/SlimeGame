using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] private Sprite itemIcon;
	[SerializeField] private WeaponData weaponData;
	public WeaponData BulletData { get => weaponData; set => weaponData = value; }
	public Sprite ItemIcon { get => itemIcon; set => itemIcon = value; }

	private void Start()
	{
		weaponData = new WeaponData(weaponData);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CheckLayer("Player"))
		{
			other.GetComponentInChildren<Weapon>().AddItem(weaponData);
			other.GetComponentInChildren<Weapon>().ItemIcons.Add(ItemIcon);
		}
		Destroy(gameObject);
	}

	private void Update()
	{
		transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0));		
	}
}
