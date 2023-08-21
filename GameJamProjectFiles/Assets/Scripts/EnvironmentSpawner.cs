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
    private GameObject platformPrefab;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float spawnDistance = 5f;

    [SerializeField]
    private Transform levelParent;

    void Start()
    {
        currentPlatformsList.Add(startingPlatform);
        currentPlatform = startingPlatform;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(currentPlatform && GameManager.instance.gameStarted)
        {
            float currentSpawnDistance = Vector2.Distance(playerTransform.position, currentPlatform.endPoint.position);

            if(currentSpawnDistance < spawnDistance)
            {
                SpawnPlatform();
            }
        }
    }

    void SpawnPlatform()
    {
        spawnablePlatform platformSpawned = Instantiate(platformPrefab, currentPlatform.endPoint.position, Quaternion.identity, levelParent).GetComponent<spawnablePlatform>();
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
