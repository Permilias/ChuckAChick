using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialButton : MonoBehaviour {

    public static TutorialButton Instance;

    public TextMeshPro tutoText;
    MenuButton button;

    public bool tutorialEnabled;

    private void Awake()
    {

        button = GetComponent<MenuButton>();
        Instance = this;
    }

    private void Start()
    {
        if(tutorialEnabled)
        {
            EnableTutorial();
        }
        else
        {
            DisableTutorial();
        }
    }


    private void Update()
    {
        if(button.clicked)
        {
            button.clicked = false;
            if(tutorialEnabled)
            {
                DisableTutorial();
            }
            else
            {
                EnableTutorial();
            }
        }
    }

    void EnableTutorial()
    {
        tutoText.text = "TUTORIAL ENABLED";
        tutorialEnabled = true;
        DataManager.Instance.Save(false);
    }

    void DisableTutorial()
    {
        tutoText.text = "TUTORIAL DISABLED";
        tutorialEnabled = false;
        DataManager.Instance.Save(false);
    }
}
