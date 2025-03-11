using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class BackpackInventoryUI : MonoBehaviour
{
    [Header("Project Reference")]
    [SerializeField] private CraftingItemDatabaseSO itemDatabaseSO;
    [SerializeField] private GameEventSO gameEventSO;

    [Header("Inventory UI")]
    [SerializeField] private BackpackInventorySlotUI inventorySlotPrefab;
    [SerializeField] private Transform inventoryEmptySlotPrefab;
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private int maxInventorySlotNumber = 40;

    [Header("Animation")]
    [SerializeField] private float shiftAmount = 20f;
    [SerializeField] private float fadeOutDuration = 0.2f;
    [SerializeField] private float fadeInDuration = 0.2f;

    [Header("Feedbacks")]
    [SerializeField] private MMFeedbacks appearFeedbacks;

    private void Start()
    {
        GenerateAllInventorySlots();
    }

    void OnEnable()
    {
        gameEventSO.OnBackpackInventoryItemChanged += GenerateAllInventorySlots;
    }

    void OnDisable()
    {
        gameEventSO.OnBackpackInventoryItemChanged -= GenerateAllInventorySlots;
    }

    /// <summary>
    /// Generates all inventory slots, filling empty slots if necessary.
    /// </summary>
    private void GenerateAllInventorySlots()
    {
        ClearAllSlots();

        appearFeedbacks.PlayFeedbacks();

        List<CurrentItemData> currentItemDatas = GameDataManager.Instance.CurrentItemDatas;
        int itemCount = currentItemDatas.Count;

        foreach (var item in currentItemDatas)
        {
            ItemSO itemSO = itemDatabaseSO.ItemSOList.Find(i => i.Name == item.ID);
            CreateSlot(itemSO, item.Amount);
        }

        CreateEmptySlots(maxInventorySlotNumber - itemCount);
    }

    /// <summary>
    /// Creates an inventory slot and assigns item data.
    /// </summary>
    private void CreateSlot(ItemSO itemSO, int currentAmount)
    {
        BackpackInventorySlotUI backpackInventorySlot = Instantiate(inventorySlotPrefab, inventoryParent);
        backpackInventorySlot.GenerateSlot(itemSO, currentAmount);
    }

    /// <summary>
    /// Creates empty inventory slots to maintain a consistent grid.
    /// </summary>
    private void CreateEmptySlots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(inventoryEmptySlotPrefab, inventoryParent);
        }
    }

    /// <summary>
    /// Clears all existing inventory slots.
    /// </summary>
    private void ClearAllSlots()
    {
        // inventoryParent.GetComponent<CanvasGroup>().DOFade(0, 0);

        foreach (Transform child in inventoryParent)
        {
            Destroy(child.gameObject);
        }
    }
}
