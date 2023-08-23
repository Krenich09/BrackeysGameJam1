using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Camera mainCamera;

    [Range(0, 1)]
    [SerializeField] private float smoothTime;

    private float velocityRef;
    private Vector3 vectorTargetpos;
    void FixedUpdate()
    {

        if(target != null)
        {   
            vectorTargetpos = target.position;
        }
        else
        {
            vectorTargetpos = Vector3.up * 3;
        }


        float smoothedYPos = Mathf.SmoothDamp(mainCamera.transform.position.y, vectorTargetpos.y, ref velocityRef, smoothTime);
        Vector3 newPos = new Vector3(mainCamera.transform.position.x, smoothedYPos, mainCamera.transform.position.z);

        // Apply the new position
        mainCamera.transform.position = newPos;
    }
}
