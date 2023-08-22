using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeter : MonoBehaviour
{
    public GameObject fillImage;

    private PlayerController pc;

    private void Start()
    {
        pc = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        fillImage.GetComponent<Image>().fillAmount = pc.oxygenAmount / pc.maxOxygen;
    }
}
