using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;



public class AdManager_True : MonoBehaviour
{
    public GameObject TextRecompense;
    private InterstitialAd ShortAd, ShortAdTest;
    private RewardBasedVideoAd LongAd,LongAdTest;
    public string adIDshort, adIDlong, appID;
    public bool internetConnected;

    private void Awake()
    {

#if UNITY_ANDROID
        appID = "ca-app-pub-7371546355195021~7725526755";
#else
        appID = "unexpected_platform";
#endif

        MobileAds.Initialize(appID);
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Y'A PAS INTERNET");
            internetConnected = false;

        }

        if(Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            
            Debug.Log("Y'A INTERNET");
            internetConnected = true;
           // LoadShortAd();
            
            
           // LoadLongAd();
            
        }
      
    }


    public void LoadShortAd()
    {
        

#if UNITY_ANDROID
        adIDshort = "ca-app-pub-7371546355195021/4675857438";

#else
        adIDshort="unexpected_platform";

#endif
        if (internetConnected)
        {
            this.ShortAd = new InterstitialAd(adIDshort);
            AdRequest request = new AdRequest.Builder().Build();

            this.ShortAd.LoadAd(request);

            Debug.Log("Ending Short Ad Loading");
        }

        ShortAd.OnAdClosed += AdClosedShort;
    }
    public void LoadShortAdTest()
    {


#if UNITY_ANDROID
        adIDshort = "ca-app-pub-3940256099942544/1033173712";

#else
        adIDshort="unexpected_platform";

#endif

        this.ShortAdTest = new InterstitialAd(adIDshort);
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice("3EA218C9D9CA09AA7F3D0EBC9861A84B")
            .Build();
        
        this.ShortAdTest.LoadAd(request);

        ShortAdTest.OnAdClosed += AdClosedShort;

    }


    public void ShowShortAd()
    {
        if (this.ShortAd.IsLoaded() && internetConnected == true)
        {
            this.ShortAd.Show();
        }

    }
    public void ShowShortAdTest()
    {
        LoadShortAdTest();
        if (this.ShortAdTest.IsLoaded() && internetConnected == true)
        {
            this.ShortAdTest.Show();
        }

    }
    public void Recompense(object sender, Reward reward)
    {
        Debug.Log("bien joué à toi");
        TextRecompense.SetActive(true);
    }

    public void LoadLongAd()
    {
#if UNITY_ANDROID
        adIDlong = "ca-app-pub-7371546355195021/2031536317";
#else
        adID="unexpected_platform";

#endif
        if (internetConnected)
        {
            this.LongAd = RewardBasedVideoAd.Instance;
            AdRequest request = new AdRequest.Builder().Build();
            LongAd.LoadAd(request, adIDlong);

        }


        LongAd.OnAdRewarded += Recompense;
        LongAd.OnAdClosed += AdClosedLong;
        Debug.Log("LONG AD LOADED");

    }
    public void ShowLongAd()
    {
        if (this.LongAd.IsLoaded() && internetConnected == true)
        {
            this.LongAd.Show();
        }
    }


    public void LoadLongAdTest()
    {
#if UNITY_ANDROID
        adIDlong = "ca-app-pub-3940256099942544/5224354917";
#else
        adID="unexpected_platform";

#endif
        if (internetConnected)
        {
            this.LongAdTest = RewardBasedVideoAd.Instance;
            AdRequest request = new AdRequest.Builder()
                .AddTestDevice("3EA218C9D9CA09AA7F3D0EBC9861A84B")
                .Build();
            LongAdTest.LoadAd(request, adIDlong);

        }


        LongAdTest.OnAdRewarded += Recompense;
        LongAdTest.OnAdClosed += AdClosedLong;
        Debug.Log("LONG AD TEST LOADED");
    }
    public void ShowLongAdTest()
    {
        LoadLongAdTest();
        if (this.LongAdTest.IsLoaded() && internetConnected == true)
        {
            this.LongAdTest.Show();
            Debug.Log("Long Ad Shown");
        }
    }
  

    public void AdClosedLong(object sender, System.EventArgs args)
    {
        print("PUB FERMÉE");
       // LoadLongAd();
        LoadLongAdTest();
    }
   
    public void AdClosedShort(object sender, System.EventArgs args)
    {
        print("PUB COURTE FERMÉE");
      //  LoadShortAd();
        LoadShortAdTest();
    }

    
}
