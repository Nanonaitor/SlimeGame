using UnityEngine;

[System.Serializable]
public struct WeaponData
{
	[Header("Weapon Stats")]
	public int Damage;
    [HideInInspector] public Vector3 BulletDirection;
	public float AttackSpeed;
	public float BulletSpeed;
	public float BulletSize;
	[Header("Fractal")]
	public int SplitNum;
	public int SplitLives;
	public float SplitAngle;
	public float SplitDelay;
	[Header("Piercing")]
	public bool CanPierce;
	[Header("Bouncing")]
	public bool CanBounce;
	[Header("LifeLeach")]
	public Health HealthTarget;
	public int LeachAmount;
	[Header("EXXUUPLOSION")]
	public bool CanExplode;
	public float ExplosionRadius;
	[Header("Homing")]
	public bool CanHome;
	public Transform HomingTarget;
	public float HomingRadius;
	public float HomingStrength;
	[Header("Spiral")]
	public float SpiralStrength;
	public float DestroyDelay;

	public WeaponData(WeaponData bulletData)
	{
		Damage = bulletData.Damage;
        BulletDirection = bulletData.BulletDirection;
		AttackSpeed = bulletData.AttackSpeed;
		BulletSpeed = bulletData.BulletSpeed;
		BulletSize = bulletData.BulletSize;
		SplitNum = bulletData.SplitNum;
		SplitLives = bulletData.SplitLives;
		SplitAngle = bulletData.SplitAngle;
		SplitDelay = bulletData.SplitDelay;
		DestroyDelay = bulletData.DestroyDelay;
		CanPierce = bulletData.CanPierce;
        CanBounce = bulletData.CanBounce;
		HealthTarget = bulletData.HealthTarget;
		LeachAmount = bulletData.LeachAmount;
		CanExplode = bulletData.CanExplode;
		ExplosionRadius = bulletData.ExplosionRadius;
		CanHome = bulletData.CanHome;
		HomingTarget = bulletData.HomingTarget;
		HomingRadius = bulletData.HomingRadius;
		HomingStrength = bulletData.HomingStrength;
		SpiralStrength = bulletData.SpiralStrength;
	}
}