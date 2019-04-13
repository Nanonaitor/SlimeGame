using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private BulletData bulletData;
	public BulletData BulletData { get => bulletData; set => bulletData = value; }
	[SerializeField] private GameObject bulletPrefab;

	// Start is called before the first frame update
	void Start()
	{
		bulletData = new BulletData(bulletData);
		StartCoroutine(DelayedDestroyBullet());
		if (bulletData.SplitNum != 1)
			StartCoroutine(DelayedSplitBullet());
	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(Vector3.forward * bulletData.Speed * Time.deltaTime);
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

	void DestroyBullet()
	{
		Destroy(gameObject);
	}

	IEnumerator DelayedSplitBullet()
	{
		yield return new WaitForSeconds(bulletData.SplitDelay);
		float initialAngle = -bulletData.SplitAngle / 2;
		float stepAngle = bulletData.SplitAngle / (bulletData.SplitNum - 1);
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
