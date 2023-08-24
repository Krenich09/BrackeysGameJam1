using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using PlayFab.MultiplayerModels;

public class PlayfabManager : MonoBehaviour
{
    string customID = "";
    public bool isOnline;

    void Start()
    {
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("customID")))
        {
            string randomString = Random.Range(1000000, 9999999).ToString();
            customID = randomString;
            PlayerPrefs.SetString("customID", randomString);
            Debug.Log("New account");
        }
        else
        {
            customID = PlayerPrefs.GetString("customID");
        }

        login();
    }

    public void login()
    {
        
        var LoginWithCustomID = new LoginWithCustomIDRequest()
        {
            CustomId = customID,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(LoginWithCustomID, 
        response => 
        {
            Debug.Log("Logged as: " + response.PlayFabId);
            if(response.NewlyCreated)
            {
                string randomName = "Player: " + Random.Range(1000, 9999);
                changeDisplayName(randomName);
                UI_Manager.instance.newlyNameInput.text = randomName;
                UI_Manager.instance.newlyCreatedPanel.SetActive(true);
            }
            else
            {
                UI_Manager.instance.newlyCreatedPanel.SetActive(false);
            }
            isOnline = true;
        }, 
        error => 
        {
            Debug.Log(error.GenerateErrorReport());
            isOnline = false;
        });
    }


    public void changeDisplayName(string value = null, bool settings = false)
    {
        string name;
        if(string.IsNullOrEmpty(value))
        {
            name = UI_Manager.instance.newlyNameInput.text;
        }
        else { name = value; }

        if(name.Length < 3)
        {
            string error = "Type 3 or more characters";
            Debug.Log(error);
            if(settings)
            {
                UI_Manager.instance.changeNamesettingsError.text = error; 
            }
            else
            {
                UI_Manager.instance.changeNameErrorTxt.text = error; 
            }
            return;
        } 
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        }, result =>
        {
            if(settings)
            {
                UI_Manager.instance.changeNamesettingsError.text = "";
            }
            else
            {
                UI_Manager.instance.changeNameErrorTxt.text = "";
                if(string.IsNullOrEmpty(value))
                {
                    UI_Manager.instance.newlyCreatedPanel.SetActive(false);
                }
            }
            Debug.Log("The player's display name is now: " + result.DisplayName);
        }, error => 
        {
            string errorReport = error.ErrorMessage;
            if(settings)
            {
                UI_Manager.instance.changeNamesettingsError.text = errorReport;
            }
            else
            {
                UI_Manager.instance.newlyCreatedPanel.SetActive(true);
                UI_Manager.instance.changeNameErrorTxt.text = errorReport;
            }
            Debug.LogError(errorReport);
        });
    }

    public void onError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        isOnline = false;
    }

    // Leaderbaord


    public void sendLeaderboard(int score)
    {
        if(isOnline == false) return;
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "depth",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, onError);
    }

    void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful Leaderboard Sent");
    }

    public void GetLeaderbaord()
    {
        if(isOnline == false) return;
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Score",
            StartPosition = 0,
            MaxResultsCount = 15
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, onError);
    }
    void OnLeaderBoardGet(GetLeaderboardResult result)
    {

        if(UI_Manager.instance.leaderBoardParent.childCount > 0)
        {
            for (int i = 0; i < UI_Manager.instance.leaderBoardParent.childCount; i++)
            {
                Destroy(UI_Manager.instance.leaderBoardParent.GetChild(i).gameObject);
            }
        }
        
            
        
        foreach (var item in result.Leaderboard)
        {
            GameObject boardObj = Instantiate(UI_Manager.instance.leaderBoardPrefab, UI_Manager.instance.leaderBoardParent);
            boardObj.GetComponent<leaderBoardItem>().id.text = item.DisplayName;
            boardObj.GetComponent<leaderBoardItem>().score.text = item.StatValue.ToString();
            boardObj.GetComponent<leaderBoardItem>().position.text = (item.Position + 1).ToString();
        }
    }
}
