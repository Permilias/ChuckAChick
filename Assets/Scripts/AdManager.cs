using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {
    private string gameId = "3141072";
    public int adNumber = 3;
    private void Start()
    {
        Advertisement.Initialize(gameId);
    }
  
    public static void ShowAd()
    {
        
    }


    public void PlayLongAd()
    {


      /*  var options = new ShowOptions();
        options.resultCallback = CheckLongAdState;
        Debug.Log(Advertisement.IsReady("LongAd"));
        Advertisement.Show(gameId, options);
        */
        if (Advertisement.IsReady("LongAd"))
        {
            Debug.Log("La pub s'affiche");
            Advertisement.Show("LongAd");
        }
        else
        {
            Debug.Log("ben ça marche pas hein");
        }
    }

    
    public void PlayShortAd()
    {
        Debug.Log("Pressed the Button");
            var options = new ShowOptions { resultCallback = CheckShortAdState };
            Advertisement.Show("ShortAd", options);
        
    }

    private void CheckLongAdState(ShowResult result)
    {

        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("PUB VALIDÉE - RECOMPENSE DONNÉE");
            if(adNumber > 0)
                {
                    //REPLAY FUNCTION
                    adNumber = adNumber - 1;
            //lets go bby. Bon ici c'est au cas où mais pour bien faire faudra juste pas montrer le bouton si adNumber est inf. à 0, et le coder dans un autre script
                }
                break;
            case ShowResult.Skipped:
                Debug.Log("PUB SKIPPÉE - PAS DE REWARD ");
                break;
            case ShowResult.Failed:
                Debug.LogError("PAS DE PUB - BIZARRE");
                break;
        }
    }

    private void CheckShortAdState(ShowResult result)
    {

        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("PUB VALIDÉE - REWARD");
                //Le reward c'est de pouvoir continuer à jouer et skip la pub
                break;
            case ShowResult.Skipped:
                Debug.Log("PUB SKIPPÉE - REWARD");
                //Le reward c'est de pouvoir continuer à jouer et skip la pub
                break;
            case ShowResult.Failed:
                Debug.LogError("PAS DE PUB - BIZARRE");
                break;
        }
    }


}

