using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    private ItemSO itemSO;

    public ItemSO ItemSO => itemSO;

    public void GenerateSlot(ItemSO itemSO)
    {
        this.itemSO = itemSO;
        gameObject.name = itemSO.Name;

        if (itemImage) itemImage.sprite = itemSO.Sprite;
    }
}
