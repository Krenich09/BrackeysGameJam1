using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBubble : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().oxygenAmount += 50;
            collision.gameObject.GetComponent<PlayerController>().oxygenAmount = Mathf.Clamp(collision.gameObject.GetComponent<PlayerController>().oxygenAmount, 0, collision.gameObject.GetComponent<PlayerController>().maxOxygen);
            Destroy(gameObject);

        }
    }
}
