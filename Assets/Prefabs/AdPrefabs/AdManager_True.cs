using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;


public class AdManager_True : MonoBehaviour
{

    private InterstitialAd ShortAd;
    private RewardedAd LongAd;
    public string adID;
    private void Start()
    {
        LoadShortAd();
    }

    public void LoadShortAd()
    {


#if UNITY_ANDROID
        adID = "ca-app-pub-7371546355195021/4675857438";

#else
        adID="unexpected_platform";

#endif

        this.ShortAd = new InterstitialAd(adID);
        AdRequest request = new AdRequest.Builder().Build();
        
        this.ShortAd.LoadAd(request);
    }

    public void ShowShortAd()
    {
        if (this.ShortAd.IsLoaded())
        {
            this.ShortAd.Show();
        }

    }


    public void LoadLongAd()
    {
#if UNITY_ANDROID
        adID = "ca-app-pub-7371546355195021/2031536317";
#else
        adID="unexpected_platform";

#endif

        this.LongAd = new RewardedAd(adID);
    }




}
