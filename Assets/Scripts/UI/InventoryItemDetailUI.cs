using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InventoryItemDetailUI : MonoBehaviour
{
    [Header("Project Reference")]
    [SerializeField] private GameAssetSO gameAssetSO;
    [SerializeField] private GameEventSO gameEventSO;

    [SerializeField] private Transform panel;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemCategory;
    [SerializeField] private Transform itemDescriptionGroup;
    [SerializeField] private TMP_Text itemDesc;
    [SerializeField] private TMP_Text itemSize;
    [SerializeField] private TMP_Text itemRepairCost;

    [Header("Resource Requirement")]
    [SerializeField] private List<InventoryItemResourceRequirementUI> inventoryItemResources = new();
    [SerializeField] private Transform resourceRequirementParent;

    private Dictionary<ResourceType, Sprite> resourceSpriteDict;

    private void Awake()
    {
        CacheResourceSprites();
    }

    private void Start()
    {
        panel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        gameEventSO.OnInventoryItemHoveredOver += RefreshUI;
    }

    private void OnDisable()
    {
        gameEventSO.OnInventoryItemHoveredOver -= RefreshUI;
    }

    private void CacheResourceSprites()
    {
        resourceSpriteDict = new Dictionary<ResourceType, Sprite>();
        foreach (var asset in gameAssetSO.ResourceAssets)
        {
            resourceSpriteDict[asset.ResourceType] = asset.Sprite;
        }
    }

    private void ClearResourceRequirements()
    {
        foreach (var item in inventoryItemResources)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void RefreshUI(ItemSO itemSO)
    {
        if (itemSO == null)
        {
            panel.gameObject.SetActive(false);
            return;
        }

        panel.gameObject.SetActive(true);

        ClearResourceRequirements();

        itemImage.sprite = itemSO.Sprite;
        itemName.text = itemSO.Name;
        itemCategory.text = itemSO.Category.ToString().Replace('_', ' ');

        bool hasDescription = !itemSO.Description.IsNullOrEmpty();
        itemDescriptionGroup.gameObject.SetActive(hasDescription);
        if (hasDescription) itemDesc.text = itemSO.Description;

        if (itemSO.Category != ItemCategory.Consumable)
        {
            itemSize.text = itemSO.Size.ToString();
            itemRepairCost.text = itemSO.RepairCost.ToString();
        }

        if (itemSO.ResourceRequirementDatas.IsNullOrEmpty()) return;

        for (int i = 0; i < itemSO.ResourceRequirementDatas.Count; i++)
        {
            if (!resourceSpriteDict.TryGetValue(itemSO.ResourceRequirementDatas[i].ResourceType, out Sprite resourceSprite)) continue;

            var currentResource = GameDataManager.Instance.CurrentResourceDatas.Find(x => x.ResourceType == itemSO.ResourceRequirementDatas[i].ResourceType);
            string currentAmount = currentResource != null ? currentResource.Amount.ToString() : "0";

            inventoryItemResources[i].gameObject.SetActive(true);
            inventoryItemResources[i].RefreshUI(resourceSprite, itemSO.Name, currentAmount, itemSO.ResourceRequirementDatas[i].Amount.ToString());
        }


    }
}
