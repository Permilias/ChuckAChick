using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryLevelButton : MonoBehaviour {


    public Sprite level0BGSprite;
    public Sprite level1BGSprite;
    public Sprite level2BGSprite;
    public Sprite level3BGSprite;

    public SpriteRenderer backgroundSR;

    [HideInInspector]
    public MenuButton button;

    public static FactoryLevelButton Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeLevel(int level)
    {
        UpgradesManager.Instance.factoryLevel = level;
        if(level == 0)
        {
            backgroundSR.sprite = level0BGSprite;
        }
        else if(level == 1)
        {
            backgroundSR.sprite = level1BGSprite;
        }
        else if (level == 2)
        {
            backgroundSR.sprite = level2BGSprite;
        }
        else if (level == 3)
        {
            backgroundSR.sprite = level3BGSprite;
        }
    }

    private void Start()
    {
        button = GetComponent<MenuButton>();
    }

    private void Update()
    {
        if (button.clicked)
        {
            button.clicked = false;
            if (!selected)
            {
                UpgradesManager.Instance.SelectFactoryButton();
            }
        }
    }

    public bool selected;
    public SpriteRenderer SR;
    public Sprite normalSprite;
    public Sprite pushSprite;
    public void Select()
    {
        selected = true;
        SR.sprite = pushSprite;
    }

    public void Unselect()
    {
        selected = false;
        SR.sprite = normalSprite;
    }

}
