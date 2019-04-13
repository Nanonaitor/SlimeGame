using UnityEngine;

[System.Serializable]
public struct BulletData
{
	public int Damage;
	public float Speed;
	public int SplitNum;
	public int SplitLives;
	public float SplitAngle;
	public float SplitDelay;
	public float DestroyDelay;
	public bool IsPiercing;

	public BulletData(BulletData bulletData)
	{
		Damage = bulletData.Damage;
		Speed = bulletData.Speed;
		SplitNum = bulletData.SplitNum;
		SplitLives = bulletData.SplitLives;
		SplitAngle = bulletData.SplitAngle;
		SplitDelay = bulletData.SplitDelay;
		DestroyDelay = bulletData.DestroyDelay;
		IsPiercing = bulletData.IsPiercing;
	}
}