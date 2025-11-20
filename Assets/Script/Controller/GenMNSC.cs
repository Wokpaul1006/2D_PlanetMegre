using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GenMNSC : Singleton<GenMNSC>
{
    [SerializeField] List<GameObject> panelList = new List<GameObject>();
    [HideInInspector] DataSC data;
    [HideInInspector] SceneMN sceneCtr;
    [HideInInspector] ArcadeSC arcadeCtr;
    [HideInInspector] ChallengeSC challengeCtr;
    [HideInInspector] HomeSC menuCtr;
    public int deviceType;
    public int curGameMode;
    public string toDay;
    private void Awake() => DontDestroyOnLoad(this);
    void Start()
    {
        sceneCtr = GameObject.Find("OBJ_SceneControl").GetComponent<SceneMN>();
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.Android) deviceType = 1;
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer) deviceType = 2;
        HideAllPanel();
        toDay = (DateTime.Today.Day).ToString() ;
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
    public void OnLoadHome() => sceneCtr.OnLoadScene(1);
    public void OnLoadChallenge() => sceneCtr.OnLoadScene(3);
    public void OnLoadArcade() => sceneCtr.OnLoadScene(2);
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
    public void OnHideSetting() => IsShowPanel(false, 0);
    public void OnHidePause() => IsShowPanel(false, 1);
    public void OnHideLose() => IsShowPanel(false, 2);
    public void OnHideWin() => IsShowPanel(false, 3);
    public void OnHideInfor() => IsShowPanel(false, 4);
    public void OnHideRate() => IsShowPanel(false, 5);
    public void OnHideCredit() => IsShowPanel(false, 6);
    public void OnHideReadMe() => IsShowPanel(false, 7);
    private void HideAllPanel()
    { 
        OnHideSetting();
        OnHidePause();
        OnHideLose();
        OnHideWin();
        OnHideInfor();
        OnHideRate();
        OnHideCredit();
        OnHideReadMe();
    }
    #endregion

    public void OnReplay()
    {
        if(curGameMode == 2)
        {
            arcadeCtr.UpdatePlayerData();
            Invoke(nameof(OnLoadArcade), 1.5f);
        }
        else if(curGameMode == 3)
        {

        }
    }
}
