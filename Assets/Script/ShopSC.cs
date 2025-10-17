using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSC : MonoBehaviour
{
    [HideInInspector] GenMNSC genCtr;
    [HideInInspector] HomeSC homeCtr;
    [HideInInspector] DataSC data;
    [SerializeField] GameObject powerupPnl, moneypackPnl;
    [SerializeField] Text pGemTxt, pMOneyTxt;
    [SerializeField] Text priceGuideTxt, priceClockTxt, priceMatchTxt, pricePack1, pricePack2, pricePack3, pricePack4, pricePack5, priceAds;
    int pCurMoney, pCurGem;
    int itemPriceGuide, itemPriceClock, itemPriceMatch;
    int packPrice1, packPrice2, packPrice3, packPrice4, packPrice5, packPriceNoAds;
    void Start()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        homeCtr = GameObject.Find("MenuMN").GetComponent<HomeSC>();
        moneypackPnl.gameObject.SetActive(false);
        SetPrice();
        LoadPlayerData();
    }
    void Update()
    {     }

    public void OnClosePanel() => homeCtr.UpdateHomeInfo();

    void LoadPlayerData()
    {
        pCurGem = data.pGems;
        pCurMoney = data.pTotalScore;

        pGemTxt.text = pCurGem.ToString();
        pMOneyTxt.text = pCurMoney.ToString();
    }
    void SetPrice()
    {
        itemPriceGuide = 10;
        itemPriceClock = 30;
        itemPriceMatch = 50;

        packPrice1 = 1;
        packPrice2 = 2;
        packPrice3 = 3;
        packPrice4 = 4;
        packPrice5 = 5;
        packPriceNoAds = 50;

        priceGuideTxt.text = itemPriceGuide.ToString();
        priceClockTxt.text = itemPriceClock.ToString();
        priceMatchTxt.text = itemPriceMatch.ToString();

        pricePack1.text = packPrice1.ToString();
        pricePack2.text = packPrice2.ToString();
        pricePack3.text = packPrice3.ToString();
        pricePack4.text = packPrice4.ToString();
        pricePack5.text = packPrice5.ToString();
        priceAds.text = packPriceNoAds.ToString();
    }
    bool IsEnoughMoney(int price, int type)
    {
        //Check this everytime buy anything
        if(type == 1)
        {
            //Buy point
            if (pCurMoney >= price)
            {
                return true;
            }
            else return false;
        }else if (type == 2)
        {
            if (pCurGem >= price)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    void HandleBuy(int vaule, int type)
    {
        //Update UI here
        switch (type)
        {
            case 1:
                pCurMoney = vaule;
                pMOneyTxt.text = vaule.ToString();
                data.UpdateTotalScore(pCurMoney);
                break;
            case 2:
                pCurGem = vaule;
                pGemTxt.text = vaule.ToString();
                data.UpdateTotalGem(pCurGem);
                break;
        }
        LoadPlayerData();
    }

    #region Handle Power Up
    public void OnBuyLine()
    {
        if(IsEnoughMoney(itemPriceGuide, 1) == true)
        {
            int tempNewCurrency = pCurMoney - itemPriceGuide;
            pCurMoney = tempNewCurrency;
            HandleBuy(pCurMoney, 1);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyClock()
    {
        if (IsEnoughMoney(itemPriceClock, 1) == true)
        {
            int tempNewCurrency = pCurMoney - itemPriceClock;
            pCurMoney = tempNewCurrency;
            HandleBuy(pCurMoney, 1);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyMatch()
    {
        if (IsEnoughMoney(itemPriceMatch, 1) == true)
        {
            int tempNewCurrency = pCurMoney - itemPriceMatch;
            pCurMoney = tempNewCurrency;
            HandleBuy(pCurMoney, 1);
        }
        else
        {
            //Show no enough money
        }
    }
    #endregion

    #region Handle Money Pack
    public void OnBuyPack1()
    {
        if (IsEnoughMoney(packPrice1, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPrice1;
            pCurGem = tempNewCurrency;
            HandleBuy(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyPack2()
    {
        if (IsEnoughMoney(packPrice2, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPrice2;
            pCurGem = tempNewCurrency;
            HandleBuy(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyPack3()
    {
        if (IsEnoughMoney(packPrice3, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPrice3;
            pCurGem = tempNewCurrency;
            HandleBuy(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyPack4()
    {
        if (IsEnoughMoney(packPrice4, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPrice4;
            pCurGem = tempNewCurrency;
            HandleBuy(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }
    }
    public void OnBuyPack5()
    {
        if (IsEnoughMoney(packPrice5, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPrice5;
            pCurGem = tempNewCurrency;
            HandleBuy(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }

    }
    public void OnBuyNoAds()
    {
        if (IsEnoughMoney(packPriceNoAds, 2) == true)
        {
            int tempNewCurrency = pCurGem - packPriceNoAds;
            pCurGem = tempNewCurrency;
            HandleBuy(pCurGem, 2);
        }
        else
        {
            //Show no enough money
        }
    }
    #endregion
}
