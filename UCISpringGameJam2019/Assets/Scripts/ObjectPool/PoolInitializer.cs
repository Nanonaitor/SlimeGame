using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInitializer : MonoBehaviour
{
    public ObjectSpawns[] objectSpawns;

    void Start()
    {
        for (int i = 0; i < objectSpawns.Length; i++)
        {
            var objectSpawn = objectSpawns[i];
            

            for (int j = 0; j < objectSpawn.amount; j++)
            {
                var pewpew = objectSpawn.objectToSpawn.GetPooledInstance<PooledObject>();
                pewpew.transform.position = transform.position;
                StartCoroutine(ReturnPool(pewpew));
            }
        }
    }

    IEnumerator ReturnPool(PooledObject po)
    {
        yield return null;
        po.ReturnToPool();
    }
}
