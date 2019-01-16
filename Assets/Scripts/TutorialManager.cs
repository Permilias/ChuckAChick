using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public static TutorialManager Instance;

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
        yield return new WaitForSeconds(1);
    }
}
