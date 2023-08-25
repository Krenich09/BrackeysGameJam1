using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Obstacle : MonoBehaviour
{   
    public string playerTag = "Player";
    public virtual void onHit()
    {
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            return;
        }
        if (other.collider.CompareTag(playerTag) && !other.gameObject.GetComponent<PlayerController>().shieldOn)
        {
            GameManager.instance.healthSystem.GetHit();
            onHit();
        }
        else
        {
            onHit();
        }
    }
}
