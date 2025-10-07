using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeSC : MonoBehaviour
{
    //Aecade mode
    //Playr control Sun to spawn planets
    [HideInInspector] GenMNSC genCtr;
    [HideInInspector] PauseSC pauseCtr;
    [HideInInspector] DataSC data;
    [SerializeField] SunSC sun;
    [SerializeField] Image planetPrevieIMG;
    [SerializeField] Text pScoreTxt, pLevelTxt;
    [SerializeField] List<Sprite> previewPlanet = new List<Sprite>();

    public int deviceMode, gameMode;
    private int  arcadeLv, baseTargetLv;
    private float arcadeScore;
    public bool isPauseGameplay;
    void Start()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        //pauseCtr = GameObject.Find("PNL_Pause").GetComponent<PauseSC>();
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        genCtr.AssistObjectPreload(2);
        //pauseCtr.AssistGameplay(2);
        sun = Instantiate(sun, new Vector3(0, 4, 0), Quaternion.identity);

        GenerateGameplay();
        SettingSun();
    }

    void Update() { }

    public void SetDeviceMode(int mode)
    {
        deviceMode = mode;
    }
    private void SettingSun()
    {
        sun.SetGameMode(2);
        sun.SetDeviceType(deviceMode);
    }

    public void SetPreviewImage(int imageOrder)
    {
        planetPrevieIMG.GetComponent<Image>().sprite = previewPlanet[imageOrder];
    }
    public void OnPause()
    {
        genCtr.OnShowPause();
        isPauseGameplay = true;
    }
    private void SettingUI()
    {
        pScoreTxt.text = "0";
        pLevelTxt.text = "0";
    }
    public void IncreaseScore(float score)
    {
        float tempScore;
        tempScore = arcadeScore + score;
        arcadeScore = tempScore;
        pScoreTxt.text = arcadeScore.ToString();
        if(arcadeScore == baseTargetLv)
        {
            arcadeLv++;
            DetermineNextLevelTarget();
        }
    }
    private void DetermineNextLevelTarget()
    {
        int newBaseLv;
        newBaseLv = baseTargetLv * arcadeLv * 2;
        baseTargetLv = newBaseLv;
    }
    public void GenerateGameplay()
    {
        baseTargetLv = 10;
        arcadeLv = 0;
        arcadeScore = 0;
        isPauseGameplay = false;

        SettingUI();
    }
    public void UpdatePlayerData()
    {
        int tempScore;
        tempScore = ((int)arcadeScore);
        data.UpdateTotalScore(tempScore);
    }
}
