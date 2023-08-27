using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(UI_Manager.instance.healthPowerUpPartical, transform.position, Quaternion.identity);
            SoundManager.instance.playSatisfyingClip();
            GameManager.instance.healthSystem.Heal();
            Destroy(gameObject);

        }
    }
}
