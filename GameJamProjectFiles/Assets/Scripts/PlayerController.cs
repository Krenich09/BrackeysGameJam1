using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float rotationSpeed = 5f;  // Speed of rotation
    public float swimForce = 10f;       // Force applied for swimming
    public float offsetAngle = 90f;
    public LayerMask groundLayer;
    [Range(0, 1)] public float PosSmoothTime;
    public float movementSineSpeed = 1;
    public float movementSineForce = 1;

    [Header("Dash")]
    public float dashFroce;
    public float dashDelay;
    public GameObject dashParticalPrefab;
    public Transform dashParticalPoint;
    public KeyCode dashKey;

    [Header("Oxygen")]
    public float oxygenAmount = 100;
    public float maxOxygen = 100;
    public float depletionSpeed = 3f;

    [HideInInspector] public Rigidbody2D rb;
    private float rotationSpeedDynamic;
    private float sineX;
    private float startValuemovementSineForce;
    Vector2 refVelocity;
    [HideInInspector] public float dashDelayCurrent;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startValuemovementSineForce = movementSineForce;
        
    }

    private void Update()
    {
        if(GameManager.instance.gameStarted == false) return;

        if(Input.GetKey(KeyCode.W))
        {
            if(!Physics2D.OverlapCircle(transform.position + transform.up * 0.3f, 0.3f, groundLayer))
            {
                movement();
                dashMovement();
            }
            mouseLook();
        }
        dashDelayCurrent -= Time.deltaTime;

        if(movementSineForce > 0.5)
        {
            movementSineForce -= Time.deltaTime;
            movementSineForce = Mathf.Clamp(movementSineForce, startValuemovementSineForce, movementSineForce);
        }
        //Deplete Oxygen
        oxygenAmount -= Time.deltaTime * depletionSpeed;

        //When oxygenAmount = 0, do loss function
    }

    void dashMovement()
    {
        // Dash movement

        if(Input.GetKeyDown(dashKey) && dashDelayCurrent <= 0)
        {
            dashDelayCurrent = dashDelay;
            Instantiate(dashParticalPrefab, dashParticalPoint.position, quaternion.identity); // Spawn Partical Prefab
            sineX = 0;
            movementSineForce *= dashFroce;
        }
    }

    void movement()
    {
        float addedY = movementSineForce * Mathf.Sin(movementSineSpeed * sineX) + movementSineForce + 1;
        sineX += Time.deltaTime;

        Vector2 direction = transform.up * swimForce * addedY;
        Vector2 smoothedPos = Vector2.SmoothDamp(rb.velocity, direction, ref refVelocity, PosSmoothTime);
        
        
        rb.velocity = smoothedPos;
    }
    void mouseLook()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        rotationSpeedDynamic = Mathf.Lerp(0.1f, rotationSpeed, Vector2.Distance(transform.position, mousePosition) / 2);
        Vector3 direction = mousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offsetAngle;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Smoothly interpolate towards the target rotation

        if(Vector2.Distance(mousePosition, transform.position) > 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeedDynamic * Time.deltaTime);
        }
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
