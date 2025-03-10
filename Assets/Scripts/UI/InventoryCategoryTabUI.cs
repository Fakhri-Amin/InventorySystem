using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class InventoryCategoryTabUI : MonoBehaviour
{
    public static Action<InventoryCategoryTabUI, float> OnTabSelected;

    [SerializeField] private Image buttonImage;
    [SerializeField] private CanvasGroup iconImage;
    [SerializeField] private TMP_Text nameText;

    void OnEnable()
    {
        OnTabSelected += DeselectTab;
    }

    void OnDisable()
    {
        OnTabSelected -= DeselectTab;
    }

    public void SelectTab(float duration)
    {
        buttonImage.DOFade(1, duration);
        iconImage.DOFade(1, duration);
        nameText.gameObject.SetActive(true);

        OnTabSelected?.Invoke(this, duration);
    }

    public void DeselectTab(InventoryCategoryTabUI tab, float duration)
    {
        if (tab == this) return;

        buttonImage.DOFade(0, duration);
        iconImage.DOFade(0.2f, duration);
        nameText.gameObject.SetActive(false);
    }

    public void SelectTabAtFirst()
    {
        buttonImage.DOFade(1, 0);
        iconImage.DOFade(1, 0);
        nameText.gameObject.SetActive(true);
    }

    public void DeselectTabAtFirst()
    {
        buttonImage.DOFade(0, 0);
        iconImage.DOFade(0.2f, 0);
        nameText.gameObject.SetActive(false);
    }
}
