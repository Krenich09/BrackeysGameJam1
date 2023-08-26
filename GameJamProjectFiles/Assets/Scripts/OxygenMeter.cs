using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeter : MonoBehaviour
{
    public GameObject fillImage;

    private PlayerController controller;

    private void Start()
    {
        controller = FindAnyObjectByType<PlayerController>();
    }

    float velocityRef;
    // Update is called once per frame
    void Update()
    {
        float smoothedTarget = Mathf.SmoothDamp(fillImage.GetComponent<Image>().fillAmount, controller.oxygenAmount / controller.maxOxygen, ref velocityRef, 0.1f);
        fillImage.GetComponent<Image>().fillAmount = smoothedTarget;
    }
}
