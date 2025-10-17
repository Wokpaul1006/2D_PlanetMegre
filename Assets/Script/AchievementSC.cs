using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSC : MonoBehaviour
{
    [HideInInspector] HomeSC menu;
    void Start()
    {
        menu = GameObject.Find("MenuMN").GetComponent<HomeSC>();
    }
    public void OnClosePanel() => menu.UpdateHomeInfo();
}
