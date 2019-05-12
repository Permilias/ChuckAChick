using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int totalGroundChicks;

    public static GameManager Instance;

    public TextMeshProUGUI moneyText;

    public float score;
    public int money;
    public int playerMoney;
    public int baseMoneyMultiplier;

    public bool tutorialEnabled;

    [Header("Game End")]
    public GameObject endBackSprite;
    public TextMeshPro endMoneyText;
    public TextMeshPro endPlayerMoneyText;
    public GameObject backToMenuButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        moneyText.text = "0 $";
        endBackSprite.SetActive(false);
        endMoneyText.gameObject.SetActive(false);
        endPlayerMoneyText.gameObject.SetActive(false);
        backToMenuButton.SetActive(false);

        endMoneyTarget = 0;
        endPlayerMoneyTarget = 0;

        DataManager.Instance.Load(true, true);

        score = 0;
        UpgradesApplier.Instance.ApplyUpgrades();

        if(tutorialEnabled)
        {
            TutorialManager.Instance.StartTutorial();
            EggGenerator.Instance.canSpawn = false;
            MatManager.Instance.cannotIncrease = true;
        }
    }

    float reference;
    float reference2;
    float reference3;
    float shownMoney;
    float endShownMoney;
    float endShownPlayerMoney;

    float endMoneyTarget;
    float endPlayerMoneyTarget;
    private void FixedUpdate()
    {
        shownMoney = Mathf.SmoothDamp(shownMoney, money, ref reference, 0.08f);
        moneyText.text = Mathf.RoundToInt(shownMoney).ToString();

        endShownMoney = Mathf.SmoothDamp(endShownMoney, endMoneyTarget, ref reference2, 0.08f);
        endMoneyText.text = "MONEY : " + Mathf.RoundToInt(endShownMoney).ToString();

        endShownPlayerMoney = Mathf.SmoothDamp(endShownPlayerMoney, endPlayerMoneyTarget, ref reference3, 0.08f);
        endPlayerMoneyText.text = Mathf.RoundToInt(endShownPlayerMoney).ToString();
    }

    public void EndGame()
    {
        StartCoroutine("EndGameCoroutine");
    }

    public bool gameEnded;
    public Animator endBackSpriteAnim;
    IEnumerator EndGameCoroutine()
    {
        endBackSpriteAnim.SetBool("isVisible", true);
        gameEnded = true;
        MatManager.Instance.Stop();
        EggGenerator.Instance.Stop();
        endBackSprite.SetActive(true);
        yield return new WaitForSeconds(2);
        endMoneyTarget = money;
        endMoneyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        endPlayerMoneyText.text = playerMoney.ToString() + "$";
        endPlayerMoneyTarget = playerMoney;
        endPlayerMoneyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        endPlayerMoneyTarget = playerMoney + money;
        playerMoney += money;

        DataManager.Instance.Save(true);
        money = 0;
        endMoneyTarget = 0;

        backToMenuButton.SetActive(true);
    }

    public void AddScore(float value)
    {
        if(!gameEnded)
        {
            score += value;
            if (score < 0) score = 0;
            money = Mathf.RoundToInt(score * baseMoneyMultiplier);
            SoundManager.Instance.PlaySound(SoundManager.Instance.moneySoundRepeat);
            ScoreDisplay.Instance.RefreshCases();
        }

    }

    public void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
        
        DataManager.Instance.Save(true);
    }

    public void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationPause");
        //  DataManager.Instance.Load();
        DataManager.Instance.Save(true);
    }

    public void LoadMenu()
    {   
        StartCoroutine(MenuLoadingCoroutine());
    }

    AsyncOperation ao;
    IEnumerator MenuLoadingCoroutine()
    {   
        Debug.Log("Loading Menu...");
        //DataManager.Instance.Save(true);
        ao = SceneManager.LoadSceneAsync("Menu");
        ao.allowSceneActivation = false;
        OpeningScreen.Instance.Close();
        yield return new WaitForSeconds(0.5F);
        ao.allowSceneActivation = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadMenu();
        }
    }

    public AudioSource ambianceUsine;
    IEnumerator StartFactorySound()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ambianceUsineStart);
        yield return new WaitForSeconds(SoundManager.Instance.ambianceUsineStart.clips[0].length);
        ambianceUsine.Play();
    }
}
