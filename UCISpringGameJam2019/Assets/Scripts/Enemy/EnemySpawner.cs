using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform bigBoiSpawn;
    public GameObject bigBoi;

    public List<Transform> spawnPoints;
    public List<PooledObject> enemies;
    List<Transform> temp;

    public ItemSpawner itemSpawner;

    Transform player;

    float spawnTimer;
    public float spawnTime;

    public float spawnAreaLimit = 45;

    [HideInInspector]
    public bool canSpawn = true;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        temp = new List<Transform>();

        GameObject big = Instantiate(bigBoi, bigBoiSpawn.position, Quaternion.identity);
        AI ai = big.GetComponent<AI>();
        ai.SetPlayer(player);
        ai.Go();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            //SpawnEnemy();
        }

        if(canSpawn)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnTime)
            {
                spawnTimer = 0;
                SpawnEnemy();
            }
        }
    }

    public void SpawnEnemy()
    {
        foreach (var item in spawnPoints)
        {
            float distanceToPlayer = Vector3.Distance(player.position, item.position);

            if (distanceToPlayer <= spawnAreaLimit)
            {
                temp.Add(item);
            }
        }

        int ha = Random.Range(0, enemies.Count);
        var newEnemy = enemies[ha].GetPooledInstance<PooledObject>();

        if (temp.Count == 0)
        {
            Debug.Log("No available spawns");
            return;
        }

        int ns = Random.Range(0, temp.Count);
        Vector3 newPos = temp[ns].position;

        newEnemy.transform.position = newPos;
        newEnemy.transform.rotation = Quaternion.identity;

        EnemyHealth eHealth = newEnemy.GetComponent<EnemyHealth>();
        eHealth.itemSpawner = itemSpawner;
        eHealth.anim.SetTrigger("Idle");

        AI ai = newEnemy.GetComponent<AI>();
        ai.SetPlayer(player);
        ai.Go();

        //2lazy4maths
        if(Time.time > 20 && Time.time <= 40)
        {
            eHealth.CurrentHealth *= 2;
        }
        else if(Time.time > 40 && Time.time <= 60)
        {
            eHealth.CurrentHealth *= 3;
        }
        else if (Time.time > 60 && Time.time <= 0)
        {
            eHealth.CurrentHealth *= 4;
        }
        else if (Time.time > 80 && Time.time <= 100)
        {
            eHealth.CurrentHealth *= 5;
        }
        else if (Time.time > 100 && Time.time <= 120)
        {
            eHealth.CurrentHealth *= 6;
        }
        else if (Time.time > 120 && Time.time <= 140)
        {
            eHealth.CurrentHealth *= 7;
        }
        else if (Time.time > 140 && Time.time <= 160)
        {
            eHealth.CurrentHealth *= 8;
        }
        else if (Time.time > 160)
        {
            eHealth.CurrentHealth *= 9;
        }

        temp.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if(spawnPoints.Count > 0)
        {
            foreach (var item in spawnPoints)
            {
                Gizmos.DrawSphere(item.position, 1);
            }
        }
    }
}
