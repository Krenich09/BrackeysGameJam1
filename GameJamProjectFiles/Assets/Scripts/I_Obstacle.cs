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
        if (other.collider.CompareTag("PowerUp"))
        {
            return;
        }
        if (other.collider.CompareTag(playerTag) && !GameManager.instance.powerUps.shieldOn)
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
