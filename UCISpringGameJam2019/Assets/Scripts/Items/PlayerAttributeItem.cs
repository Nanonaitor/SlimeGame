using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributeItem : MonoBehaviour
{
	[SerializeField] private float movementSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CheckLayer("Player"))
		{
			other.GetComponentInChildren<Player>().Motor.Speed *= movementSpeed;
		}
		Destroy(gameObject);
	}

	private void Update()
	{
		transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0));
	}
}
