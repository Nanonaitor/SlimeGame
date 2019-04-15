using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributeItem : MonoBehaviour
{
	[SerializeField] private float movementSpeed = 1;
	[SerializeField] private int healthToAdd = 20;
	[SerializeField] private Sprite itemIcon;

	public Sprite ItemIcon { get => itemIcon; set => itemIcon = value; }

	// Start is called before the first frame update
	void Start()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CheckLayer("Player"))
		{
			other.gameObject.GetComponent<Player>().Motor.Speed *= movementSpeed;
			other.gameObject.GetComponent<PlayerHealth>().MaxHealth += healthToAdd;
			other.gameObject.GetComponent<PlayerHealth>().AddHealth(0);
			other.GetComponentInChildren<Weapon>().ItemIcons.Add(ItemIcon);
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0));
	}
}
