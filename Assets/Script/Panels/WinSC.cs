using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSC : MonoBehaviour
{
    [HideInInspector] GenMNSC genCtr;
    [HideInInspector] ArcadeSC arcadeCtrl;
    [HideInInspector] ChallengeSC challengeCtr;
    private int gameMode;
    void Start()
    {
    }
    public void AssitGameControl()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
    }

    public void OnGameExit() { Application.Quit(); }
    public void OnNextGame()
    {

    }
    public void OnHome() => genCtr.OnLoadHome();
}
