using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScreen : MonoBehaviour
{
    public static OpeningScreen Instance;

    public GameObject graphics;
    Animator anim;

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Open();
    }

    public void Open()
    {
        StartCoroutine("OpenCoroutine");
    }

    public void Close()
    {
        StartCoroutine("CloseCoroutine");
    }

    public float openingDelay;
    public IEnumerator OpenCoroutine()
    {
        graphics.SetActive(true);
        anim.SetTrigger("open");
        yield return new WaitForSeconds(openingDelay);
        graphics.SetActive(false);
    }

    public float closingDelay;
    public IEnumerator CloseCoroutine()
    {
        graphics.SetActive(true);
        anim.SetTrigger("close");
        yield return new WaitForSeconds(closingDelay);
    }
}
