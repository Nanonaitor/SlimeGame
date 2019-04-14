using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	[SerializeField] private List<GameObject> commonItems;
	[SerializeField] private List<GameObject> rareItems;
	[SerializeField] private List<GameObject> epicItems;
	[SerializeField] private List<GameObject> legendaryItems;

	private float spawnTimer = 0;
	[SerializeField] private float spawnDelay = 3;

	private int diceRoll = 0;

    void Update()
    {
		//spawnTimer += Time.deltaTime;
		//if (spawnTimer >= spawnDelay)
		//{
		//	SpawnItem(transform.position, transform.rotation);
		//	spawnTimer = 0;
		//}
	}

	public void SpawnItem(Vector3 position, Quaternion rotation)
	{
		diceRoll = Random.Range(0, 100);
		if (diceRoll > 90)
			SpawnLegendary(position, rotation);
		else if (diceRoll > 70)
			SpawnEpic(position, rotation);
		else if (diceRoll > 40)
			SpawnRare(position, rotation);
		else
			SpawnCommon(position, rotation);
	}

	private void SpawnLegendary(Vector3 position, Quaternion rotation)
	{
		if (legendaryItems.Count != 0)
			Instantiate(legendaryItems[Random.Range(0, legendaryItems.Count)], position, rotation);
		else
			SpawnEpic(position, rotation);
	}

	private void SpawnEpic(Vector3 position, Quaternion rotation)
	{
		if (epicItems.Count != 0)
			Instantiate(epicItems[Random.Range(0, epicItems.Count)], position, rotation);
		else
			SpawnRare(position, rotation);
	}

	private void SpawnRare(Vector3 position, Quaternion rotation)
	{
		if (rareItems.Count != 0)
			Instantiate(rareItems[Random.Range(0, rareItems.Count)], position, rotation);
		else
			SpawnCommon(position, rotation);
	}

	private void SpawnCommon(Vector3 position, Quaternion rotation)
	{
		Instantiate(commonItems[Random.Range(0, commonItems.Count)], position, rotation);
	}
}
