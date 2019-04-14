using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoi : PooledObject
{
    Animator anim;

    private void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(Bye());
        anim.SetTrigger("Die");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Bye()
    {
        yield return new WaitForSeconds(1.28f);
        ReturnToPool();
    }
}
