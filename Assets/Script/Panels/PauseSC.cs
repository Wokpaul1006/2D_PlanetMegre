using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSC : Singleton<PauseSC>
{
    [HideInInspector] ArcadeSC arcadeCtr;
    [HideInInspector] ChallengeSC challengeCtr;
    [HideInInspector] GenMNSC genCtr;
    public bool isPause;
    private void Start() => SettingStart();
    private void SettingStart()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
    }
    public void OnResume() 
    {
        if (arcadeCtr != null) arcadeCtr.isPauseGameplay = false;
        else if (challengeCtr != null) challengeCtr.isPauseGameplay = false;
    }
    public void OnHome() => genCtr.OnLoadHome();
    public void OnQuit() => Application.Quit(0);
    public void AssistGameplay(int i)
    {
        if(i == 2) arcadeCtr = GameObject.Find("ArcadeMN").GetComponent<ArcadeSC>();
        else if(i == 3) challengeCtr = GameObject.Find("ChallengeMN").GetComponent<ChallengeSC>();
    }
}
