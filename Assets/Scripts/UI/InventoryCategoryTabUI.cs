using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class InventoryCategoryTabUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static Action<InventoryCategoryTabUI, float> OnTabSelected;

    [SerializeField] private Image buttonImage;
    [SerializeField] private CanvasGroup iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private CanvasGroup outlineImage;

    [Header("Animation")]
    [SerializeField] private float fadeInDuration;

    [Header("Config")]
    [SerializeField] private float hoveredAlphaValue;
    [SerializeField] private float disableAlphaValue;

    private void Start()
    {
    }

    void OnEnable()
    {
        OnTabSelected += DeselectTab;
        buttonImage.GetComponent<CanvasGroup>().alpha = 0;
        buttonImage.GetComponent<CanvasGroup>().DOFade(1, fadeInDuration);
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
        outlineImage.DOFade(hoveredAlphaValue, fadeInDuration);

        AudioManager.Instance.PlayClickSound();

        OnTabSelected?.Invoke(this, duration);
    }

    public void DeselectTab(InventoryCategoryTabUI tab, float duration)
    {
        if (tab == this) return;

        buttonImage.DOFade(0, duration);
        iconImage.DOFade(0.2f, duration);
        nameText.gameObject.SetActive(false);
        outlineImage.DOFade(disableAlphaValue, 0);
    }

    public void SelectTabAtFirst()
    {
        buttonImage.DOFade(1, 0);
        iconImage.DOFade(1, 0);
        nameText.gameObject.SetActive(true);
        outlineImage.DOFade(hoveredAlphaValue, fadeInDuration);
    }

    public void DeselectTabAtFirst()
    {
        buttonImage.DOFade(0, 0);
        iconImage.DOFade(0.2f, 0);
        nameText.gameObject.SetActive(false);
        outlineImage.DOFade(disableAlphaValue, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlayHoverSound();
        outlineImage.DOFade(hoveredAlphaValue, fadeInDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outlineImage.DOKill();
        outlineImage.DOFade(disableAlphaValue, 0);
    }
}
