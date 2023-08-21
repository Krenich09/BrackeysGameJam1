using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera mainCamera;

    [Range(0, 1)]
    [SerializeField] private float smoothTime;

    private float velocityRef;

    void FixedUpdate()
    {

        float smoothedYPos = Mathf.SmoothDamp(mainCamera.transform.position.y, target.position.y, ref velocityRef, smoothTime);
        Vector3 newPos = new Vector3(mainCamera.transform.position.x, smoothedYPos, mainCamera.transform.position.z);

        // Apply the new position
        mainCamera.transform.position = newPos;
    }
}
