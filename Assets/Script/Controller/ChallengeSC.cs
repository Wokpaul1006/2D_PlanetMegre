using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeSC : MonoBehaviour
{
    //Score from this game mode not contribue to data score/coin
    //Gameplay concept: In a determined amount of time, player must complete objective, if not = loose
    [HideInInspector] GenMNSC genctr;
    [HideInInspector] DataSC data;
    [HideInInspector] PauseSC pauseCtr;
    [SerializeField] Text pScoreChallenge, pTimeCountRemaintxt;
    [SerializeField] Text objective01Txt, objective02Txt, objective03Txt, rewardTxt;
    [SerializeField] Image objective1Img, objective2Img, objective3Img;
    [SerializeField] GameObject startPanel, winPanel;
    [SerializeField] List<Image> objectivePlanet = new List<Image>();

    private int numberOfObjective, pHighestLv;
    private int challengeScore, challengeTimeRemain; //variable for win condition check
    private string objective01, objective02, objective03;
    private bool planetSlot1Occupied, planetSlot2Occupied, planetSlot3Occupied;
    private int tempScoreTarget, tempTimeRemain, coinToReward, gemToReward;
    public bool isEnablePlay, isContinuePlay;
    public bool isPauseGameplay;
    void Start()
    {
        Init();
        GenerateChallenge();
    }
    void Update()
    {    }

    private void Init()
    {
        genctr = GameObject.Find("GenGameControlMN").GetComponent<GenMNSC>();
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        pauseCtr = GameObject.Find("PNL_Pause").GetComponent<PauseSC>();
        winPanel.gameObject.SetActive(false);
        GatheringData();
        pauseCtr.AssistGameplay(3);
        objective01 = objective02 = objective03 = "";
        isEnablePlay = false;
        isContinuePlay = false;
        tempScoreTarget = 0;
        tempTimeRemain = 0;
    }

    private void GatheringData() => pHighestLv = data.pHighLv;
    private void GenerateChallenge()
    {
        DetermindChallenge();
        SelectReward();
        GenerateGameplay();
        StartCoroutine(WaitToUnShow());
    }
    private void DetermindChallenge()
    {
        DetermineAmountObjective();
        if (numberOfObjective == 1 || numberOfObjective == 0)
        {
            SelectFirstObjective();
            objective02Txt.gameObject.SetActive(false);
            objective03Txt.gameObject.SetActive(false);
        }
        else if (numberOfObjective == 2)
        {
            SelectFirstObjective();
            SelectSecondObjective();
            objective03Txt.gameObject.SetActive(false);
        }
        else if (numberOfObjective >= 3)
        {
            SelectFirstObjective();
            SelectSecondObjective();
            SelectThirdObjective();
        }
    }

    #region Challenge Setup
    private void DetermineAmountObjective()
    {
        //Tell system how much objective need to show in game
        if (pHighestLv > 0 && pHighestLv <= 10) numberOfObjective = 1;
        else if (pHighestLv > 10 && pHighestLv <= 50) numberOfObjective = 2;
        else if (pHighestLv > 50) numberOfObjective = 3;
    }
    private void SelectFirstObjective()
    {
        //int tempObjectiveOder = Random.Range(1, 4);
        //if (tempObjectiveOder == 1)
        //{
        //    scoreOccupied = true;
        //    if (pHighestLv <= 10) tempScoreTarget = Random.Range(20, 100);
        //    else if (pHighestLv > 10 && pHighestLv <= 50) tempScoreTarget = Random.Range(150, 300);
        //    else if (pHighestLv > 50) tempScoreTarget = Random.Range(300, 500);
        //    objective01 = "OBJECT: SCORE " + tempScoreTarget.ToString();
        //}
        //else if (tempObjectiveOder == 2)
        //{
        //    surviveOccupied = true;
        //    if (pHighestLv <= 10) tempSurviveTarget = Random.Range(20, 60);
        //    else if (pHighestLv > 10 && tempSurviveTarget <= 50) tempSurviveTarget = Random.Range(60, 180);
        //    else if (pHighestLv > 50) tempSurviveTarget = Random.Range(180, 300);
        //    objective01 = "OBJECTIVE: SURVIVE FOR " + tempSurviveTarget + " S";
        //}
        //else if (tempObjectiveOder == 3)
        //{
        //    killOccupied = true;
        //    if (pHighestLv <= 10) tempKillTarget = Random.Range(20, 60);
        //    else if (pHighestLv > 10 && tempKillTarget <= 50) tempScoreTarget = Random.Range(60, 180);
        //    else if (pHighestLv > 50) tempKillTarget = Random.Range(180, 300);
        //    objective01 = "OBJECTIVE: KILL " + tempKillTarget + " MORPINOS";
        //}
    }
    private void SelectSecondObjective()
    {
        //int tempObjectiveOder = Random.Range(1, 4);
        //switch (tempObjectiveOder)
        //{
        //    case 1:
        //        if (scoreOccupied == false)
        //        {
        //            if (pHighestLv <= 10) tempScoreTarget = Random.Range(20, 100);
        //            else if (pHighestLv > 10 && pHighestLv <= 50) tempScoreTarget = Random.Range(150, 300);
        //            else if (pHighestLv > 50) tempScoreTarget = Random.Range(300, 500);
        //            objective02 = "OBJECT: SCORE " + tempScoreTarget.ToString();
        //        }
        //        else SelectSecondObjective();
        //        break;
        //    case 2:
        //        if (surviveOccupied == false)
        //        {
        //            if (pHighestLv <= 10) tempSurviveTarget = Random.Range(20, 60);
        //            else if (pHighestLv > 10 && tempSurviveTarget <= 50) tempSurviveTarget = Random.Range(60, 180);
        //            else if (pHighestLv > 50) tempSurviveTarget = Random.Range(180, 300);
        //            objective02 = "OBJECTIVE: SURVIVE FOR " + tempSurviveTarget + " S";
        //        }
        //        else SelectSecondObjective();
        //        break;
        //    case 3:
        //        if (killOccupied == false)
        //        {
        //            killOccupied = true;
        //            if (pHighestLv <= 10) tempKillTarget = Random.Range(20, 60);
        //            else if (pHighestLv > 10 && tempKillTarget <= 50) tempScoreTarget = Random.Range(60, 180);
        //            else if (pHighestLv > 50) tempKillTarget = Random.Range(180, 300);
        //            objective02 = "OBJECTIVE: KILL " + tempKillTarget + " MORPINOS";
        //        }
        //        else SelectSecondObjective();
        //        break;
        //}
    }
    private void SelectThirdObjective()
    {
        //int tempObjectiveOder = Random.Range(1, 4);
        //switch (tempObjectiveOder)
        //{
        //    case 1:
        //        if (scoreOccupied == false)
        //        {
        //            if (pHighestLv <= 10) tempScoreTarget = Random.Range(20, 100);
        //            else if (pHighestLv > 10 && pHighestLv <= 50) tempScoreTarget = Random.Range(150, 300);
        //            else if (pHighestLv > 50) tempScoreTarget = Random.Range(300, 500);
        //            objective03 = "OBJECT: SCORE " + tempScoreTarget.ToString();
        //        }
        //        else SelectSecondObjective();
        //        break;
        //    case 2:
        //        if (surviveOccupied == false)
        //        {
        //            if (pHighestLv <= 10) tempSurviveTarget = Random.Range(20, 60);
        //            else if (pHighestLv > 10 && tempSurviveTarget <= 50) tempSurviveTarget = Random.Range(60, 180);
        //            else if (pHighestLv > 50) tempSurviveTarget = Random.Range(180, 300);
        //            objective03 = "OBJECTIVE: SURVIVE FOR " + tempSurviveTarget + " S";
        //        }
        //        else SelectSecondObjective();
        //        break;
        //    case 3:
        //        if (killOccupied == false)
        //        {
        //            killOccupied = true;
        //            if (pHighestLv <= 10) tempKillTarget = Random.Range(20, 60);
        //            else if (pHighestLv > 10 && tempKillTarget <= 50) tempScoreTarget = Random.Range(60, 180);
        //            else if (pHighestLv > 50) tempKillTarget = Random.Range(180, 300);
        //            objective03 = "OBJECTIVE: KILL " + tempKillTarget + " MORPINOS";
        //        }
        //        else SelectSecondObjective();
        //        break;
        //}
    }
    private void SelectReward()
    {
        int tempRewardOder = Random.Range(1, 4);
        if (tempRewardOder == 1)
        {
            //Case of reward Coin
            coinToReward = Random.Range(10, 50);
            gemToReward = 0;
            rewardTxt.text = "REWARD: " + coinToReward + " COIN ONCE WIN!";
        }
        else if (tempRewardOder == 2)
        {
            //Case of reward Free Gem
            gemToReward = Random.Range(1, 3);
            coinToReward = 0;
            rewardTxt.text = "REWARD: " + gemToReward + " GEM ONCE WIN!";
        }
        else if (tempRewardOder == 3)
        {
            coinToReward = Random.Range(10, 50);
            gemToReward = Random.Range(1, 3);
            rewardTxt.text = "REWARD: " + coinToReward + " COIN AND " + gemToReward + " GEM ONCE WIN!";
            //Case of Reward both Gem & Coin
        }


        if (!startPanel.activeSelf && isContinuePlay == true)
        {
            //Case of replay
            OnShowObjectivePanel(true);
        }
        else if (startPanel.activeSelf == true && isContinuePlay == false)
        {
            //Case of init play
            OnShowObjectivePanel(true);
        }
    }
    #endregion

    #region Objective Panel
    private void OnShowObjectivePanel(bool isShow)
    {
        if (isShow == false)
        {
            objective01 = objective02 = objective03 = "";
            objective01Txt.text = objective01;
            objective02Txt.text = objective02;
            objective03Txt.text = objective03;

            isEnablePlay = true;
        }
        else if (isShow == true)
        {
            objective01Txt.text = objective01;
            objective02Txt.text = objective02;
            objective03Txt.text = objective03;
            isEnablePlay = false;
        }
        startPanel.gameObject.SetActive(isShow);
    }
    private IEnumerator WaitToUnShow()
    {
        yield return new WaitForSeconds(5f);
        OnShowObjectivePanel(false);
    }
    private IEnumerator WaitToStartGame()
    {
        yield return new WaitForSeconds(5f);
        isEnablePlay = true;
    }
    #endregion

    #region Gameplay Control
    private void GenerateGameplay()
    {
        SetIngamePlayerStat();

        InvokeRepeating(nameof(AddChallengeScore), 1f, 1f);
        InvokeRepeating(nameof(AddChallenegSurviveTime), 1f, 1f);

        StartCoroutine(WaitToStartGame());
    }
    private void SetIngamePlayerStat()
    {
        //challengeScore = 0;
        //challenSurviveTime = 0;
        //pScoreChallenge.text = challengeScore.ToString();
        //pSurviveChallenge.text = challenSurviveTime.ToString() + "s";
    }
    #endregion

    private void AddChallengeScore()
    {
        //if (isEnablePlay == true)
        //{
        //    challengeScore += 1;
        //    if (scoreOccupied == true)
        //    {
        //        if (challengeScore >= tempScoreTarget)
        //        {
        //            OnWinChallenge();
        //        }
        //    }
        //    else if (killOccupied)
        //    {
        //        if (challengeScore >= tempKillTarget)
        //        {
        //            OnWinChallenge();
        //        }
        //    }
        //    else if (scoreOccupied == true && killOccupied == true)
        //    {
        //        if (challengeScore >= tempScoreTarget && challengeScore >= tempKillTarget)
        //        {
        //            OnWinChallenge();
        //        }
        //    }
        //}
        //pScoreChallenge.text = challengeScore.ToString();
    }
    private void AddChallenegSurviveTime()
    {
        //if (isEnablePlay == true)
        //{
        //    challenSurviveTime += 1;
        //    if (surviveOccupied == true)
        //    {
        //        if (challenSurviveTime >= tempSurviveTarget)
        //        {
        //            OnWinChallenge();
        //        }
        //    }
        //}
        //pSurviveChallenge.text = challenSurviveTime.ToString() + "s";
    }
    private void OnWinChallenge()
    {
        isEnablePlay = false;
        winPanel.gameObject.SetActive(true);

        if (coinToReward != 0 && gemToReward == 0)
        {
            int tempScore;
            tempScore = coinToReward + data.pTotalScore;
            data.UpdateTotalScore(tempScore);
        }
        else if (coinToReward == 0 && gemToReward != 0)
        {
            int tempScore;
            tempScore = gemToReward + data.pGems;
            data.UpdateTotalGem(tempScore);
        }
        else if (coinToReward != 0 && gemToReward != 0)
        {
            int tempCoin, tempGems;
            tempCoin = coinToReward + data.pTotalScore;
            tempGems = gemToReward + data.pGems;
            data.UpdateTotalScore(tempCoin);
            data.UpdateTotalGem(tempGems);
        }

    }
    public void OnToHome()
    {
        genctr.OnLoadHome();
    }
    public void OnNextChallenge()
    {
        isContinuePlay = true;
        GenerateChallenge();
    }

    public void OnGameLose()
    {
        isEnablePlay = false;
        CancelInvoke(nameof(AddChallengeScore));
        CancelInvoke(nameof(AddChallenegSurviveTime));
    }
}
