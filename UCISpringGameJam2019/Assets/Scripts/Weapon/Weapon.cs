using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PooledObject bullet;
    [SerializeField] private Transform shootPoint;

    [Header("Weapon Stats")]
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float bulletSpeed;
    [Header("Fractal")]
    [SerializeField] private int splitNum;
    [SerializeField] private float splitDelay;
    [SerializeField] private int splitLives;
    [SerializeField] private float splitAngle;
    [Header("Piercing")]
    [SerializeField] private bool canPierce;
    [Header("Bouncing")]
    [SerializeField] private bool canBounce;
	[Header("LifeLeach")]
	[SerializeField] private Health healthTarget;
	[SerializeField] private int leachAmount;
	[Header("EXXUUPLOSION")]
	[SerializeField] private bool canExplode;
	[SerializeField] private float explosionRadius;
	[Header("Homing")]
	[SerializeField] private bool canHome;
	[SerializeField] private Transform homingTarget;
	[SerializeField] private float homingRadius;
	[SerializeField] private float homingStrength;
	[Header("Spiral")]
	[SerializeField] private float spiralStrength;

    float shootTimer;

	public int Damage { get => damage; set => damage = value; }
	public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
	public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
	public int SplitNum { get => splitNum; set => splitNum = value; }
	public float SplitDelay { get => splitDelay; set => splitDelay = value; }
	public int SplitLives { get => splitLives; set => splitLives = value; }
	public float SplitAngle { get => splitAngle; set => splitAngle = value; }
	public bool CanPierce { get => canPierce; set => canPierce = value; }
	public bool CanBounce { get => canBounce; set => canBounce = value; }
	public Health HealthTarget { get => healthTarget; set => healthTarget = value; }
	public int LeachAmount { get => leachAmount; set => leachAmount = value; }
	public bool CanExplode { get => canExplode; set => canExplode = value; }
	public float ExplosionRadius { get => explosionRadius; set => explosionRadius = value; }
	public bool CanHome { get => canHome; set => canHome = value; }
	public Transform HomingTarget { get => homingTarget; set => homingTarget = value; }
	public float HomingRadius { get => homingRadius; set => homingRadius = value; }
	public float HomingStrength { get => homingStrength; set => homingStrength = value; }
	public float SpiralStrength { get => spiralStrength; set => spiralStrength = value; }

	private void Start()
    {
        shootTimer = AttackSpeed;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
    }

    public void Shoot()
    {
        if(shootTimer >= AttackSpeed)
        {
            shootTimer = 0;

            var projectile = bullet.GetPooledInstance<PooledObject>();
            projectile.gameObject.transform.position = shootPoint.transform.position;
            projectile.gameObject.transform.rotation = shootPoint.transform.rotation;

            Bullet newBullet = projectile.GetComponent<Bullet>();
            
            BulletData newData = default;
            newData.Damage = Damage;
            newData.Speed = BulletSpeed;
            newData.SplitDelay = SplitDelay;
            newData.SplitAngle = SplitAngle;
            newData.SplitLives = SplitLives;
            newData.SplitNum = SplitNum;
            newData.CanPierce = CanPierce;
            newData.CanBounce = CanBounce;
			newData.HealthTarget = HealthTarget;
			newData.LeachAmount = LeachAmount;
			newData.CanExplode = CanExplode;
			newData.ExplosionRadius = ExplosionRadius;
			newData.CanHome = CanHome;
			newData.HomingTarget = HomingTarget;
			newData.HomingRadius = HomingRadius;
			newData.HomingStrength = HomingStrength;
			newData.SpiralStrength = SpiralStrength;
            newData.DestroyDelay = 3f; //Change this later?

            newBullet.BulletPrefab = bullet;
            newBullet.InitBullet(newData);
            newBullet.StartBullet();
        }
    }
}
