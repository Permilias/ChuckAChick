using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesPanel : MonoBehaviour {

    public static UpgradesPanel Instance;
    public List<MenuButton> shownButtons;
    public List<MenuButton> hiddenButtons;

    public bool canSwitch = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (MenuButton button in shownButtons)
        {
            button.enabled = false;
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
        canSwitch = false;
        foreach (MenuButton button in hiddenButtons)
        {
            button.enabled = false;
        }
        yield return new WaitForSeconds(1);
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
        canSwitch = false;
        foreach (MenuButton button in shownButtons)
        {
            button.enabled = false;
        }
        yield return new WaitForSeconds(1);
        foreach (MenuButton button in hiddenButtons)
        {
            button.enabled = true;
        }
        canSwitch = true;
    }
}
