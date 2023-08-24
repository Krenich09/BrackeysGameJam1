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

    public void OxygenBoostPowerup()
    {
        Player.GetComponent<PlayerController>().oxygenAmount = Player.GetComponent<PlayerController>().maxOxygen;
    }

    public void ExtraLife()
    {
        GameObject.Find("GameManager").GetComponent<HealthSystem>().currentHearts = GameObject.Find("GameManager").GetComponent<HealthSystem>().currentHearts + 1;
    }

    IEnumerator Sheild()
    {
        shieldOn = true;
        yield return new WaitForSeconds(10f);
        shieldOn = false;
    }
    IEnumerator SheildCooldown()
    {
        ShieldCooled = false;
        yield return new WaitForSeconds(10f);
        ShieldCooled = true;
    }

}
