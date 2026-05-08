using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeSC : MonoBehaviour
{
    [HideInInspector] GenMNSC genCtr;
    [HideInInspector] DataSC data;
    [SerializeField] Text pGemsTxt, pCoinTxt;
    [SerializeField] GameObject shopPnl, achievementPnl, rewardPnl, leaderPnl;
    bool isDailyCollect, isMonthlyCollect;
    string pName;
    int pCoin, pGem;
    void Start()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        data = GameObject.Find("GenMN").GetComponent<DataSC>();
        genCtr.AssistObjectPreload(1);
        OnShowAchievement(false);
        OnShowLeader(false);
        OnShowReward(false);
        OnShowShop(false);
        GetPlayerInfor();

    }
    #region Change Scene
    public void OnToArcade() => genCtr.OnLoadArcade();
    public void OnToChallenge() => genCtr.OnLoadChallenge();
    #endregion

    #region Panel Visible
    public void OnShowShop(bool isShow)
    {
        print("in show");
        shopPnl.gameObject.SetActive(isShow);
    }
    public void OnShowAchievement(bool isShow)
    {
        print("in show");
        achievementPnl.gameObject.SetActive(isShow);
    }
    public void OnShowLeader(bool isShow)
    {
        print("in show");
        leaderPnl.gameObject.SetActive(isShow);
    }
    public void OnShowReward(bool isShow)
    {
        print("in show");
        rewardPnl.gameObject.SetActive(isShow);
    }
    public void OnShowSetting()
    {
        print("in show");
        genCtr.OnShowSetting();
    }
    public void OnShowInfor()
    {
        print("in show");
        genCtr.OnShowInfor();
    }
    #endregion
    #region Home Behaviour
    private void GetPlayerInfor()
    {
        pName = data.pName;
        pGem = data.pGems;
        pCoin = data.pTotalScore;
        OverridePlayerInfor();
    }
    private void OverridePlayerInfor()
    {
        pGemsTxt.text = pGem.ToString();
        pCoinTxt.text = pCoin.ToString();
    }
    public void UpdateHomeInfo()
    {
        //Only call once close any panel
        GetPlayerInfor();
        OverridePlayerInfor();
    }
    public void OnShowPromotion()
    {
        genCtr.OnShowPromtion();
    }
    #endregion
}
