using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject Player;
    public void ShieldPowerUp()
    {
        StartCoroutine(Sheild());
    }

    public void Oxygen()
    {
        Player.GetComponent<PlayerController>().oxygenAmount = Player.GetComponent<PlayerController>().maxOxygen;
    }

    public void ExtraLife()
    {
        GameObject.Find("GameManager").GetComponent<HealthSystem>().currentHearts = GameObject.Find("GameManager").GetComponent<HealthSystem>().currentHearts + 1;
    }

    IEnumerator Sheild()
    {
        Player.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(10f);
        Player.GetComponent<PolygonCollider2D>().enabled = true;
    }

}
