using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class RandomObsticalSpawn : MonoBehaviour
{
    public Transform spawnNearTarget;
    public GameObject[] objectsToSpawn;
    public List<float> spawnProbabilities;

    
    public float spawnDistanceDown = 10f; // Radius around the player where obstacles can spawn
    public float spawnInterval = 2f; // Time interval between spawns
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
                Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), spawnNearTarget.position.y - spawnDistanceDown, 0);
                SpawnObjectAtPosition(spawnPosition, objectsToSpawn);
                currentSpawnDelay = spawnInterval;
                Debug.Log("Spawned Obstacle");
            }
        }
    }

    public void SpawnObjectAtPosition(Vector3 position, GameObject[] objectsToSpawn)
    {

        if(Physics2D.OverlapCircle(position, 2))
        {
            Debug.Log("Canceled Object Spawn due to overlap");
            return;
        }

        if (objectsToSpawn.Length == 0 || spawnProbabilities.Count != objectsToSpawn.Length)
        {
            Debug.LogError("Number of objects to spawn and spawn probabilities list must match.");
            return;
        }

        float totalProbability = 0f;
        foreach (float probability in spawnProbabilities)
        {
            totalProbability += probability;
        }

        float randomValue = Random.Range(0f, totalProbability);
        float cumulativeProbability = 0f;

        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            cumulativeProbability += spawnProbabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                Instantiate(objectsToSpawn[i], position, Quaternion.identity);
                break;
            }
        }
    }
}
