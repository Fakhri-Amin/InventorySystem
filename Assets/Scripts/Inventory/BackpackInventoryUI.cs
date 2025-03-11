using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BackpackInventoryUI : MonoBehaviour
{
    [Header("Project Reference")]
    [SerializeField] private BackpackItemDatabaseSO itemDatabaseSO;

    [Header("Inventory UI")]
    [SerializeField] private InventorySlotUI inventorySlotPrefab;
    [SerializeField] private Transform inventoryEmptySlotPrefab;
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private int maxInventorySlotNumber = 40;

    [Header("Animation")]
    [SerializeField] private float shiftAmount = 20f;
    [SerializeField] private float fadeOutDuration = 0.2f;
    [SerializeField] private float fadeInDuration = 0.2f;

    private void Start()
    {
        GenerateAllInventorySlots();
    }

    /// <summary>
    /// Generates all inventory slots, filling empty slots if necessary.
    /// </summary>
    private void GenerateAllInventorySlots()
    {
        ClearAllSlots();

        int itemCount = itemDatabaseSO.ItemSOList.Count;
        foreach (var item in itemDatabaseSO.ItemSOList)
        {
            CreateSlot(item);
        }

        CreateEmptySlots(maxInventorySlotNumber - itemCount);

        inventoryParent.GetComponent<CanvasGroup>().DOFade(1, fadeInDuration);
    }

    /// <summary>
    /// Generates inventory slots based on selected category.
    /// </summary>
    private void GenerateInventorySlots(ItemCategory itemCategory)
    {
        ClearAllSlots();

        int itemCount = 0;
        foreach (var item in itemDatabaseSO.ItemSOList)
        {
            if (item.Category == itemCategory)
            {
                CreateSlot(item);
                itemCount++;
            }
        }

        CreateEmptySlots(maxInventorySlotNumber - itemCount);

        inventoryParent.GetComponent<CanvasGroup>().DOFade(1, fadeInDuration);
    }

    /// <summary>
    /// Creates an inventory slot and assigns item data.
    /// </summary>
    private void CreateSlot(ItemSO itemData)
    {
        Instantiate(inventorySlotPrefab, inventoryParent).GenerateSlot(itemData);
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
        inventoryParent.GetComponent<CanvasGroup>().DOFade(0, 0);

        foreach (Transform child in inventoryParent)
        {
            Destroy(child.gameObject);
        }
    }
}
