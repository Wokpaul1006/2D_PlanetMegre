using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardSC : MonoBehaviour
{
    [HideInInspector] HomeSC menu;
    void Start()
    {
        menu = GameObject.Find("MenuMN").GetComponent<HomeSC>();
    }
    public void OnClosePanel() => menu.UpdateHomeInfo();
}
