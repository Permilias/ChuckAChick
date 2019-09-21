using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class TestAd_Script : MonoBehaviour {

    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewarded;
    public string appID, interstitialId, rewardedId;
    public GameObject TextClosed, TextRecompense;
    private bool interstitialLoaded = false;


	public void Start () {
        TextClosed.SetActive(false);
        TextRecompense.SetActive(false);
#if UNITY_ANDROID
        appID = "ca-app-pub-7371546355195021~7725526755";
#else
        appID = "unexpected_platform";
#endif

        MobileAds.Initialize(appID);   
        
    }

    public void Awake()
    {
        RequestInterstitial();
        RequestRewarded();
    }


    public void ShowInterstitial()
    {
        
        // RequestInterstitial();
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

 

    private void RequestInterstitial()
    {
        Debug.Log("request");
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
        interstitial.OnAdClosed += HandleOnAdClosed;
        
        interstitial.LoadAd(CreateNewRequest());
    }

    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        RequestInterstitial();
        TextClosed.SetActive(true);
    }

    private void HandleOnAdLoaded(object sender, EventArgs args)
    {
       

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
        rewarded = RewardBasedVideoAd.Instance;
        rewarded.OnAdRewarded += HandleOnAdRewarded;
        rewarded.OnAdOpening += HandleOnApOpening;
        rewarded.LoadAd(new AdRequest.Builder().Build(), rewardedId);
        
    }

    public void HandleOnApOpening(object sender, EventArgs e)
    {
        Debug.Log("LA PUB S'OUVRE - EVENTUELLEMENT LAUNCH DES PROCESS");
    }

    public void ShowRewarded()
    {
        if (rewarded.IsLoaded())
        {
            rewarded.Show();
        }
        else
        {
            Debug.Log("La pub est pas encore chargée !");
        }

    }

    public void HandleOnAdRewarded(object sender, Reward args)
    {
        TextRecompense.SetActive(true);
        
    }
}
