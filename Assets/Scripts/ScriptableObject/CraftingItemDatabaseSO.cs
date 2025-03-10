using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingItemDatabaseSO", menuName = "MySO/Crafting Item Database")]
public class CraftingItemDatabaseSO : ScriptableObject
{
    public List<ItemSO> ItemSOList = new List<ItemSO>();
}
