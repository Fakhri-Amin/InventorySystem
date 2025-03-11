using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using MyBox;

public class CraftingInventoryUI : MonoBehaviour
{
    /// <summary>
    /// Represents a tab reference for inventory category filtering.
    /// </summary>
    [System.Serializable]
    public class TabReference
    {
        public ItemCategory TabCategory;
        public Button Button;
    }

    [Header("Project Reference")]
    [SerializeField] private CraftingItemDatabaseSO itemDatabaseSO;
    [SerializeField] private GameEventSO gameEventSO;

    [Header("Tab Management")]
    [SerializeField] private Button tabAllCategoryButton;
    [SerializeField] private List<TabReference> tabReferences = new List<TabReference>();

    [Header("Inventory UI")]
    [SerializeField] private CraftingInventorySlotUI inventorySlotPrefab;
    [SerializeField] private Transform inventoryEmptySlotPrefab;
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private int maxInventorySlotNumber = 48;

    [Header("Animation")]
    [SerializeField] private float shiftAmount = 20f;
    [SerializeField] private float fadeOutDuration = 0.2f;
    [SerializeField] private float fadeInDuration = 0.2f;

    private List<CraftingInventorySlotUI> createdInventorySlotUI = new();

    private void Start()
    {
        InitializeTabs();
        GenerateAllInventorySlots();
    }

    /// <summary>
    /// Initializes tab buttons to filter inventory by category.
    /// </summary>
    private void InitializeTabs()
    {
        RegisterAllCategoryTab();
        RegisterCategoryTabs();
    }

    /// <summary>
    /// Registers the "All Categories" tab and sets its selection state.
    /// </summary>
    private void RegisterAllCategoryTab()
    {
        var allCategoryTabUI = tabAllCategoryButton.GetComponent<InventoryCategoryTabUI>();

        tabAllCategoryButton.onClick.AddListener(() =>
        {
            GenerateAllInventorySlots();
            SelectTab(allCategoryTabUI);
        });

        allCategoryTabUI.SelectTabAtFirst();
    }

    /// <summary>
    /// Registers category-specific tabs while ensuring no duplicates.
    /// </summary>
    private void RegisterCategoryTabs()
    {
        HashSet<ItemCategory> registeredTabs = new HashSet<ItemCategory>();

        foreach (var tab in tabReferences)
        {
            if (tab.Button == null || registeredTabs.Contains(tab.TabCategory)) continue;

            registeredTabs.Add(tab.TabCategory);
            var tabUI = tab.Button.GetComponent<InventoryCategoryTabUI>();
            tabUI.DeselectTabAtFirst();

            tab.Button.onClick.AddListener(() =>
            {
                gameEventSO.OnInventoryItemHoveredOver?.Invoke(null);
                SelectTab(tabUI);
                GenerateInventorySlots(tab.TabCategory);
                gameEventSO.OnInventoryTabChanged?.Invoke();
            });
        }
    }

    /// <summary>
    /// Selects a tab with an animation.
    /// </summary>
    private void SelectTab(InventoryCategoryTabUI tabUI)
    {
        tabUI.SelectTab(fadeInDuration);
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

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (!createdInventorySlotUI.IsNullOrEmpty())
                EventSystem.current.SetSelectedGameObject(createdInventorySlotUI[0].gameObject);
        }
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

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (!createdInventorySlotUI.IsNullOrEmpty())
                EventSystem.current.SetSelectedGameObject(createdInventorySlotUI[0].gameObject);
        }
    }

    /// <summary>
    /// Creates an inventory slot and assigns item data.
    /// </summary>
    private void CreateSlot(ItemSO itemData)
    {
        CraftingInventorySlotUI slot = Instantiate(inventorySlotPrefab, inventoryParent);
        createdInventorySlotUI.Add(slot);
        slot.GenerateSlot(itemData);
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
        createdInventorySlotUI.Clear();
        EventSystem.current.SetSelectedGameObject(null);

        foreach (Transform child in inventoryParent)
        {
            Destroy(child.gameObject);
        }
    }
}
