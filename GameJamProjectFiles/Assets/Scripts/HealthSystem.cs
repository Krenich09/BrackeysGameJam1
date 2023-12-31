using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHearts = 3; // Total number of hearts
    public int currentHearts; // Current number of hearts

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
            heartIcons[currentHearts - 1].gameObject.GetComponent<Animator>().SetTrigger("die");
            currentHearts--;

            if (currentHearts == 0)
            {
                PlayerDie();
            }
        }
    }
    public void Heal()
    {

        if (currentHearts < 3)
        {
            heartIcons[currentHearts].gameObject.SetActive(true);
            currentHearts++;
        }
    }

    public void PlayerDie()
    {
        if(GameManager.instance.gameEnded) return;
        // Perform actions when the player dies
        Debug.Log("Player has died!");
        GameManager.instance.EndGame();
    }
}
