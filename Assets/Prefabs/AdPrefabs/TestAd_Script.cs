using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class TestAd_Script : MonoBehaviour {

    private InterstitialAd interstitial;
    public string appID, interstitialId, rewardedId;
	

	void Start () {

#if UNITY_ANDROID
        appID = "ca-app-pub-7371546355195021~7725526755";
#else
        appID = "unexpected_platform";
#endif

        MobileAds.Initialize(appID);

        
    }

    public void ShowInterstitial()
    {
        RequestInterstitial();
    }

    public void ShowRewarded()
    {


    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        interstitialId = "ca-app-pub-3940256099942544/1033173712";
#else
        interstitialId = "";
#endif
        if(interstitial != null)
        {
            interstitial.Destroy();
        }
        interstitial = new InterstitialAd(interstitialId);
        interstitial.OnAdLoaded += HandleOnAdLoaded;
        interstitial.LoadAd(CreateNewRequest());
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }


    }

    private AdRequest CreateNewRequest()
    {
        return new AdRequest.Builder().Build();
    }


    private void RequestRewarded()
    {
#if UNITY_ANDROID
        rewardedId = "ca-app-pub-3940256099942544/5224354917";
#else
        rewardedId = "";
#endif

    }

}
