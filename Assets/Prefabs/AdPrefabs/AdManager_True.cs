using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;



public class AdManager_True : MonoBehaviour
{

    private InterstitialAd ShortAd, ShortAdTest;
    private RewardBasedVideoAd LongAd,LongAdTest;
    public string adID, appID;
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
            LoadShortAd();
            LoadLongAd();
            
        }
      
    }
    public void LoadShortAd()
    {
        

#if UNITY_ANDROID
        adID = "ca-app-pub-7371546355195021/4675857438";

#else
        adID="unexpected_platform";

#endif
        if (internetConnected)
        {
            this.ShortAd = new InterstitialAd(adID);
            AdRequest request = new AdRequest.Builder().Build();

            this.ShortAd.LoadAd(request);

            Debug.Log("Ending Short Ad Loading");
        }
       
    }
    public void LoadShortAdTest()
    {


#if UNITY_ANDROID
        adID = "ca-app-pub-3940256099942544/1033173712";

#else
        adID="unexpected_platform";

#endif

        this.ShortAdTest = new InterstitialAd(adID);
        AdRequest request = new AdRequest.Builder().Build();

        this.ShortAdTest.LoadAd(request);
       
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
    public void LoadLongAd()
    {
#if UNITY_ANDROID
        adID = "ca-app-pub-7371546355195021/2031536317";
#else
        adID="unexpected_platform";

#endif
        if (internetConnected)
        {
            RewardBasedVideoAd LongAd = RewardBasedVideoAd.Instance;
            AdRequest request = new AdRequest.Builder().Build();
            LongAd.LoadAd(request, adID);
        }
     
    }
    public void ShowLongAd()
    {
        if (this.LongAd.IsLoaded() && internetConnected == true)
        {
            this.LongAd.Show();
            LongAd.OnAdRewarded += Recompense;
            LongAd.OnAdClosed += AdClosed;
        }
    }

    public void Recompense(object sender, Reward reward)
    {
        Debug.Log("bien joué à toi");

    }

    public void AdClosed(object sender, System.EventArgs args)
    {
        print("PUB FERMÉE");
        LoadLongAd();
        LoadShortAd();
    }
   
}
