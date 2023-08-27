using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubble : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.playSatisfyingClip();
            Instantiate(UI_Manager.instance.shieldPowerUpPartical, transform.position, Quaternion.identity);
            GameManager.instance.powerUps.ShieldPowerUp();
            Destroy(gameObject);

        }
    }
}
