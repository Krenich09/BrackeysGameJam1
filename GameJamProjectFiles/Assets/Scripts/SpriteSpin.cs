using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSpin : MonoBehaviour
{
    public float speed = 2.0f;
    float randomOffset;

    private void Awake()
    {
        randomOffset = Random.Range(-10f, 10f);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, randomOffset * Time.deltaTime * speed));
    }

}
