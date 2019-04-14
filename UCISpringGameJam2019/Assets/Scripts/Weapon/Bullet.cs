using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PooledObject
{
    [SerializeField] private WeaponData weaponData;
    public WeaponData WeaponData { get => weaponData; set => weaponData = value; }
    private PooledObject bulletPrefab;
    public PooledObject BulletPrefab { get => bulletPrefab; set => bulletPrefab = value; }

    public void StartBullet()
    {
        if (gameObject.activeSelf)
        {
            weaponData.BulletDirection = Vector3.forward;
			transform.localScale *= weaponData.BulletSize;

            if (weaponData.DestroyDelay > 0)
                StartCoroutine(DelayedDestroyBullet());

            if (weaponData.SplitNum != 1 && weaponData.SplitLives != 0)
            {
                StartCoroutine(DelayedSplitBullet());
            }
        }
    }

    public void InitBullet(WeaponData newData)
    {
        weaponData = new WeaponData(newData);
    }

    void Update()
    {
        transform.Translate(weaponData.BulletDirection * weaponData.BulletSpeed * Time.deltaTime);
		if (WeaponData.CanHome && WeaponData.HomingTarget != null)
		{
			if (Vector3.Distance(transform.position, WeaponData.HomingTarget.position) < 3)
			{
				transform.LookAt(WeaponData.HomingTarget);
			}
			else
			{
				Vector3 direction = (WeaponData.HomingTarget.position - transform.position).normalized;
				Quaternion lookRotation = Quaternion.LookRotation(direction);
				transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * weaponData.HomingStrength);
			}
		}
		else
		{
			transform.rotation = transform.rotation * Quaternion.Euler(0, weaponData.SpiralStrength, 0);
		}
	}

    private void FixedUpdate()
    {
		if (weaponData.CanHome && weaponData.HomingTarget == null)
			GetHomingTarget();
        if(weaponData.CanBounce && weaponData.BounceNum > 0)
        {
            LayerMask mask = LayerMask.GetMask("Environment");
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * (Time.deltaTime * weaponData.BulletSpeed + 1.2f), Color.blue);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Time.deltaTime * weaponData.BulletSpeed + 1.2f, mask))
            {
                Vector3 reflectDir = Vector3.Reflect(transform.TransformDirection(Vector3.forward), hit.normal);
                float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, rot, 0);
				--weaponData.BounceNum;
			}
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
        if(other.CheckLayer("Enemy"))
        {
			if (weaponData.CanExplode)
				EXXUUPLOSION();
			else
				other.gameObject.ApplyDamage(WeaponData.Damage);
			if (weaponData.HealthTarget != null && weaponData.LeachAmount != 0)
				weaponData.HealthTarget.AddHealth(weaponData.LeachAmount);
			if ((weaponData.CanPierce && weaponData.CanHome) || weaponData.PierceNum <= 0)
				ReturnToPool();
			else
				--weaponData.PierceNum;
        }
        else if (other.CheckLayer("Environment") && !weaponData.CanBounce)
        {
			if (weaponData.CanExplode)
				EXXUUPLOSION();
			ReturnToPool();
        }
    }

	private void GetHomingTarget()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, weaponData.HomingRadius);
		int i = 0;
		while (i < hitColliders.Length)
		{
			if (hitColliders[i].CheckLayer("Enemy"))
			{
				if (WeaponData.HomingTarget != null && WeaponData.HomingTarget.GetComponent<AI>().ready)
				{
					if (Vector3.Distance(transform.position, hitColliders[i].transform.position) < Vector3.Distance(transform.position, WeaponData.HomingTarget.position))
						weaponData.HomingTarget = hitColliders[i].transform;
				}
				else
				{
					weaponData.HomingTarget = hitColliders[i].transform;
				}
			}
			i++;
		}
	}

	public void EXXUUPLOSION()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, weaponData.ExplosionRadius);
		int i = 0;
		while (i < hitColliders.Length)
		{
			hitColliders[i].gameObject.ApplyDamage(weaponData.Damage);
			i++;
		}
	}

    void DestroyBullet()
    {
        ReturnToPool();
    }


    IEnumerator DelayedSplitBullet()
    {
        yield return new WaitForSeconds(weaponData.SplitDelay / weaponData.BulletSpeed);
        float initialAngle = -weaponData.SplitAngle / 2;
        float stepAngle = weaponData.SplitAngle / (weaponData.SplitNum - 1);
        weaponData.SplitLives -= 1;
        for (int i = 0; i < weaponData.SplitNum; ++i)
        {
            var splitBullet = bulletPrefab.GetPooledInstance<PooledObject>();
            splitBullet.transform.position = transform.position;
			splitBullet.transform.localScale = weaponData.InitialBulletSize * weaponData.BulletSize;
			Quaternion newRot = transform.rotation * Quaternion.Euler(0, initialAngle + i * stepAngle, 0);
            splitBullet.transform.rotation = newRot;

            Bullet b = splitBullet.GetComponent<Bullet>();
            b.BulletPrefab = BulletPrefab;
            b.InitBullet(weaponData);
            b.StartBullet();
        }
        OnBulletDestroyed();
    }

    IEnumerator DelayedDestroyBullet()
    {
        yield return new WaitForSeconds(weaponData.DestroyDelay);
        OnBulletDestroyed();
    }
}
