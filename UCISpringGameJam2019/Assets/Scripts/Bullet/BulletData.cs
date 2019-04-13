using UnityEngine;

[System.Serializable]
public struct BulletData
{
	public int Damage;
    public Vector3 BulletDirection;
	public float Speed;
	public float bulletSize;
	public int SplitNum;
	public int SplitLives;
	public float SplitAngle;
	public float SplitDelay;
	public float DestroyDelay;
	public bool CanPierce;
	public bool CanBounce;
	public Health HealthTarget;
	public int LeachAmount;
	public bool CanExplode;
	public float ExplosionRadius;

	public BulletData(BulletData bulletData)
	{
		Damage = bulletData.Damage;
        BulletDirection = bulletData.BulletDirection;
		Speed = bulletData.Speed;
		bulletSize = bulletData.bulletSize;
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
	}
}