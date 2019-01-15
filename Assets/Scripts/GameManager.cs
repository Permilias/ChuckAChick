using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int totalGroundChicks;

    public static GameManager Instance;

    public TextMeshPro moneyText;

    public float score;
    public int money;
    public int playerMoney;
    public int baseMoneyMultiplier;

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
        moneyText.text = Mathf.RoundToInt(shownMoney).ToString() + " $";

        endShownMoney = Mathf.SmoothDamp(endShownMoney, endMoneyTarget, ref reference2, 0.08f);
        endMoneyText.text = Mathf.RoundToInt(endShownMoney).ToString() + " $";

        endShownPlayerMoney = Mathf.SmoothDamp(endShownPlayerMoney, endPlayerMoneyTarget, ref reference3, 0.08f);
        endPlayerMoneyText.text = Mathf.RoundToInt(endShownPlayerMoney).ToString() + " $";
    }

    public void EndGame()
    {
        StartCoroutine("EndGameCoroutine");
    }

    public bool gameEnded;
    IEnumerator EndGameCoroutine()
    {
        gameEnded = true;
        MatManager.Instance.Stop();
        EggGenerator.Instance.Stop();
        endBackSprite.SetActive(true);
        yield return new WaitForSeconds(1);
        endMoneyTarget = money;
        endMoneyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        endPlayerMoneyText.text = playerMoney.ToString();
        endPlayerMoneyTarget = playerMoney;
        endPlayerMoneyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        endPlayerMoneyTarget = playerMoney + money;
        playerMoney += money;

        DataManager.Instance.Save(true);
        money = 0;
        endMoneyTarget = 0;

        yield return new WaitForSeconds(1);
        backToMenuButton.SetActive(true);
    }

    public void AddScore(float value)
    {
        if(!gameEnded)
        {
            score += value;
            if (score < 0) score = 0;
            money = Mathf.RoundToInt(score * baseMoneyMultiplier);
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

    IEnumerator MenuLoadingCoroutine()
    {
        Debug.Log("Loading Menu...");
        //DataManager.Instance.Save(true);
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadMenu();
        }
    }
}
