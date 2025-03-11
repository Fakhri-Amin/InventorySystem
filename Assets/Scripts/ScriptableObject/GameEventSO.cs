using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventSO", menuName = "MySO/Game Event")]
public class GameEventSO : ScriptableObject
{
    public Action<ItemSO> OnInventoryItemHoveredOver;
    public Action OnInventoryTabChanged;
}
