using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public List<spawnablePlatform> currentPlatforms = new List<spawnablePlatform>();
    public spawnablePlatform startingPlatform;
    public spawnablePlatform currentPlatform;

    void Start()
    {
        currentPlatforms.Add(startingPlatform);
    }


    void spawnPlatform()
    {
        
    }
}
