using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseSC : MonoBehaviour
{
    [HideInInspector] GenMNSC genCtr;
    [HideInInspector] ArcadeSC arcadeCtrl;
    [HideInInspector] ChallengeSC challengeCtr;
    private int gameMode;
    void Start() 
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
    }

    public void OnGameExit() { Application.Quit(); }
    public void OnReplay()
    {
        genCtr.OnReplay();
        genCtr.OnHideLose();
    }
    public void OnHome() => genCtr.OnLoadHome();
}
