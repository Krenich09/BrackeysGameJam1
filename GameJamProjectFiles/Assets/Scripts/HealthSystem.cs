using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHearts = 3; // Total number of hearts
    private int currentHearts; // Current number of hearts

    public GameObject heartContainer;
    public GameObject heartIconPrefab;
    private Image[] heartIcons;

    private void Start()
    {
        currentHearts = maxHearts;
        for (int i = 0; i < maxHearts; i++)
        {
            Instantiate(heartIconPrefab, heartContainer.transform);
            heartIcons = heartContainer.GetComponentsInChildren<Image>();
        }
        
    }

    public void GetHit()
    {
        if (currentHearts > 0)
        {
            heartIcons[currentHearts - 1].gameObject.SetActive(false);
            currentHearts--;

            if (currentHearts == 0)
            {
                PlayerDie();
            }
        }
    }

    public void PlayerDie()
    {
        // Perform actions when the player dies
        Debug.Log("Player has died!");
    }
}
