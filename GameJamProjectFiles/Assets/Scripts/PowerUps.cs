using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject Player;
    private bool ShieldCooled = true;
    public bool shieldOn;
    private void Start()
    {
        Player = FindAnyObjectByType<PlayerController>().gameObject;
    }

    public void ShieldPowerUp()
    {
        if (ShieldCooled)
        {
            StartCoroutine(Sheild());
        }
    }

    IEnumerator Sheild()
    {
        shieldOn = true;
        yield return new WaitForSeconds(10f);
        shieldOn = false;
    }

}
