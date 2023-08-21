using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObsticalSpawn : MonoBehaviour
{
    public GameObject prefab;
    public float rate;
    public float speed;


    void Update()
    {

        /*speed = GameObject.Find("speed").GetComponent<speed>().speedValue;
        int rateAndSpeed = (int)Mathf.Round(rate * (1f / speed));

        int chance = UnityEngine.Random.Range(0, rateAndSpeed);

        if (chance == 1 && speed != 0)
        {
            var position = transform.position + new Vector3(0, UnityEngine.Random.Range(15f, -15f), 0);
            Instantiate(prefab, position, Quaternion.identity);
        }*/
    }
}
