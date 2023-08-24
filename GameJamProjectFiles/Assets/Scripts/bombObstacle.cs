using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bombObstacle : I_Obstacle
{
    public bool hitOnce = false;
    public GameObject explosionPartical;
    public Rigidbody2D rb;
    public float explosionRad = 5f;
    public float explostionForce = 10f;
    public override void onHit()
    {
        if(hitOnce) return;

        if (rb)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRad);
            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody2D rb = colliders[i].GetComponent<Rigidbody2D>();

                if(!rb) continue;

                if(rb.gameObject.CompareTag("Player") && GameManager.instance.powerUps.shieldOn) continue;

                if(rb.gameObject.CompareTag("Player"))
                {
                    GameManager.instance.controller.freezePlayer(1);
                }
                Vector2 direction  = (colliders[i].transform.position - transform.position).normalized;
                Debug.Log(rb.name);
                rb.AddForce(direction * explostionForce, ForceMode2D.Impulse);
            }
        }
        if(explosionPartical) Instantiate(explosionPartical, transform.position, Quaternion.identity);
        hitOnce = true;
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRad);
    }
}
