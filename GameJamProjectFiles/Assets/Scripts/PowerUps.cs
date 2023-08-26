using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject Player;
    private bool ShieldCooled = true;
    public bool shieldOn;

    public float shieldDuration = 10f;
    public float currentShieldDuration;
    private void Start()
    {
        Player = FindAnyObjectByType<PlayerController>().gameObject;
        currentShieldDuration = shieldDuration;
    }

    public void ShieldPowerUp()
    {
        if (ShieldCooled)
        {
            currentShieldDuration = shieldDuration;
            shieldOn = true;
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(shieldOn)
        {
            currentShieldDuration -= Time.deltaTime;
            UI_Manager.instance.shieldVisualImage.fillAmount = currentShieldDuration / shieldDuration;
            if(currentShieldDuration <= 0)
            {
                shieldOn = false;
            }
        }
        else
        {
            currentShieldDuration = shieldDuration;
        }
    }

}
