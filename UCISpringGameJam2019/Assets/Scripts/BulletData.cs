using UnityEngine;

[System.Serializable]
public struct BulletData
{
	public int Damage;
	public float Speed;
	public int SplitNum;
	public float SplitAngle;
	public float SplitDelay;
	public float DestroyDelay;

	public BulletData(BulletData bulletData)
	{
		Damage = bulletData.Damage;
		Speed = bulletData.Speed;
		SplitNum = bulletData.SplitNum;
		SplitAngle = bulletData.SplitAngle;
		SplitDelay = bulletData.SplitDelay;
		DestroyDelay = bulletData.DestroyDelay;
	}
}