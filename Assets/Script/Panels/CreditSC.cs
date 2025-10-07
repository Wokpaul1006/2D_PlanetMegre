using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditSC : MonoBehaviour
{
    [HideInInspector] GenMNSC genCtr;
    void Start() { genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>(); }
    void Update() { }
    public void OnCloseCredit() => genCtr.OnHideCredit();
    public void ToPrivaciPolicy() { Application.OpenURL("https://sadekgame.wordpress.com/2024/10/17/dino-adventure-privacy-policy/"); }
    public void ToTermUse() { Application.OpenURL("https://sadekgame.wordpress.com/"); }
    public void ToFB() { Application.OpenURL("https://www.facebook.com/sadeksoftVn"); }
    public void ToIG() { Application.OpenURL("https://www.instagram.com/sadekgamesstudio/"); }
    public void ToX() { Application.OpenURL("https://x.com/SadekGame15769"); }
    public void ToWebsite() { Application.OpenURL("https://play.google.com/store/apps/developer?id=Sadek+Games+Studio"); }
    public void ToYTB() { Application.OpenURL("https://www.youtube.com/@SadekGamesStudio"); }
}
