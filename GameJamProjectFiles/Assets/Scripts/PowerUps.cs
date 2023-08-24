using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject Player;

    private void Start()
    {
        Player = FindAnyObjectByType<PlayerController>().gameObject;
    }

    public void ShieldPowerUp()
    {
        StartCoroutine(Sheild());
    }

    public void OxygenBoostPowerup()
    {
        Player.GetComponent<PlayerController>().oxygenAmount = Player.GetComponent<PlayerController>().maxOxygen;
    }

    IEnumerator Sheild()
    {
        Player.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(10f);
        Player.GetComponent<PolygonCollider2D>().enabled = true;
    }

}
