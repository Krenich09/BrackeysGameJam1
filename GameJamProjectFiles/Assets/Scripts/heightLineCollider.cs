using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heightLineCollider : MonoBehaviour
{
    public float downForce;
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().canMove = false;
            other.attachedRigidbody.velocity += Vector2.down * downForce * Time.deltaTime;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().canMove = true;
        }
    }
}
