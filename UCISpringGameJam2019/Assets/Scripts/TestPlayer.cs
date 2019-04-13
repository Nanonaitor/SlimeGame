using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
	[SerializeField]
	private GameObject bullet;
	private float fireDelay;
	private float fireTimer;

    // Start is called before the first frame update
    void Start()
    {
		fireDelay = 1f;
		fireTimer = fireDelay;
		//Instantiate(bullet, transform.position, transform.rotation);
	}

    // Update is called once per frame
    void Update()
    {
		fireTimer -= Time.deltaTime;
		if (fireTimer <= 0)
		{
			Instantiate(bullet, transform.position, transform.rotation);
			//Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(20,0,0));
			//Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(-20, 0, 0));
			fireTimer = fireDelay;
		}
	}
}
