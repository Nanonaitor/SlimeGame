using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private BulletData bulletData;
	public BulletData BulletData { get => bulletData; set => bulletData = value; }
	[SerializeField] private GameObject bulletPrefab;
	private Vector3 bulletDirection;

	// Start is called before the first frame update
	void Start()
	{
		bulletData = new BulletData(bulletData);
		StartCoroutine(DelayedDestroyBullet());
		if (bulletData.SplitNum != 1 && bulletData.SplitLives != 0)
			StartCoroutine(DelayedSplitBullet());

		bulletDirection = Vector3.forward;
	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(bulletDirection * bulletData.Speed * Time.deltaTime);
	}

	private void FixedUpdate()
	{
		LayerMask mask = LayerMask.GetMask("Environment");
		RaycastHit hit;
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * (Time.deltaTime * bulletData.Speed + 1.2f), Color.blue);
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Time.deltaTime * bulletData.Speed + 1.2f, mask))
		{
			Vector3 reflectDir = Vector3.Reflect(transform.TransformDirection(Vector3.forward), hit.normal);
			float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
			transform.eulerAngles = new Vector3(0, rot, 0);
		}
	}

	void OnEnable()
	{
		OnBulletDestroyed += DestroyBullet;
	}

	void OnDisable()
	{
		OnBulletDestroyed -= DestroyBullet;
	}

	public delegate void OnBulletDestroy();

	public event OnBulletDestroy OnBulletDestroyed;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CheckLayer("Environment"))
		{
			//Destroy(gameObject);
		}
		//other.gameObject.ApplyDamage(BulletData.Damage);
		//if (!bulletData.IsPiercing)
		//	Destroy(gameObject);
	}

	void DestroyBullet()
	{
		Destroy(gameObject);
	}

	IEnumerator DelayedSplitBullet()
	{
		yield return new WaitForSeconds(bulletData.SplitDelay);
		float initialAngle = -bulletData.SplitAngle / 2;
		float stepAngle = bulletData.SplitAngle / (bulletData.SplitNum - 1);
		bulletData.SplitLives -= 1;
		for (int i = 0; i < bulletData.SplitNum; ++i)
		{
			GameObject bulletObject = Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, initialAngle + i * stepAngle, 0));
			Bullet bullet = bulletObject.GetComponent<Bullet>();
			bullet.BulletData = new BulletData(bulletData);
		}
		OnBulletDestroyed();
	}

	IEnumerator DelayedDestroyBullet()
	{
		yield return new WaitForSeconds(bulletData.DestroyDelay);
		OnBulletDestroyed();
	}
}
