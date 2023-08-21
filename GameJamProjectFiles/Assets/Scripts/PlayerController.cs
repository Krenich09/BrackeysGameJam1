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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            movement();
            mouseLook();
        }
    }

    void movement()
    {
        Vector2 direction = transform.up * swimForce;
        rb.velocity = direction;
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
}
