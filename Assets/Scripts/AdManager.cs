using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

    public int adNumber = 3;
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayShortAd();
        }

    }

    public void PlayLongAd()
    {
        if (Advertisement.IsReady("LongAd"))
        {
            var options = new ShowOptions { resultCallback = CheckLongAdState };
            Advertisement.Show("LongAd", options);
        }
        
    }

    
    public void PlayShortAd()
    {

        if (Advertisement.IsReady("ShortAd"))
        {
            var options = new ShowOptions { resultCallback = CheckShortAdState };
            Advertisement.Show("ShortAd", options);
        }
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

