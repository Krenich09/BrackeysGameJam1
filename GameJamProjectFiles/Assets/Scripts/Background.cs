using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject background;
    void OnTriggerExit()
    {
        var position = transform.position + new Vector3(0, -8, 0);
        Instantiate(background, position, Quaternion.identity);
    }
}
