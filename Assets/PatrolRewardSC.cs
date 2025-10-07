using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PatrolRewardSC : MonoBehaviour
{
    [HideInInspector] DataSC data;
    [HideInInspector] GenMNSC genCtr;
    [HideInInspector] HomeSC menu;
    [SerializeField] List<GameObject> dailyGrid = new List<GameObject>();

    [SerializeField] Button claimDailyBtn, claimMonthlyBtn;
    [SerializeField] List<GameObject> monthlyGrid = new List<GameObject>();

    private const string LastPatrolTimeKey = "LastPatrolTime";
    private const string PatrolStreakKey = "PatrolStreak";
    public int baseReward = 100; // example reward
    void Start()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        menu = GameObject.Find("MenuMN").GetComponent<HomeSC>();

        CheckDaily();
        CheckMonthly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CheckDaily()
    {
        if(IsAbleClaim(1) == true)
        {
            print("1. Start check claim");
            claimDailyBtn.GetComponent<Button>().interactable = true;
        }
        else
        {
            claimDailyBtn.GetComponent<Button>().interactable = false;
        }
    }
    void CheckMonthly()
    {
        if(IsAbleClaim(2) == true)
        {
            claimMonthlyBtn.GetComponent<Button>().interactable = true;
        }
        else
        {
            claimMonthlyBtn.GetComponent<Button>().interactable = false;
        }
    }
    bool IsAbleClaim(int type)
    {
        if (type == 1)
        {
            //Check claim daily
            print("2. In check daily");
            DateTime lastDailyPatrolClaim = GetLastPatrolTime();
            DateTime currentDateTime = DateTime.UtcNow;
            if (IsNewDay(lastDailyPatrolClaim, currentDateTime))
            {
                print("3. Isnew day");
                return true;
            }
            else return false;
        }
        else if (type == 2)
        {
            return true;
        }
        else return false;
    }
    DateTime GetLastPatrolTime()
    {
        string timeString = PlayerPrefs.GetString(LastPatrolTimeKey, "");
        if (string.IsNullOrEmpty(timeString)) return DateTime.MinValue;
        DateTime.TryParse(timeString, out DateTime result);
        return result;
    }

    bool IsNewDay(DateTime lastTime, DateTime currentTime) { return lastTime.Date != currentTime.Date; }

    public void OnClaimDaily()
    {
        //Give reward Daily, attach to Day claim button
        int streak = PlayerPrefs.GetInt(PatrolStreakKey, 0);
        streak++;
        PlayerPrefs.SetInt(PatrolStreakKey, streak);

        int reward = CalculateReward(streak);
        GiveReward(reward);

        DateTime curTimeClaim;
        curTimeClaim = DateTime.UtcNow;
        string currentTimeString = curTimeClaim.ToString();
        data.UpdatePatrolDailyReward(currentTimeString);
    }
    public void OnClaimMonthly()
    {
        //Give Reward Monthly, attach to Month Claim button
    }

  
    int CalculateReward(int streak)
    {
        // Example: reward increases every day up to a cap
        return baseReward + (streak - 1) * 50;
    }

    void GiveReward(int amount)
    {
        Debug.Log($"Patrol reward granted: {amount}");
        // Add reward to player inventory / currency
    }
}
