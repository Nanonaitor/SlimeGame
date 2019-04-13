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

    float shootTimer;

    private void Start()
    {
        shootTimer = attackSpeed;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
    }

    public void Shoot()
    {
        if(shootTimer >= attackSpeed)
        {
            shootTimer = 0;

            var projectile = bullet.GetPooledInstance<PooledObject>();
            projectile.gameObject.transform.position = shootPoint.transform.position;
            projectile.gameObject.transform.rotation = shootPoint.transform.rotation;

            Bullet newBullet = projectile.GetComponent<Bullet>();
            
            BulletData newData = default;
            newData.Damage = damage;
            newData.Speed = bulletSpeed;
            newData.SplitDelay = splitDelay;
            newData.SplitAngle = splitAngle;
            newData.SplitLives = splitLives;
            newData.SplitNum = splitNum;
            newData.IsPiercing = canPierce;
            newData.IsBouncy = canBounce;
            newData.DestroyDelay = 3f; //Change this later?

            newBullet.BulletPrefab = bullet;
            newBullet.InitBullet(newData);
            newBullet.StartBullet();
        }
    }
}
