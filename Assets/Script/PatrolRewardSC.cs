using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PatrolRewardSC : MonoBehaviour
{
    [HideInInspector] DataSC data;
    [HideInInspector] GenMNSC genCtr;
    [SerializeField] List<GameObject> rewardDailyLocker = new List<GameObject>();
    [SerializeField] List<GameObject> rewardMonthlyLocker = new List<GameObject>();
    [HideInInspector] HomeSC menu;
    [SerializeField] List<GameObject> dailyGrid = new List<GameObject>();

    [SerializeField] Button claimDailyBtn, claimMonthlyBtn;
    [SerializeField] List<GameObject> monthlyGrid = new List<GameObject>();

    private const string LastPatrolTimeKey = "LastPatrolTime";
    private const string PatrolStreakKey = "PatrolStreak";
    public int baseReward = 100; // example reward
    private int prefClaimDailyState, prefClaimMonthlyState;
    private bool isAllowDailyClaim, isAllowMonthlyClaim;
    private int rewardDaily, rewardMonthly;
    private int streakDaily, streakMonthly;
    void Start()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        menu = GameObject.Find("MenuMN").GetComponent<HomeSC>();

        streakDaily = 0; //Change it by PlayerPrefs
        streakMonthly = 0; //Change it by PlayerPrefs

        streakDaily = data.pDailyStreak;
        streakMonthly = data.pMonthlyStreak;

        rewardDaily = 0;
        rewardMonthly = 0;

        CheckLock(streakDaily, streakMonthly);
        CheckClaimAvailable();
        CheckNewDay();

        print("streak Daily = " + streakDaily);
    }

    private void CheckLock(int streakD, int streakM)
    {
        if(streakD >= 1 && streakD < 8)
        {
            for(int i = 0; i <= streakD - 1; i++)
            {
                rewardDailyLocker[i].gameObject.SetActive(false);
            }
        }

        if(streakM >= 1 && streakM < 4)
        {
            for(int i = 0; i <= streakM - 1; i++)
            {
                rewardMonthlyLocker[i].gameObject.SetActive(false);
            }
        }
    }

    private void CheckClaimAvailable()
    {
        prefClaimDailyState = data.pAllowClaimDaily;
        if(prefClaimDailyState == 1 || prefClaimDailyState == 0) 
        { 
            isAllowDailyClaim = true; 
        }
        else if(prefClaimDailyState == 2) 
        {
            isAllowDailyClaim = false; 
        }

        if (prefClaimMonthlyState == 1 || prefClaimMonthlyState == 0)
        {
            isAllowMonthlyClaim = true;
        }
        else if (prefClaimMonthlyState == 2)
        {
            isAllowMonthlyClaim = false;
        }
    }
    private void CheckNewDay()
    {
        DateTime today;
        DateTime lastClaimdaily;
        string tempLastDaily;
        string tempToday;
        today = DateTime.Today;
        tempToday = today.ToString();
        tempLastDaily = data.pLastDailyClaim;

        print("tempLastDaily: " + tempLastDaily);
        print("tempToday: " + tempToday);
    }

    void CheckDaily()
    {
        if(IsAbleClaim(1) == true)
        {
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
            DateTime lastDailyPatrolClaim = GetLastPatrolTime();
            DateTime currentDateTime = DateTime.UtcNow;
            if (IsNewDay(lastDailyPatrolClaim, currentDateTime))
            {
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
        streakDaily = data.pDailyStreak;
        streakDaily++;
        data.UpdateStreak(1, streakDaily);

        switch (streakDaily)
        {
            case 1:
                rewardDaily = 10;
                break;
            case 2:
                rewardDaily = 20;
                break;
            case 3:
                rewardDaily = 40;
                break;
            case 4:
                rewardDaily = 80;
                break;
            case 5:
                rewardDaily = 100;
                break;
            case 6:
                rewardDaily = 200;
                break;
            case 7:
                rewardDaily = 400;
                break;
            case 8:
                rewardDaily = 1000;
                break;
        }
        GiveReward(rewardDaily, 1);

        DateTime curTimeClaim;
        curTimeClaim = DateTime.UtcNow;
        string currentTimeString = curTimeClaim.ToString();
        data.UpdatePatrolDailyReward(currentTimeString);
    }
    public void OnClaimMonthly()
    {
        //Give Reward Monthly, attach to Month Claim button
    }

  
    int CalculateReward(int streak, int rewardType)
    {
        // Example: reward increases every day up to a cap
        if (rewardType == 1) return baseReward + (streak - 1) * 50;
        else if (rewardType == 2) return 1;
        else return 0;
    }

    private void GiveReward(int amount, int type)
    {
        Debug.Log($"Patrol reward granted: {amount}");
        // Add reward to player inventory / currency
        if(type  == 1)
        {
            int pCoinPref;
            int tempCoin;
            pCoinPref = data.pTotalScore;
            tempCoin = pCoinPref + amount;
            data.UpdateTotalScore(tempCoin);
            data.UpdateAllowClaimDaily(2);
            isAllowDailyClaim = false;
        }else if(type == 2)
        {
            int pGemPref;
            int tempGem;
            pGemPref = data.pGems;
            tempGem = pGemPref + amount;
            data.UpdateTotalGem(tempGem);
            data.UpdateAllowClaimDaily(2);
            isAllowDailyClaim = false;
        }

    }
}
