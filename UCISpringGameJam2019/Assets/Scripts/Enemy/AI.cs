using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private float aggroRange;

    [HideInInspector]
    public bool ready;

    bool canFight;

    float attackTimer;

    private NavMeshAgent agent;

    private void OnEnable()
    {
        ready = true;
    }

    private void OnDisable()
    {
        ready = false;
        canFight = false;
    }

    public void SetPlayer(Transform t)
    {
        player = t;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Go()
    {
        canFight = true;
    }

    void Update()
    {
        if (!canFight)
            return;

        attackTimer += Time.deltaTime;

        float distanceBetweenPlayer = Vector3.Distance(player.position, transform.position);

        if(distanceBetweenPlayer <= aggroRange)
        {
            agent.isStopped = true;

            if(distanceBetweenPlayer <= attackRange)
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
            else
            {
                //Chase
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
            }
        }

        Vector3 debugStart = transform.position;
        debugStart.y = 1;
        Debug.DrawRay(debugStart, transform.forward * attackRange, Color.red);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
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
