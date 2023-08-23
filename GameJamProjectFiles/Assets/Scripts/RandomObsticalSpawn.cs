using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObsticalSpawn : MonoBehaviour
{
    public Transform spawnNearTarget;
    public List<GameObject> obstaclePrefabs; // List of obstacle prefabs
    public float spawnDistanceDown = 10f; // Radius around the player where obstacles can spawn
    public float spawnInterval = 2f; // Time interval between spawns
    public List<float> obstaclePercentages; // Percentages of each obstacle in the list
    public Transform spawnParent;
    public bool doSpawn = false;


    private float currentSpawnDelay;
    void Start()
    {
        currentSpawnDelay = spawnInterval;
    }

    void Update()
    {
        if(GameManager.instance.gameStarted && doSpawn)
        {
            currentSpawnDelay -= Time.deltaTime;
            if(currentSpawnDelay <= 0)
            {
                spawnObstacle();
                currentSpawnDelay = spawnInterval;
                Debug.Log("Spawned Obstacle");
            }
        }
    }

    void spawnObstacle()
    {
        Vector3 spawnPosition = spawnNearTarget.position + Vector3.down * spawnDistanceDown + Vector3.right * Random.Range(-5, 5);

            float totalPercentage = 0f;
            float randomValue = Random.value;

            for (int i = 0; i < obstaclePercentages.Count; i++)
            {
                totalPercentage += obstaclePercentages[i];
                if (randomValue <= totalPercentage)
                {
                    Instantiate(obstaclePrefabs[i], spawnPosition, Quaternion.identity, spawnParent);
                }
            }
    }
}
