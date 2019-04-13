using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PooledObject
{
    [SerializeField] private BulletData bulletData;
    public BulletData BulletData { get => bulletData; set => bulletData = value; }
    private PooledObject bulletPrefab;
    public PooledObject BulletPrefab { get => bulletPrefab; set => bulletPrefab = value; }

    public void StartBullet()
    {
        if (gameObject.activeSelf)
        {
            bulletData.BulletDirection = Vector3.forward;

            if (bulletData.DestroyDelay > 0)
                StartCoroutine(DelayedDestroyBullet());

            if (bulletData.SplitNum != 1 && bulletData.SplitLives != 0)
            {
                StartCoroutine(DelayedSplitBullet());
            }
        }
    }

    public void InitBullet(BulletData newData)
    {
        bulletData = new BulletData(newData);
    }

    void Update()
    {
        transform.Translate(bulletData.BulletDirection * bulletData.Speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(bulletData.CanBounce)
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
            other.gameObject.ApplyDamage(BulletData.Damage);
			if (bulletData.HealthTarget != null && bulletData.LeachAmount != 0)
			{
				bulletData.HealthTarget.AddHealth(bulletData.LeachAmount);
			}
            if (!bulletData.CanPierce)
                ReturnToPool();
        }
        else if (other.CheckLayer("Environment") && !bulletData.CanBounce)
        {
            ReturnToPool();
        }
    }

    void DestroyBullet()
    {
        ReturnToPool();
    }


    IEnumerator DelayedSplitBullet()
    {
        yield return new WaitForSeconds(bulletData.SplitDelay);
        float initialAngle = -bulletData.SplitAngle / 2;
        float stepAngle = bulletData.SplitAngle / (bulletData.SplitNum - 1);
        bulletData.SplitLives -= 1;
        for (int i = 0; i < bulletData.SplitNum; ++i)
        {
            var splitBullet = bulletPrefab.GetPooledInstance<PooledObject>();
            splitBullet.transform.position = transform.position;
            Quaternion newRot = transform.rotation * Quaternion.Euler(0, initialAngle + i * stepAngle, 0);
            splitBullet.transform.rotation = newRot;

            Bullet b = splitBullet.GetComponent<Bullet>();
            b.BulletPrefab = BulletPrefab;
            b.InitBullet(bulletData);
            b.StartBullet();

        }
        OnBulletDestroyed();
    }

    IEnumerator DelayedDestroyBullet()
    {
        yield return new WaitForSeconds(bulletData.DestroyDelay);
        OnBulletDestroyed();
    }
}
