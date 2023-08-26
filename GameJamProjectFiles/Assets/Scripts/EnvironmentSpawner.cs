using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField]
    private List<spawnablePlatform> currentPlatformsList = new List<spawnablePlatform>();

    [SerializeField]
    private spawnablePlatform startingPlatform;

    [SerializeField]
    private spawnablePlatform currentPlatform;

    [SerializeField]
    private GameObject[] platformPrefabs;

    [SerializeField] private GameObject[] Special_platformPrefabs;
    public float volcanoBiome = 300f; 
    public float specialSpawnPercantage = 5f;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float spawnDistance = 5f;

    [SerializeField]
    private Transform levelParent;

    private float startPercantageVolcano;
    private float lastDistanceCalled = 0f;

    void Start()
    {
        currentPlatformsList.Add(startingPlatform);
        currentPlatform = startingPlatform;
        volcanoBiome = Random.Range(150f, 300f);
        startPercantageVolcano = specialSpawnPercantage;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(currentPlatform && GameManager.instance.gameStarted)
        {
            float currentSpawnDistance = Vector2.Distance(playerTransform.position, currentPlatform.endPoint.position);


            if (Mathf.FloorToInt(GameManager.instance.meter.currentHighScore / volcanoBiome) > Mathf.FloorToInt(lastDistanceCalled / volcanoBiome))
            {
                lastDistanceCalled = GameManager.instance.meter.currentHighScore;
                ActivateForDistance(60);
            }

            if(currentSpawnDistance < spawnDistance)
            {
                if(spawnSpecial(specialSpawnPercantage))
                {
                    SpawnPlatform(Special_platformPrefabs);
                }
                else
                {
                    SpawnPlatform(platformPrefabs);
                }
            }
        }
    }

    private void ActivateForDistance(float distance)
    {
        StartCoroutine(setVolcanoFor(distance));
    }

    private IEnumerator setVolcanoFor(float distance)
    {
        specialSpawnPercantage = 95f;

        float startingDistance = GameManager.instance.meter.currentHighScore;

        while (GameManager.instance.meter.currentHighScore - startingDistance < distance)
        {
            yield return null; // Wait for the next frame
        }

        specialSpawnPercantage = startPercantageVolcano;
    }

    private System.Random random = new System.Random();

    private bool spawnSpecial(float spawnRate)
    {
        if (random.NextDouble() < 1 - (spawnRate / 100))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void SpawnPlatform(GameObject[] platforms)
    {
        int randomPlatform = Random.Range(0, platforms.Length);
        Debug.Log("Spawned Platform: " + randomPlatform);
        spawnablePlatform platformSpawned = Instantiate(platforms[randomPlatform], currentPlatform.endPoint.position, Quaternion.identity, levelParent).GetComponent<spawnablePlatform>();
        currentPlatformsList.Add(platformSpawned);
        currentPlatform = platformSpawned;
        DeleteLastPlatform();
    }

    void DeleteLastPlatform()
    {
        if(currentPlatformsList.Count > 4)
        {
            Destroy(currentPlatformsList[0].gameObject);
            currentPlatformsList.RemoveAt(0);
        }
    }
}
