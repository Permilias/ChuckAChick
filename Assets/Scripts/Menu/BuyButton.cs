﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour {

    public static BuyButton Instance;

    public Sprite inactiveSprite;
    public Sprite buySprite;
    public Sprite pushBuySprite;
    public Sprite sellSprite;
    public Sprite pushSellSprite;

    public Image image;

    public bool buys;
    public bool active;
    public RectTransform rt;
    Vector2 initialPos;

    private void Awake()
    {
        Instance = this;
        initialPos = rt.anchoredPosition;
    }

    private void Start()
    {
        rt.localScale = Vector3.zero;
    }

    public void Show()
    {
        rt.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        rt.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
    }

    public void SetActionnable(bool _buys)
    {
        if(_buys)
        {
            SetBuys();
        }
        else
        {
            SetSells();
        }
        active = true;
    }

    public void SetInactive()
    {
        image.sprite = inactiveSprite;
        active = false;
    }

    public void SetBuys()
    {
        image.sprite = buySprite;
        buys = true;
    }

    public void SetSells()
    {
        image.sprite = sellSprite;
        buys = false;
    }

    public void Activate()
    {
        if(buys)
        {
            if(UpgradesManager.Instance.factoryButtonSelected)
            {
                UpgradesManager.Instance.UpgradeFactoryLevel();
            }
            else
            {
                if (UpgradesManager.Instance.currentSelectedButton != null)
                {
                    if (UpgradesManager.Instance.currentSelectedButton.upgradeState > -1)
                    {
                        UpgradesManager.Instance.Upgrade(UpgradesManager.Instance.currentSelectedButton);
                    }
                }
            }
        }
        else
        {
            if(UpgradesManager.Instance.factoryButtonSelected)
            {

            }
            else
            {
                if (UpgradesManager.Instance.currentSelectedButton != null)
                {
                    UpgradesManager.Instance.Downgrade(UpgradesManager.Instance.currentSelectedButton);
                }
            }


        }

    }
}
