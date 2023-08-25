using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubble : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().ShieldPowerUp();
            Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }

}
