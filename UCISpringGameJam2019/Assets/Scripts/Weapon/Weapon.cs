using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PooledObject bullet;
    [SerializeField] private Transform shootPoint;

    float shootTimer;

	[SerializeField] private WeaponData initialWeaponData;
	[SerializeField] private WeaponData currentWeaponData;
	public WeaponData WeaponData { get => initialWeaponData; set => initialWeaponData = value; }
	public List<Sprite> ItemIcons { get => itemIcons; set => itemIcons = value; }

	private List<WeaponData> items = new List<WeaponData>();

	private List<Sprite> itemIcons = new List<Sprite>();

	private void Start()
    {
		initialWeaponData = new WeaponData(initialWeaponData);
		currentWeaponData = WeaponData;
        shootTimer = currentWeaponData.AttackSpeed;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
    }

    public void Shoot()
    {
        if(shootTimer >= currentWeaponData.AttackSpeed)
        {
            shootTimer = 0;

            var projectile = bullet.GetPooledInstance<PooledObject>();
			projectile.gameObject.transform.localScale = currentWeaponData.InitialBulletSize * currentWeaponData.BulletSize;
			projectile.gameObject.transform.position = shootPoint.transform.position;
            projectile.gameObject.transform.rotation = shootPoint.transform.rotation;

            Bullet newBullet = projectile.GetComponent<Bullet>();

            newBullet.BulletPrefab = bullet;
            newBullet.InitBullet(currentWeaponData);
            newBullet.StartBullet();
        }
    }

	public void AddItem(WeaponData item)
	{
		items.Add(item);
		UpdateStats();
	}

	private void UpdateStats()
	{
		currentWeaponData = initialWeaponData;
		foreach (WeaponData item in items)
		{
			currentWeaponData.Damage += item.Damage;
			currentWeaponData.AttackSpeed *= item.AttackSpeed;
			currentWeaponData.BulletSpeed *= item.BulletSpeed;
			currentWeaponData.BulletSize *= item.BulletSize;
			currentWeaponData.SplitNum += item.SplitNum;
			currentWeaponData.SplitLives += item.SplitLives;
			currentWeaponData.SplitAngle += item.SplitAngle;
			currentWeaponData.LeachAmount += item.LeachAmount;
			if (item.CanPierce)
			{
				currentWeaponData.CanPierce = true;
				currentWeaponData.PierceNum += item.PierceNum;
			}
			if (item.CanBounce)
			{
				currentWeaponData.CanBounce = true;
				currentWeaponData.BounceNum += item.BounceNum;
			}
			if (item.CanExplode)
			{
				currentWeaponData.CanExplode = true;
				currentWeaponData.ExplosionRadius *= item.ExplosionRadius;
			}
			if (item.CanHome)
			{
				currentWeaponData.CanHome = true;
				currentWeaponData.HomingRadius *= item.HomingRadius;
				currentWeaponData.HomingStrength *= item.HomingStrength;
			}
			currentWeaponData.SpiralStrength += item.SpiralStrength;
		}
	}
}
