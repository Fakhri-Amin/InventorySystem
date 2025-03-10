using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private bool isHoverOverUI;

    [Header("Hover Over UI")]
    [SerializeField] private CanvasGroup hoverOverUI;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemSize;
    [SerializeField] private TMP_Text itemRepairCost;

    [Header("Animation")]
    [SerializeField] private float fadeInDuration = 0.2f;

    private ItemSO itemSO;

    public ItemSO ItemSO => itemSO;

    public void GenerateSlot(ItemSO itemSO)
    {
        this.itemSO = itemSO;
        gameObject.name = itemSO.Name;

        if (itemImage) itemImage.sprite = itemSO.Sprite;

        if (isHoverOverUI)
        {
            itemName.text = itemSO.Name;
            itemSize.text = itemSO.Size.ToString();
            itemRepairCost.text = itemSO.RepairCost.ToString();
        }

        hoverOverUI.DOFade(0, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverOverUI.DOFade(1, fadeInDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverOverUI.DOFade(0, fadeInDuration);
    }
}
