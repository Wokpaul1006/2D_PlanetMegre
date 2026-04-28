using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PatrolRewardSC : MonoBehaviour
{
    [HideInInspector] DataSC data;
    [HideInInspector] GenMNSC genCtr;

    [SerializeField] List<Button> rewardBtn = new List<Button>();
    [SerializeField] List<GameObject> dailyGrid = new List<GameObject>();
    [SerializeField] List<Text> rewardText = new List<Text>();

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
        OnCheckDailyClaimOnInit();
    }
    void Start()
    {
        isAllowDailyClaim = false;
        streakDaily = data.pDailyStreak;
        lastCollectDay = "";
        rewardToGive = 0;
        OverrideUI();
        ShowRewardDaily();
    }

    private void OverrideUI()
    {
        rewardText[0].text = "10";
        rewardText[1].text = "20";
        rewardText[2].text = "40";
        rewardText[3].text = "80";
        rewardText[4].text = "160";
        rewardText[5].text = "320";
        rewardText[6].text = "640";
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

        print("tempFinalScoreToOverride = " + tempFinalScoreToOverride);

        data.UpdateTotalScore(tempFinalScoreToOverride); // Update score
        data.UpdateStreak(1, streakDaily); //Update streak
        data.UpdatePatrolDailyReward(lastCollectDay); //Update last collect day
        ShowRewardDaily();
        genCtr.UpdateHmeUI();
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
