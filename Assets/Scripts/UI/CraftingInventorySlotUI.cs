using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class CraftingInventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Project Reference")]
    [SerializeField] private GameEventSO gameEventSO;

    [SerializeField] private Button inventoryButton;
    [SerializeField] private Image itemImage;
    [SerializeField] private CanvasGroup hoveredOutline;
    [SerializeField] private bool isHoverOverUI;

    [Header("Hover Over UI")]
    [SerializeField] private CanvasGroup hoverOverUI;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemSize;
    [SerializeField] private TMP_Text itemRepairCost;

    [Header("Color")]
    [SerializeField] private Color32 highlightedColor;
    [SerializeField] private Color32 insufficientColor;

    [Header("Animation")]
    [SerializeField] private float fadeInDuration = 0.2f;

    [Header("Feedbacks")]
    [SerializeField] private MMFeedbacks appearFeedbacks;
    [SerializeField] private MMFeedbacks clickFeedbacks;

    private ItemSO itemSO;

    public ItemSO ItemSO => itemSO;

    public void GenerateSlot(ItemSO itemSO)
    {
        this.itemSO = itemSO;
        gameObject.name = itemSO.Name;

        inventoryButton.onClick.AddListener(() =>
        {
            clickFeedbacks.PlayFeedbacks();
            GameDataManager.Instance.AddItemData(itemSO, 1);
        });

        if (itemImage) itemImage.sprite = itemSO.Sprite;

        if (isHoverOverUI)
        {
            itemName.text = itemSO.Name;
            itemSize.text = itemSO.Size.ToString();
            itemRepairCost.text = itemSO.RepairCost.ToString();
            hoverOverUI.gameObject.SetActive(false);
        }

        hoveredOutline.DOFade(0, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveredOutline.gameObject.SetActive(true);
        hoveredOutline.alpha = 1;
        hoveredOutline.DOFade(1, fadeInDuration);
        if (hoverOverUI) hoverOverUI.gameObject.SetActive(true);
        if (appearFeedbacks) appearFeedbacks.PlayFeedbacks();
        if (itemSO) gameEventSO.OnInventoryItemHoveredOver?.Invoke(itemSO);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveredOutline.gameObject.SetActive(false);
        if (hoverOverUI) hoverOverUI.gameObject.SetActive(false);
        if (itemSO) gameEventSO.OnInventoryItemHoveredOver?.Invoke(null);
    }
}
