using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBubble : MonoBehaviour
{

    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(UI_Manager.instance.oxygenPowerUpPartical, transform.position, Quaternion.identity);
            SoundManager.instance.playSatisfyingClip();
            other.gameObject.GetComponent<PlayerController>().oxygenAmount += 50;
            other.gameObject.GetComponent<PlayerController>().oxygenAmount = Mathf.Clamp(other.gameObject.GetComponent<PlayerController>().oxygenAmount, 0, other.gameObject.GetComponent<PlayerController>().maxOxygen);
            Destroy(gameObject);

        }
    }
}
