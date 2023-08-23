using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxygenPowerUp : MonoBehaviour
{
    public void OxygenPowerUp()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().oxygenAmount = GameObject.Find("Player").GetComponent<PlayerController>().maxOxygen;
    }
}
