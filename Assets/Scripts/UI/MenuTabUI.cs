using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
using MoreMountains.Feedbacks;

public class MenuTabUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static Action<MenuTabUI, float> OnTabSelected;

    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private CanvasGroup outlineImage;

    [Header("Animation")]
    [SerializeField] private float fadeInDuration;
    [SerializeField] private Ease iconMoveEase;

    [Header("Config")]
    [SerializeField] private Color32 selectedColor;
    [SerializeField] private Color32 deselectedColor;
    [SerializeField] private float selectedIconPosition = 12;

    private bool isSelected = false;

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
        AudioManager.Instance.PlayClickSound();

        iconImage.DOColor(selectedColor, fadeInDuration);
        iconImage.transform.DOLocalMoveY(selectedIconPosition, fadeInDuration).SetEase(iconMoveEase);

        nameText.gameObject.SetActive(true);
        outlineImage.DOFade(0, 0);

        isSelected = true;

        OnTabSelected?.Invoke(this, duration);
    }

    public void DeselectTab(MenuTabUI tab, float duration)
    {
        if (tab == this) return;

        iconImage.DOColor(deselectedColor, fadeInDuration);
        nameText.gameObject.SetActive(false);
        iconImage.transform.DOLocalMoveY(0, 0);
        isSelected = false;
    }

    public void SelectTabAtFirst()
    {
        iconImage.DOColor(selectedColor, fadeInDuration);
        nameText.gameObject.SetActive(true);
    }

    public void DeselectTabAtFirst()
    {
        iconImage.DOColor(deselectedColor, fadeInDuration);
        nameText.gameObject.SetActive(false);
        outlineImage.DOFade(0, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected) return;

        AudioManager.Instance.PlayHoverSound();
        outlineImage.DOFade(0.5f, fadeInDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outlineImage.DOFade(0, fadeInDuration);
    }
}
