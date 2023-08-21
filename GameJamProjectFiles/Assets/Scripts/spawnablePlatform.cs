using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnablePlatform : MonoBehaviour
{
    public Transform startPoint, endPoint;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        transform.localScale =  new Vector3(transform.localScale.x * Random.Range(0.9f, 1.1f), 1, 1);
    }
}
