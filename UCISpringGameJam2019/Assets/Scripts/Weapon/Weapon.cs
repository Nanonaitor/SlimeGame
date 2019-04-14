using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PooledObject bullet;
    [SerializeField] private Transform shootPoint;

    float shootTimer;

	[SerializeField] private WeaponData bulletData;
	public WeaponData BulletData { get => bulletData; set => bulletData = value; }

	private void Start()
    {
		bulletData = new WeaponData(bulletData);
        shootTimer = bulletData.AttackSpeed;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
    }

    public void Shoot()
    {
        if(shootTimer >= bulletData.AttackSpeed)
        {
            shootTimer = 0;

            var projectile = bullet.GetPooledInstance<PooledObject>();
            projectile.gameObject.transform.position = shootPoint.transform.position;
            projectile.gameObject.transform.rotation = shootPoint.transform.rotation;

            Bullet newBullet = projectile.GetComponent<Bullet>();

            newBullet.BulletPrefab = bullet;
            newBullet.InitBullet(bulletData);
            newBullet.StartBullet();
        }
    }
}
