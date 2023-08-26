using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoObstacle : MonoBehaviour
{

    public float ImpulseForce = 5;


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(GameManager.instance.powerUps.shieldOn) return;

            GameManager.instance.controller.freezePlayer(1);
            GameManager.instance.healthSystem.GetHit();

            Vector2 direction  = (other.transform.position - transform.position).normalized;

            other.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * ImpulseForce, ForceMode2D.Impulse);
        }
    }
}
