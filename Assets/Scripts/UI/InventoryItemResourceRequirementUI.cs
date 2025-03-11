using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemResourceRequirementUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private Color32 highlightedColor;
    [SerializeField] private Color32 insufficientColor;

    public void RefreshUI(Sprite resourceSprite, String resourceName, int currentResourceAmount, int resourceRequiredAmount)
    {
        image.sprite = resourceSprite;
        nameText.text = resourceName;

        if (currentResourceAmount >= resourceRequiredAmount)
        {
            string highlightecColorHex = ColorUtility.ToHtmlStringRGB(highlightedColor);
            amountText.text = $"<color=#{highlightecColorHex}>{currentResourceAmount}</color>/{resourceRequiredAmount}";
        }
        else
        {
            string insufficientColorHex = ColorUtility.ToHtmlStringRGB(insufficientColor);
            amountText.text = $"<color=#{insufficientColorHex}>{currentResourceAmount}</color>/{resourceRequiredAmount}";
        }
    }
}
