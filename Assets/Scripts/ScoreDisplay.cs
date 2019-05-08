using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreDisplay : MonoBehaviour {

    public static ScoreDisplay Instance;

    public GameObject scoreCase;
    List<RectTransform> scrts;

    public GameObject caseParent;

    RectTransform rt;

    public float caseDistance;
    public float tweenSpeed;

    private void Awake()
    {
        Instance = this;
        rt = GetComponent<RectTransform>();
    }

    GameObject newCase;
    private void Start()
    {
        scrts = new List<RectTransform>();
        for(int i = 0; i < 6; i++)
        {
            newCase = Instantiate(scoreCase, caseParent.transform);
            scrts.Add(newCase.GetComponent<RectTransform>());
            newCase.SetActive(false);
            
        }

        scoreCase.SetActive(false);

        RefreshCases();
    }

    int currentCaseAmount;
    int lastCaseAmount;
    public void RefreshCases()
    {
        if(GameManager.Instance.score < 100000)
        {
            if (GameManager.Instance.score < 10000)
            {
                if (GameManager.Instance.score < 1000)
                {
                    if (GameManager.Instance.score < 100)
                    {
                        if (GameManager.Instance.score < 10)
                        {
                            currentCaseAmount = 1;
                        }
                        else
                        {
                            currentCaseAmount = 2;
                        }
                    }
                    else
                    {
                        currentCaseAmount = 3;
                    }
                }
                else
                {
                    currentCaseAmount = 4;
                }
            }
            else
            {
                currentCaseAmount = 5;
            }
        }
        else
        {
            currentCaseAmount = 6;
        }

        if(lastCaseAmount != currentCaseAmount)
        {
            RefreshCasePositions();
        }
    }

    public void DeactivateAll()
    {
        foreach(RectTransform scrt in scrts)
        {
            scrt.gameObject.SetActive(false);
        }
    }

    public void ActivateAll()
    {
        foreach (RectTransform scrt in scrts)
        {
            scrt.gameObject.SetActive(true);
        }
    }

    public void RefreshCasePositions()
    {
        if(currentCaseAmount == 1)
        {
            DeactivateAll();
            scrts[0].gameObject.SetActive(true);
            scrts[0].DOAnchorPos(rt.anchoredPosition, tweenSpeed);
        }
        else if(currentCaseAmount == 2)
        {
            DeactivateAll();
            scrts[0].gameObject.SetActive(true);
            scrts[0].DOAnchorPos(rt.anchoredPosition + new Vector2(-(caseDistance/2), 0), tweenSpeed);
            scrts[1].gameObject.SetActive(true);
            scrts[1].DOAnchorPos(rt.anchoredPosition + new Vector2(caseDistance / 2, 0), tweenSpeed);
        }
        else if(currentCaseAmount == 3)
        {
            DeactivateAll();
            scrts[0].gameObject.SetActive(true);
            scrts[0].DOAnchorPos(rt.anchoredPosition + new Vector2(-caseDistance, 0), tweenSpeed);
            scrts[1].gameObject.SetActive(true);
            scrts[1].DOAnchorPos(rt.anchoredPosition, tweenSpeed);
            scrts[2].gameObject.SetActive(true);
            scrts[2].DOAnchorPos(rt.anchoredPosition + new Vector2(caseDistance, 0), tweenSpeed);
        }
        else if(currentCaseAmount == 4)
        {
            DeactivateAll();
            scrts[0].gameObject.SetActive(true);
            scrts[0].DOAnchorPos(rt.anchoredPosition + new Vector2(-(caseDistance + (caseDistance/2)), 0), tweenSpeed);
            scrts[1].gameObject.SetActive(true);
            scrts[1].DOAnchorPos(rt.anchoredPosition + new Vector2(-(caseDistance / 2), 0), tweenSpeed);
            scrts[2].gameObject.SetActive(true);
            scrts[2].DOAnchorPos(rt.anchoredPosition + new Vector2(caseDistance / 2, 0), tweenSpeed);
            scrts[3].gameObject.SetActive(true);
            scrts[3].DOAnchorPos(rt.anchoredPosition + new Vector2(caseDistance + (caseDistance / 2), 0), tweenSpeed);
        }
        else if(currentCaseAmount == 5)
        {
            DeactivateAll();
            scrts[0].gameObject.SetActive(true);
            scrts[0].DOAnchorPos(rt.anchoredPosition + new Vector2(-(caseDistance*2), 0), tweenSpeed);
            scrts[1].gameObject.SetActive(true);
            scrts[1].DOAnchorPos(rt.anchoredPosition + new Vector2(-caseDistance, 0), tweenSpeed);
            scrts[2].gameObject.SetActive(true);
            scrts[2].DOAnchorPos(rt.anchoredPosition, tweenSpeed);
            scrts[3].gameObject.SetActive(true);
            scrts[3].DOAnchorPos(rt.anchoredPosition + new Vector2(caseDistance, 0), tweenSpeed);
            scrts[4].gameObject.SetActive(true);
            scrts[4].DOAnchorPos(rt.anchoredPosition + new Vector2(caseDistance * 2, 0), tweenSpeed);
        }
        else
        {
            ActivateAll();
            scrts[0].DOAnchorPos(rt.anchoredPosition + new Vector2(-((2*caseDistance) + (caseDistance / 2)), 0), tweenSpeed);
            scrts[1].DOAnchorPos(rt.anchoredPosition + new Vector2(-(caseDistance + (caseDistance / 2)), 0), tweenSpeed);
            scrts[2].DOAnchorPos(rt.anchoredPosition + new Vector2(-(caseDistance / 2), 0), tweenSpeed);
            scrts[3].DOAnchorPos(rt.anchoredPosition + new Vector2(caseDistance / 2, 0), tweenSpeed);
            scrts[4].DOAnchorPos(rt.anchoredPosition + new Vector2(caseDistance + (caseDistance / 2), 0), tweenSpeed);
            scrts[5].DOAnchorPos(rt.anchoredPosition + new Vector2((2*caseDistance) + (caseDistance / 2), 0), tweenSpeed);

        }
    }

}
