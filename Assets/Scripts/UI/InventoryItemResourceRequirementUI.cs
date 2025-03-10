using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemResourceRequirementUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text amountText;

    public void RefreshUI(Sprite resourceSprite, String resourceName, String currentResourceAmount, String resourceRequiredAmount)
    {
        // string resourceName = resource.ResourceType.ToString().Replace('_', ' ');
        image.sprite = resourceSprite;
        nameText.text = resourceName;
        amountText.text = $"{currentResourceAmount}/{resourceRequiredAmount}";
    }
}
