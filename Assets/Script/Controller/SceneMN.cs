using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMN : Singleton<SceneMN>
{
    [HideInInspector] GenMNSC genCtr;
    void Start()
    {
        DontDestroyOnLoad(this);
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
    }

    // Update is called once per frame
    void Update()
    { }

    public void OnLoadScene(int sceneOder)
    {
        switch (sceneOder)
        {
            case 1:
                SceneManager.LoadScene("HomeScene");
                break;
            case 2:
                SceneManager.LoadScene("ArcadeScene");
                break;
            case 3:
                SceneManager.LoadScene("Challenge");
                break;
        }
    }
}
