using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpgradesPanel : MonoBehaviour {

    public static UpgradesPanel Instance;
    public List<MenuButton> shownButtons;
    public List<MenuButton> hiddenButtons;
    public GameObject showingButton;
    public GameObject hidingButton;

    public RectTransform upgradeButtonRt;

    public RectTransform optionsButtonRt;

    Animator anim;

    public bool canSwitch = true;

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        hidingButton.SetActive(false);
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

            optionsButtonRt.DOScale(Vector3.zero, 0.4f);
            SoundManager.Instance.PlaySound(SoundManager.Instance.buttonPress);
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
        showingButton.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        foreach (MenuButton button in shownButtons)
        {
            button.enabled = true;
        }
        hidingButton.SetActive(true);
        canSwitch = true;
        upgradeButtonRt.DOScale(Vector3.zero, 0.5f);
        BuyButton.Instance.Show();
    }

    public void HidePanel()
    {
        if(canSwitch)
        {
            if(BuyButton.Instance.active)
            {
                BuyButton.Instance.SetInactive();
            }
            BuyButton.Instance.Hide();
            optionsButtonRt.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack, 1.1f);
            SoundManager.Instance.PlaySound(SoundManager.Instance.goBackButtonPress);
            StartCoroutine("HidePanelCoroutine");
        }
    }

    IEnumerator HidePanelCoroutine()
    {
        anim.SetBool("isVisible", false);
        hidingButton.SetActive(false);
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
        showingButton.SetActive(true);
        upgradeButtonRt.DOScale(Vector3.one, 0.4f);
        canSwitch = true;
    }
}
