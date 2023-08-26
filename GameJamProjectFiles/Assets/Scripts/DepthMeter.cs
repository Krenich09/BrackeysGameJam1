using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class DepthMeter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] numberText;

    private GameObject player;
    public float distance;
    [HideInInspector] public float currentHighScore;
    public Animator sourceAnim;
    public float lastDistanceCalled = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Reset numbers to 0
        for (int i = 0; i < numberText.Length; i++)
        {
            numberText[i].text = "0";
        }

        player = FindAnyObjectByType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.FloorToInt(currentHighScore / 100f) > Mathf.FloorToInt(lastDistanceCalled / 100f))
        {
            lastDistanceCalled = currentHighScore;
            sourceAnim.SetTrigger("milestone");
        }


        CalculateDistance();
        if (distance > 25 && distance < 50)
        {
            GameManager.instance.randomObsticalSpawn.spawnInterval = 0.9f;
        }
        if (distance > 50 && distance < 75)
        {
            GameManager.instance.randomObsticalSpawn.spawnInterval = 0.8f;
        }
        if (distance > 75 && distance < 100)
        {
            GameManager.instance.randomObsticalSpawn.spawnInterval = 0.7f;
        }
        if (distance > 100 && distance < 150)
        {
            GameManager.instance.randomObsticalSpawn.spawnInterval = 0.6f;
        }
        if (distance > 150 && distance < 200)
        {
            GameManager.instance.randomObsticalSpawn.spawnInterval = 0.5f;
        }
        if (distance > 200 && distance < 300)
        {
            GameManager.instance.randomObsticalSpawn.spawnInterval = 0.4f;
        }
        if (distance > 300 && distance < 400)
        {
            GameManager.instance.randomObsticalSpawn.spawnInterval = 0.3f;
        }
        if (distance > 500)
        {
            GameManager.instance.randomObsticalSpawn.spawnInterval = 0.25f;
        }
        UpdateNumbers();
        UpdateNumbers();

        if(Vector2.Distance(player.transform.position, UI_Manager.instance.heightLine.position) < 3 && Mathf.Abs(GameManager.instance.controller.rb.velocity.magnitude) > 3 && 
        GameManager.instance.checkIfFacingDown(GameManager.instance.controller.transform, 65))
        {
            GameManager.instance.randomObsticalSpawn.doSpawn = true;
        }
        else
        {
            GameManager.instance.randomObsticalSpawn.doSpawn = false;
        }
    }

    void CalculateDistance()
    {
        if (distance < Vector2.Distance(player.transform.position, Vector2.zero))
        {
            distance = Vector2.Distance(player.transform.position, Vector2.zero);
            player.GetComponent<PlayerController>().speedMultiplier = Mathf.Clamp((distance / 1000) + 1f, 1f, 3f);
        }
        else
        {
            do
            {
                currentHighScore = distance;
            } while (currentHighScore < distance);
        }
    }

    void UpdateNumbers()
    {
        char[] numberChars = Mathf.Round(currentHighScore).ToString().ToCharArray(); //Splits distance into char array

        if (distance.ToString() != numberChars.ToString())
        {
            for (int i = 0; i < numberChars.Length; i++)
            {
                switch (i) //Sets each text to its digit // if we have time, animate text to scroll up out of the mask then new number scrolls up from bottom to middle.
                {
                    case 0:

                        numberText[0].text = numberChars[0].ToString();

                        break;

                    case 1:

                        numberText[1].text = numberChars[0].ToString();
                        numberText[0].text = numberChars[1].ToString();

                        break;

                    case 2:

                        numberText[2].text = numberChars[0].ToString();
                        numberText[1].text = numberChars[1].ToString();
                        numberText[0].text = numberChars[2].ToString();

                        break;

                    case 3:

                        numberText[3].text = numberChars[0].ToString();
                        numberText[2].text = numberChars[1].ToString();
                        numberText[1].text = numberChars[2].ToString();
                        numberText[0].text = numberChars[3].ToString();

                        break;

                    case 4:

                        numberText[4].text = numberChars[0].ToString();
                        numberText[3].text = numberChars[1].ToString();
                        numberText[2].text = numberChars[2].ToString();
                        numberText[1].text = numberChars[3].ToString();
                        numberText[0].text = numberChars[4].ToString();

                        break;

                    case 5:

                        numberText[5].text = numberChars[0].ToString();
                        numberText[4].text = numberChars[1].ToString();
                        numberText[3].text = numberChars[2].ToString();
                        numberText[2].text = numberChars[3].ToString();
                        numberText[1].text = numberChars[4].ToString();
                        numberText[0].text = numberChars[5].ToString();

                        break;

                    default:
                        break;
                }
            }
        }
    }
}
