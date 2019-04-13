using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aim and shoot, no pathfinding yet.

public class AI : MonoBehaviour
{
    private Transform player;

    //Sloppy, can refactor if have time
    [SerializeField] private PooledObject enemyProjectile;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float attackRange;

    float attackTimer;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; //Get rid of this later, implementation of EnemySpawner will reference the player to the enemy to avoid so many calls.
        }
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (Vector3.Distance(player.position, transform.position) <= attackRange)
        {
            if (player)
            {
                Vector3 relativeDir = player.position - transform.position;
                Quaternion newRot = Quaternion.LookRotation(relativeDir, Vector3.up);
                newRot.x = 0;
                newRot.z = 0;
                transform.rotation = newRot;
            }

            ShootProjectile();
        }

        Vector3 debugStart = transform.position;
        debugStart.y = 1;
        Debug.DrawRay(debugStart, transform.forward * attackRange, Color.red);
    }

    void ShootProjectile()
    {
        if (attackTimer >= attackSpeed)
        {
            attackTimer = 0;

            var p = enemyProjectile.GetPooledInstance<PooledObject>();
            p.transform.position = shootPoint.transform.position;
            p.transform.rotation = shootPoint.transform.rotation;

            Projectile pr = p.GetComponent<Projectile>();
            pr.InitProjectile(bulletSpeed, damage);
            pr.StartDestroy();
        }
    }
}
