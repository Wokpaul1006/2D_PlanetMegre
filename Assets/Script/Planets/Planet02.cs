using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet02 : MonoBehaviour
{
    [HideInInspector] ArcadeSC arcadeCtrl;
    [HideInInspector] ChallengeSC challengeCtr;
    [HideInInspector] GenMNSC genCtr;

    private float collisionStart = -1f;
    private GameObject otherObject;
    private float selfScore;
    private int gameMode;
    private bool isCheckDead;
    void Start()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        selfScore = 0.5f;
        isCheckDead = false;
        StartCoroutine(EnableCheckLoose());
        CheckGameomde();
    }
    void Update()
    {
        CheckLoose();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet2")
        {
            collisionStart = Time.time;
            otherObject = collision.gameObject;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == otherObject)
        {
            if (Time.time - collisionStart >= 1f)
            {
                // Destroy both objects
                AddScoring();
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet2")
        {
            collisionStart = -1f;
            otherObject = null;
        }
    }
    private void CheckGameomde()
    {
        if (genCtr.curGameMode == 2)
        {
            gameMode = 2;
            arcadeCtrl = GameObject.Find("ArcadeMN").GetComponent<ArcadeSC>();
        }
        else if (genCtr.curGameMode == 3)
        {
            gameMode = 3;
            challengeCtr = GameObject.Find("ChallengeMN").GetComponent<ChallengeSC>();
        }
    }
    private void AddScoring()
    {
        if (gameMode == 2) arcadeCtrl.IncreaseScore(selfScore);
        else if (gameMode == 3) { } //Add score Challenge
    }
    private void CheckLoose()
    {
        if (isCheckDead == true)
        {
            if (gameObject.transform.position.y >= 2.5f)
            {
                genCtr.OnShowLose();
            }
        }
    }
    IEnumerator EnableCheckLoose()
    {
        yield return new WaitForSeconds(1f);
        isCheckDead = true;
    }
}
