using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 5f;  // Speed of rotation
    public float swimForce = 10f;       // Force applied for swimming
    public float offsetAngle = 90f;
    private Rigidbody2D rb;

    private float rotationSpeedDynamic;
    public LayerMask groundLayer;


    [Range(0, 1)] public float PosSmoothTime;
    Vector2 refVelocity;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            if(!Physics2D.OverlapCircle(transform.position + transform.up * 0.3f, 0.3f, groundLayer))
            {
                movement();
            }
            mouseLook();
        }
    }

    void movement()
    {
        Vector2 direction = transform.up * swimForce;
        Vector2 smoothedPos = Vector2.SmoothDamp(rb.velocity, direction, ref refVelocity, PosSmoothTime);
        rb.velocity = smoothedPos;
    }
    void mouseLook()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        rotationSpeedDynamic = Mathf.Lerp(0.1f, rotationSpeed, Vector2.Distance(transform.position, mousePosition));
        Vector3 direction = mousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offsetAngle;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Smoothly interpolate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeedDynamic * Time.deltaTime);
    }

    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.up  * 0.3f, 0.3f);
    }
}
