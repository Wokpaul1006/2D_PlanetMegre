using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenMNSC : Singleton<GenMNSC>
{
    [SerializeField] List<GameObject> panelList = new List<GameObject>();
    [HideInInspector] DataSC data;
    [HideInInspector] SceneMN sceneCtr;
    [HideInInspector] ArcadeSC arcadeCtr;
    [HideInInspector] ChallengeSC challengeCtr;
    [HideInInspector] HomeSC menuCtr;
    [HideInInspector] AdsMN adsCtr;
    public int deviceType;
    public int curGameMode;
    public string today;
    private int interAdsCount, rewardAdsCount;
    private int targetInterAdsCount;
    private void Awake() => DontDestroyOnLoad(this);
    void Start()
    {
        sceneCtr = GameObject.Find("OBJ_SceneControl").GetComponent<SceneMN>();
        data = GameObject.Find("GenMN").GetComponent<DataSC>();
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.Android) deviceType = 1;
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer) deviceType = 2;
        HideAllPanel();
        Invoke(nameof(AssistAdsMn), 10f);
        today = (DateTime.Today.Day).ToString();

        interAdsCount = 0;
        rewardAdsCount = 0;
        targetInterAdsCount = 0;
    }
    void Update() { }
    public void AssistObjectPreload(int sceneOder)
    {
        switch (sceneOder)
        {
            case 1:
                menuCtr = GameObject.Find("MenuMN").GetComponent<HomeSC>();
                break;
            case 2:
                arcadeCtr = GameObject.Find("ArcadeMN").GetComponent<ArcadeSC>();
                arcadeCtr.deviceMode = deviceType;
                curGameMode = 2;
                break;
            case 3:
                challengeCtr = GameObject.Find("ChallengeMN").GetComponent<ChallengeSC>();
                curGameMode = 3;
                break;
        }
    }

    #region Handle Switching Scene
    public void OnLoadHome() 
    {
        //update score
        //updtae high level
        curGameMode = 0;
        interAdsCount += 1;
        //adsLoadChance = UnityEngine.Random.Range(0, 100);
        if (interAdsCount >= targetInterAdsCount)
        {
            adsCtr.ShowAds(1);
            OnToHome();
            interAdsCount = 0;
        }
        else if (interAdsCount < targetInterAdsCount)
        {
            OnToHome();
        }
    }
    public void OnLoadChallenge() => sceneCtr.OnLoadScene(3);
    public void OnLoadArcade() => sceneCtr.OnLoadScene(2);
    public void OnToHome() => sceneCtr.OnLoadScene(1);
    #endregion

    #region Handle Panels
    void IsShowPanel(bool isShow, int panel)
    {
        //1 = setting
        //2 = pause
        //3 = lose
        //4 = win
        //5 = infor
        //6 = rate us
        //7 = credit
        panelList[panel].SetActive(isShow);
    }
    public void OnShowSetting() => IsShowPanel(true, 0);
    public void OnShowPause() => IsShowPanel(true, 1);
    public void OnShowLose() => IsShowPanel(true, 2);
    public void OnShowWin() => IsShowPanel(true, 3);
    public void OnShowInfor() => IsShowPanel(true, 4);
    public void OnShowRate() => IsShowPanel(true, 5);
    public void OnShowCredit() => IsShowPanel(true, 6);
    public void OnShowPromtion() => IsShowPanel(true, 7);
    public void OnHideSetting() => IsShowPanel(false, 0);
    public void OnHidePause() => IsShowPanel(false, 1);
    public void OnHideLose() => IsShowPanel(false, 2);
    public void OnHideWin() => IsShowPanel(false, 3);
    public void OnHideInfor() => IsShowPanel(false, 4);
    public void OnHideRate() => IsShowPanel(false, 5);
    public void OnHideCredit() => IsShowPanel(false, 6);
    public void OnHidePromo() => IsShowPanel(false, 7);
    private void HideAllPanel()
    { 
        OnHideSetting();
        OnHidePause();
        OnHideLose();
        OnHideWin();
        OnHideInfor();
        OnHideRate();
        OnHideCredit();
        OnHidePromo();
    }
    #endregion

    public void OnReplay()
    {
        if(curGameMode == 2)
        {
            arcadeCtr.UpdatePlayerData();
            ToLoadRewardThenArcadeScene();
            Invoke(nameof(OnLoadArcade), 1.5f);
        }
        else if(curGameMode == 3)
        {
            challengeCtr.OnUpdatePlayerData();
        }
    }
    public void UpdateHmeUI()
    {
        menuCtr.UpdateHomeInfo();
    }
    private void AssistAdsMn()
    {
        adsCtr = GameObject.Find("AdsMN").GetComponent<AdsMN>();
        if (adsCtr == null) { print("adsMN null"); }
    }
    private void ToLoadRewardThenArcadeScene() => adsCtr.ShowAds(2);
    public void LoadArcadeFromWin()
    {
        curGameMode = 0;
        interAdsCount += 1;
        if (interAdsCount >= targetInterAdsCount)
        {
            adsCtr.ShowAds(1);
            OnLoadArcade();
            interAdsCount = 0;
        }
        else if (interAdsCount < targetInterAdsCount)
        {
            OnLoadArcade();
        }
    }
}
