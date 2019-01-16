using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour {

    public static TutorialManager Instance;
    public TextMeshPro tutorialText;
    public Animator tutorialTextAnim;

    public string welcomeText;
    public string text1;
    public string text2;
    public string text3;
    public string text4;
    public string text5;
    public string text6;
    public string text7;
    public string text8;
    public string endText;

    public bool waiting1;
    public bool waiting2;
    public bool waiting3;
    public bool waiting4;
    public bool waiting5;
    public bool waiting6;
    public bool waiting7;
    public bool waiting8;

    public GameObject sprite1;
    public GameObject sprite2;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        sprite1.SetActive(false);
        sprite2.SetActive(false);
    }

    public void StartTutorial()
    {
        StartCoroutine("WaitThenStartTutorial");
    }

    IEnumerator WaitThenStartTutorial()
    {
        yield return new WaitForSeconds(1.5f);
        MatManager.Instance.SetSpeed(0.7f);
        tutorialText.text = welcomeText;
        tutorialTextAnim.SetTrigger("show");
        yield return new WaitForSeconds(1.5f);
        EggGenerator.Instance.SpawnSpecificEgg(0);
        yield return new WaitForSeconds(1.5f);
        tutorialText.text = text1;
        waiting1 = true;
    }

    IEnumerator Tutorial1()
    {

        yield return new WaitForSeconds(0.5f);
        waiting2 = true;
        tutorialText.text = text2;
        sprite1.SetActive(true);
    }

    IEnumerator Tutorial2()
    {
        sprite1.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        waiting3 = true;
        EggGenerator.Instance.SpawnSpecificEgg(1);
        tutorialText.text = text3;
        sprite2.SetActive(true);
    }

    IEnumerator Tutorial3()
    {
        sprite2.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        waiting4 = true;
        EggGenerator.Instance.SpawnSpecificEgg(2);
        tutorialText.text = text4;
        sprite2.SetActive(true);
    }

    IEnumerator Tutorial4()
    {
        sprite2.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        waiting5 = true;
        EggGenerator.Instance.SpawnSpecificEgg(3);
        EggGenerator.Instance.SpawnSpecificEgg(1);
        EggGenerator.Instance.SpawnSpecificEgg(1);
        EggGenerator.Instance.SpawnSpecificEgg(1);
        tutorialText.text = text5;
    }
    IEnumerator Tutorial5()
    {

        yield return new WaitForSeconds(0.5f);
        waiting6 = true;
        EggGenerator.Instance.SpawnSpecificEgg(4);
        EggGenerator.Instance.SpawnSpecificEgg(0);
        EggGenerator.Instance.SpawnSpecificEgg(0);
        EggGenerator.Instance.SpawnSpecificEgg(0);
        tutorialText.text = text6;
    }
    IEnumerator Tutorial6()
    {

        yield return new WaitForSeconds(0.5f);
        waiting7 = true;
        EggGenerator.Instance.SpawnSpecificEgg(5);
        EggGenerator.Instance.SpawnSpecificEgg(0);
        EggGenerator.Instance.SpawnSpecificEgg(0);
        EggGenerator.Instance.SpawnSpecificEgg(0);
        tutorialText.text = text7;
    }
    IEnumerator Tutorial7()
    {

        yield return new WaitForSeconds(0.5f);
        waiting8 = true;
        EggGenerator.Instance.SpawnSpecificEgg(6);
        EggGenerator.Instance.SpawnSpecificEgg(0);
        EggGenerator.Instance.SpawnSpecificEgg(0);
        EggGenerator.Instance.SpawnSpecificEgg(0);
        tutorialText.text = text8;
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(1.5f);
        tutorialText.text = endText;
        MatManager.Instance.Reset();
        EggGenerator.Instance.canSpawn = true;
        yield return new WaitForSeconds(1.5f);
        tutorialTextAnim.SetTrigger("hide");
        yield return new WaitForSeconds(0.5f);
        tutorialText.text = "";

    }

    public void NextTutorial(int index)
    {
        if (index == 1)
        {
            waiting1 = false;
            StartCoroutine("Tutorial1");
        }
        else if (index == 2)
        {
            waiting2 = false;
            StartCoroutine("Tutorial2");
        }
        else if(index == 3)
        {
            waiting3 = false;
            StartCoroutine("Tutorial3");
        }
        else if(index == 4)
        {
            waiting4 = false;
            StartCoroutine("Tutorial4");
        }
        else if (index == 5)
        {
            waiting5 = false;
            StartCoroutine("Tutorial5");
        }
        else if (index == 6)
        {
            waiting6 = false;
            StartCoroutine("Tutorial6");
        }
        else if (index == 7)
        {
            waiting7 = false;
            StartCoroutine("Tutorial7");
        }
        else
        {
            StartCoroutine("EndTutorial");
        }
    }
}
