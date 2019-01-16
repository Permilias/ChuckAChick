using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour {

    public static TutorialManager Instance;
    public TextMeshPro tutorialText;
    public Animator tutorialTextAnim;
    public Animator tutorialSpriteAnim;
    public SpriteRenderer tutorialSR;

    public string text1;
    public string text2;
    public string text3;
    public string text4;

    public bool waiting1;
    public bool waiting2;
    public bool waiting3;
    public bool waiting4;

    public Sprite sprite1;

    private void Awake()
    {
        Instance = this;
    }

    public void StartTutorial()
    {
        StartCoroutine("WaitThenStartTutorial");
    }

    IEnumerator WaitThenStartTutorial()
    {
        MatManager.Instance.SetSpeed(0.5f);
        yield return new WaitForSeconds(1);
        tutorialText.text = text1;
        waiting1 = true;
        EggGenerator.Instance.SpawnSpecificEgg(0);
    }

    private void Update()
    {
        if(waiting1)
        {

        }
    }
}
