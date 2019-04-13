using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PooledObject
{
    [SerializeField] private float timeToDestroy = 5f;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    public void InitProjectile(float s, int d)
    {
        speed = s;
        damage = d;
    }

    public void StartDestroy()
    {
        StartCoroutine(DelayedPoolDestroy());
    }

    IEnumerator DelayedPoolDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        ReturnToPool();
    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CheckLayer("Player"))
        {
            other.gameObject.ApplyDamage(damage);         
        }
        else if(other.CheckLayer("Environment"))
        {
            ReturnToPool();
        }
    }
}
