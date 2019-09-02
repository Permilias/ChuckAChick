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

    public GameObject body;

    public RectTransform upgradeButtonRt;

    public RectTransform optionsButtonRt;

    public Vector3 shownPos, hiddenPos;
    public float shownX, hiddenX;

    Animator anim;

    public bool canSwitch = true;

    private void Awake()
    {
        Instance = this;
        //anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Vector3 pos = body.transform.position;
        shownPos = new Vector3(shownX, pos.y, 0);
        hiddenPos = new Vector3(hiddenX, pos.y, 0);
        body.transform.localPosition = hiddenPos;

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
        if(UpgradesManager.Instance.currentSelectedButton == null)
        {
            UpgradesManager.Instance.titleText.text = "";
            UpgradesManager.Instance.costText.text = "";
            UpgradesManager.Instance.upgradeText.text = "";
        }

        //anim.SetBool("isVisible", true);
        body.transform.DOLocalMove(shownPos, 0.8f).SetEase(Ease.OutBack, 0.5f);
        upgradeButtonRt.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
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
        hidingButton.transform.localScale = Vector3.zero;
        hidingButton.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        canSwitch = true;

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
        //anim.SetBool("isVisible", false);
        body.transform.DOLocalMove(hiddenPos, 0.6f).SetEase(Ease.InBack, 0.01f);

        hidingButton.transform.localScale = Vector3.one;
        hidingButton.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
        canSwitch = false;
        foreach (MenuButton button in shownButtons)
        {
            button.enabled = false;
        }
        yield return new WaitForSeconds(0.5f);
        hidingButton.SetActive(false);
        foreach (MenuButton button in hiddenButtons)
        {
            button.enabled = true;
        }
        showingButton.SetActive(true);
        upgradeButtonRt.DOScale(Vector3.one, 0.4f);
        canSwitch = true;
    }
}
