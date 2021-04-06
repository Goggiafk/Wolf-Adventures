using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private bool isStatsRecieved;
    private bool isAchievementCleared;
    private bool isAchievementStatusUpdated;
    private bool isStatsStored;

    void Start()
    {

    }

    void Update()
    {
        //if(!SteamManager.Initialized) { return; }
    }

    public void RequestStats()
    {
        isStatsRecieved = SteamUserStats.RequestCurrentStats();

        Debug.Log("is status recieved: " + isStatsRecieved);

    }

    public void ClearAchievement(string achName)
    {
        isAchievementCleared = Steamworks.SteamUserStats.ClearAchievement(achName);

        Debug.Log("is achievement cleared: " + isAchievementCleared);
        StoreStats();
    }

    public void SetAchievement(string achName)
    {
        isAchievementStatusUpdated = SteamUserStats.SetAchievement(achName);

        Debug.Log("is achievement recieved: " + isAchievementStatusUpdated);
        StoreStats();
    }

    public void StoreStats()
    {
        isStatsStored = SteamUserStats.StoreStats();

        Debug.Log("is status stored: " + isStatsStored);
    }
}
