using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDetailUI : MonoBehaviour
{
    [Header("Project Reference")]
    [SerializeField] private GameAssetSO gameAssetSO;

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemCategory;
    [SerializeField] private Transform itemDescriptionGroup;
    [SerializeField] private TMP_Text itemDesc;
    [SerializeField] private TMP_Text itemSize;
    [SerializeField] private TMP_Text itemRepairCost;

    [Header("Resource Requirement")]
    [SerializeField] private InventoryItemResourceRequirementUI itemResourceRequirementUI;
    [SerializeField] private Transform resourceRequirementParent;

    private void Start()
    {
        foreach (Transform child in resourceRequirementParent)
        {
            if (child.GetComponent<InventoryItemResourceRequirementUI>()) Destroy(child.gameObject);
        }
    }

    public void RefreshUI(ItemSO itemSO)
    {
        itemImage.sprite = itemSO.Sprite;
        itemName.text = itemSO.Name;
        itemCategory.text = itemSO.Category.ToString().Replace('_', ' ');

        if (!itemSO.Description.IsNullOrEmpty())
        {
            itemDescriptionGroup.gameObject.SetActive(true);
            itemDesc.text = itemSO.Description;
        }

        if (itemSO.Category != ItemCategory.Consumable)
        {
            itemSize.text = itemSO.Size.ToString();
            itemRepairCost.text = itemSO.RepairCost.ToString();
        }

        if (!itemSO.ResourceRequirementDatas.IsNullOrEmpty())
        {
            foreach (var item in itemSO.ResourceRequirementDatas)
            {
                InventoryItemResourceRequirementUI resourceUI = Instantiate(itemResourceRequirementUI, resourceRequirementParent);
                Sprite resourceSprite = gameAssetSO.ResourceAssets.Find(i => i.ResourceType == item.ResourceType).Sprite;
                resourceUI.RefreshUI(resourceSprite, itemSO.Name, GameDataManager.Instance.CurrentResourceDatas.Find(i => i.ResourceType == item.ResourceType).Amount.ToString(), item.Amount.ToString());
            }
        }
    }
}
