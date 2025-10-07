using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSC : MonoBehaviour
{
    [SerializeField] GenMNSC genCtr;
    [SerializeField] Text loadTips;
    [SerializeField] GameObject studioIMG;
    [SerializeField] GameObject logoIMG;
    private float loadSpd1, loadSpd2;
    public float targetAlpha = 1f;
    void Start()
    {
        SetupStart();
        StartCoroutine(RunLoadStudioLogo());
    }
    void SetupStart()
    {
        studioIMG.GetComponent<Image>().fillAmount = 0;
        logoIMG.GetComponent<Image>().fillAmount = 0;

        studioIMG.gameObject.SetActive(true);
        logoIMG.gameObject.SetActive(false);
    }
    IEnumerator RunLoadStudioLogo()
    {
        loadSpd1 = Random.Range(0.01f, 0.5f);
        if (studioIMG.GetComponent<Image>().fillAmount >= 1)
        {
            studioIMG.gameObject.SetActive(false);
            logoIMG.gameObject.SetActive(true);
            StopCoroutine(RunLoadStudioLogo());
            StartCoroutine(RunLoadGameLogo());
        }
        yield return new WaitForSeconds(0.1f);
        studioIMG.GetComponent<Image>().fillAmount += loadSpd1 * Time.deltaTime * 10;
        StartCoroutine(RunLoadStudioLogo());
    }

    IEnumerator RunLoadGameLogo()
    {
        loadSpd2 = Random.Range(0.01f, 0.5f);
        if (logoIMG.GetComponent<Image>().fillAmount >= 1)
        {
            logoIMG.gameObject.SetActive(true);
            StopCoroutine(RunLoadGameLogo());
            genCtr.OnLoadHome();
        }
        yield return new WaitForSeconds(0.1f);
        logoIMG.GetComponent<Image>().fillAmount += loadSpd2 * Time.deltaTime * 10;
        StartCoroutine(RunLoadGameLogo());
    }
}
