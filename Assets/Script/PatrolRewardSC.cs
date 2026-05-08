using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class PatrolRewardSC : MonoBehaviour
{
    [HideInInspector] DataSC data;
    [HideInInspector] GenMNSC genCtr;
    [HideInInspector] HomeSC menuCtr;

    [SerializeField] List<Button> rewardBtn = new List<Button>();

    private const string LastPatrolTimeKey = "LastPatrolTime";
    private const string PatrolStreakKey = "PatrolStreak";
    private int rewardToGive = 10; // example reward, x2 for each time count
    private bool isAllowDailyClaim;
    private int streakDaily;
    private string lastCollectDay;
    private void Awake()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        data = GameObject.Find("GenMN").GetComponent<DataSC>();
        menuCtr = GameObject.Find("MenuMN").GetComponent<HomeSC>();
        OnCheckDailyClaimOnInit();
    }
    void Start()
    {
        isAllowDailyClaim = false;
        streakDaily = data.pDailyStreak;
        lastCollectDay = "";
        rewardToGive = 0;
        ShowRewardDaily();
    }

    #region Handle Claim Daily
    void ShowRewardDaily()
    {
        if (data.pLastDailyClaim == "")
        {
            //First day of play
            isAllowDailyClaim = true;
            for (int i = 0; i < rewardBtn.Count; i++)
            {
                rewardBtn[i].GetComponent<Button>().interactable = false;
            }
            rewardBtn[0].GetComponent<Button>().interactable = true;
        }
        else
        {
            if (genCtr.today != data.pLastDailyClaim)
            {
                //New day access + unclaimed
                for (int i = 0; i < rewardBtn.Count; i++)
                {
                    rewardBtn[streakDaily].GetComponent<Button>().interactable = false;
                }

                if (streakDaily >= 1 && streakDaily < 8)
                {
                    //Lock previous day claim buttons
                    for (int i = 0; i < streakDaily; i++)
                    {
                        rewardBtn[i].GetComponent<Button>().interactable = false;
                    }

                    for (int j = streakDaily + 1; j > rewardBtn.Count; j++)
                    {
                        rewardBtn[j].GetComponent<Button>().interactable = false;
                    }
                    rewardBtn[streakDaily].GetComponent<Button>().interactable = true;

                    isAllowDailyClaim = false;
                }
            }
            else if (genCtr.today == data.pLastDailyClaim)
            {
                if (data.pAllowClaimDaily == 1)
                {
                    //Same day access + claimed
                    isAllowDailyClaim = false;
                    for (int i = 0; i < rewardBtn.Count; i++)
                    {
                        rewardBtn[i].GetComponent<Button>().interactable = false;
                    }
                }
                else if (data.pAllowClaimDaily == 0)
                {
                    //Same day, unclaimed
                    isAllowDailyClaim = true;
                    for (int i = 0; i < rewardBtn.Count; i++)
                    {
                        rewardBtn[i].GetComponent<Button>().interactable = false;
                    }
                    rewardBtn[streakDaily].GetComponent<Button>().interactable = true; //Enable only able-to-claim button
                }
            }
        }
    }
    public void OnClaimDaily()
    {
        int tempFinalScoreToOverride;
        SelectRewardDaily();
        rewardBtn[streakDaily].GetComponent<Button>().interactable = false;
        lastCollectDay = DateTime.Today.Day.ToString();
        isAllowDailyClaim = false;
        tempFinalScoreToOverride = rewardToGive;
        streakDaily++;
        data.UpdateAllowClaimDaily(1);

        data.UpdateTotalScore(tempFinalScoreToOverride); // Update score
        data.UpdateStreak(streakDaily); //Update streak
        data.UpdatePatrolDailyReward(lastCollectDay); //Update last collect day
        ShowRewardDaily();
        menuCtr.UpdateHomeInfo();
    }
    private void SelectRewardDaily()
    {
        switch (streakDaily)
        {
            case 0:
                rewardToGive = 10;
                break;
            case 1:
                rewardToGive = 20;
                break;
            case 2:
                rewardToGive = 40;
                break;
            case 3:
                rewardToGive = 80;
                break;
            case 4:
                rewardToGive = 160;
                break;
            case 5:
                rewardToGive = 320;
                break;
            case 6:
                rewardToGive = 640;
                break;
            case 7:
                rewardToGive = 1280;
                break;
        }
    }
    #endregion

    private void OnCheckDailyClaimOnInit()
    {
        if (genCtr.today != data.pLastDailyClaim)
        {
            data.UpdateAllowClaimDaily(0);
            isAllowDailyClaim = true;
        }
        else
        {
            isAllowDailyClaim = false;
        }
    }
}
