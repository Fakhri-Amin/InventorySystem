using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackpackItemDatabaseSO", menuName = "MySO/Backpack Item Database")]
public class BackpackItemDatabaseSO : ScriptableObject
{
    public List<ItemSO> ItemSOList = new List<ItemSO>();
}
