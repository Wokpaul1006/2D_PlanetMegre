using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSC : MonoBehaviour
{
    [HideInInspector] ArcadeSC arcadeCtr;
    [HideInInspector] ChallengeSC challengeCtr;
    [HideInInspector] SceneMN sceneCtr;
    [SerializeField] Sprite alternateApparance, normalApparance;
    [SerializeField] List<GameObject> planetList = new List<GameObject>();
    private int deviceType, gameMode;
    private float moveSpd = 5f;
    private int randPlanetToSpawn;
    void Start()
    {
        sceneCtr = GameObject.Find("OBJ_SceneControl").GetComponent<SceneMN>();
    }
    public void SetGameMode(int mode)
    {
        gameMode = mode;
        SetMode();
    }
    public void SetDeviceType(int type) => deviceType = type;
    private void SetMode()
    {

        if (gameMode == 2) arcadeCtr = GameObject.Find("ArcadeMN").GetComponent<ArcadeSC>();
        else if (gameMode == 3) challengeCtr = GameObject.Find("ChallengeMN").GetComponent<ChallengeSC>();
        SelectNextPlanet();
    }

    // Update is called once per frame
    void Update()
    {
        if(deviceType == 1)
        {
            OnSpawnPlanetByTouch();
            OnMoveTouch();
        }else if(deviceType == 2)
        {
            OnSpawnPlanetByKey();
            OnMoveByKey();
        }
    }

    private void OnSpawnPlanetByKey()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space))
        {
            int randPlanet = randPlanetToSpawn;
            Vector3 curPos = transform.position;
            gameObject.GetComponent<SpriteRenderer>().sprite = alternateApparance;
            float tempY = curPos.y - 0.5f;
            Instantiate(planetList[randPlanet], new Vector3(curPos.x, tempY, 0), Quaternion.identity);
            SelectNextPlanet();
        }
    }
    private void OnSpawnPlanetByTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if(t.phase == TouchPhase.Ended)
            {
                float screenMidVertical = (Screen.height / 2) + (Screen.height / 4);
                print("screenMidVertical = " + screenMidVertical);
                if(t.position.y < screenMidVertical)
                {
                    int randPlanet = randPlanetToSpawn;
                    Vector3 curPos = transform.position;
                    float tempY = curPos.y - 0.5f;
                    gameObject.GetComponent<SpriteRenderer>().sprite = alternateApparance;
                    Instantiate(planetList[randPlanet], new Vector3(curPos.x, tempY, 0), Quaternion.identity);
                    SelectNextPlanet();
                }
            }
        }
    }

    private void OnMoveTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Stationary || t.phase == TouchPhase.Moved)
            {
                float screenMid = Screen.width / 2;
                if (transform.position.x >= -3 && transform.position.x <= 3)
                {
                    if (t.position.x > screenMid)
                    {
                        gameObject.transform.position += Vector3.right * Time.deltaTime * moveSpd;
                    }
                    else if (t.position.x < screenMid)
                    {
                        gameObject.transform.position += Vector3.left * Time.deltaTime * moveSpd;
                    }
                }
                else
                {
                    if (gameObject.transform.position.x > 3) gameObject.transform.position = new Vector3(3, 4, 0);
                    else if (gameObject.transform.position.x < -3) gameObject.transform.position = new Vector3(-3, 4, 0);
                }
                
            }
        }
        
    }
    private void OnMoveByKey()
    {
        if(transform.position.x >= -3 && transform.position.x <= 3)
        {
            if (Input.GetKey(KeyCode.A))
            {
                //Move left
                transform.position += Vector3.left * Time.deltaTime * moveSpd;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //Move Right
                transform.position += Vector3.right * Time.deltaTime * moveSpd;
            }
        }
        else
        {
            if (gameObject.transform.position.x > 3) gameObject.transform.position = new Vector3(3, 4, 0);
            else if (gameObject.transform.position.x < -3) gameObject.transform.position = new Vector3(-3, 4, 0);
        }
    }

    private void SelectNextPlanet()
    {
        randPlanetToSpawn = Random.RandomRange(0, planetList.Count);
        if (gameMode == 2) arcadeCtr.SetPreviewImage(randPlanetToSpawn);
        else if(gameMode == 3) { }
        gameObject.GetComponent<SpriteRenderer>().sprite = normalApparance;
    }
}
