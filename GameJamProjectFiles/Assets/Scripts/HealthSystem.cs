using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHearts = 3; // Total number of hearts
    private int currentHearts; // Current number of hearts

    private void Start()
    {
        currentHearts = maxHearts;
    }

    public void GetHit()
    {
        if (currentHearts > 0)
        {
            currentHearts--;

            if (currentHearts == 0)
            {
                PlayerDie();
            }
        }
    }

    private void PlayerDie()
    {
        // Perform actions when the player dies
        Debug.Log("Player has died!");
    }
}
