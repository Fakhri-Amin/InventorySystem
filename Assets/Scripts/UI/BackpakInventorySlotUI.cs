using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class BackpackInventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Project Reference")]
    [SerializeField] private GameEventSO gameEventSO;

    [SerializeField] private Button inventoryButton;
    [SerializeField] private Image itemImage;
    [SerializeField] private CanvasGroup hoveredOutline;
    [SerializeField] private TMP_Text itemAmount;

    [Header("Hover Over UI")]
    [SerializeField] private CanvasGroup hoverOverUI;
    [SerializeField] private TMP_Text itemName;

    [Header("Animation")]
    [SerializeField] private float fadeInDuration = 0.2f;

    [Header("Feedbacks")]
    [SerializeField] private MMFeedbacks clickFeedbacks;

    private ItemSO itemSO;

    public ItemSO ItemSO => itemSO;

    public void GenerateSlot(ItemSO itemSO, int currentAmount)
    {
        this.itemSO = itemSO;
        // gameObject.name = itemSO.Name;

        inventoryButton.onClick.AddListener(() =>
        {
            clickFeedbacks.PlayFeedbacks();
        });

        if (itemImage) itemImage.sprite = itemSO.Sprite;

        itemName.text = itemSO.Name;
        hoverOverUI.gameObject.SetActive(false);

        itemAmount.text = currentAmount.ToString();

        hoveredOutline.DOFade(0, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveredOutline.gameObject.SetActive(true);
        hoveredOutline.alpha = 1;
        hoveredOutline.DOFade(1, fadeInDuration);
        if (hoverOverUI) hoverOverUI.gameObject.SetActive(true);
        if (itemSO) gameEventSO.OnInventoryItemHoveredOver?.Invoke(itemSO);

        AudioManager.Instance.PlayHoverSound();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveredOutline.gameObject.SetActive(false);
        if (hoverOverUI) hoverOverUI.gameObject.SetActive(false);
        if (itemSO) gameEventSO.OnInventoryItemHoveredOver?.Invoke(null);
    }
}
