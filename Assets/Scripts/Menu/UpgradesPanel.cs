using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesPanel : MonoBehaviour {

    public static UpgradesPanel Instance;
    public List<MenuButton> shownButtons;
    public List<MenuButton> hiddenButtons;

    Animator anim;

    public bool canSwitch = true;

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        foreach (MenuButton button in shownButtons)
        {
            button.enabled = false;
        }
        foreach (MenuButton button in hiddenButtons)
        {
            button.enabled = true;
        }
    }

    public void ShowPanel()
    {
        if(canSwitch)
        {
            StartCoroutine("ShowPanelCoroutine");
        }
    }

    IEnumerator ShowPanelCoroutine()
    {
        anim.SetBool("isVisible", true);
        canSwitch = false;
        foreach (MenuButton button in hiddenButtons)
        {
            button.enabled = false;
        }
        yield return new WaitForSeconds(0.5f);
        foreach (MenuButton button in shownButtons)
        {
            button.enabled = true;
        }
        canSwitch = true;
    }

    public void HidePanel()
    {
        if(canSwitch)
        {
            StartCoroutine("HidePanelCoroutine");
        }
    }

    IEnumerator HidePanelCoroutine()
    {
        anim.SetBool("isVisible", false);
        canSwitch = false;
        foreach (MenuButton button in shownButtons)
        {
            button.enabled = false;
        }
        yield return new WaitForSeconds(0.5f);
        foreach (MenuButton button in hiddenButtons)
        {
            button.enabled = true;
        }
        canSwitch = true;
    }
}
