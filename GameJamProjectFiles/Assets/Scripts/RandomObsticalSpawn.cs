 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObsticalSpawn : MonoBehaviour
{
    public GameObject prefab;
    public int rate;
    public float size;
    public List<GameObject> objects;
    public List<int> chance2;
    void Update()
    {
        List<GameObject> objects2 = new List<GameObject>();
        int chance = UnityEngine.Random.Range(0, rate);
        if (chance == 1)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                for (int i2 = 0; i2 < chance2[i2]; i2++)
                {
                    objects2.Add(objects[i]);
                }
            }
            var position = transform.position + new Vector3(UnityEngine.Random.Range(size, -size), 0, 0);
            Instantiate(objects2[UnityEngine.Random.Range(0, objects2.Count)], position, Quaternion.identity);
        }
    }
}
