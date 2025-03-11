using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    [Header("Project Reference")]
    [SerializeField] private GameEventSO gameEventSO;

    public List<CurrentResourceData> CurrentResourceDatas = new List<CurrentResourceData>();
    public List<CurrentItemData> CurrentItemDatas = new List<CurrentItemData>();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddItemData(ItemSO itemSO, int amount)
    {
        CurrentItemData existingItemData = CurrentItemDatas.Find(x => x.ID == itemSO.Name);

        if (existingItemData != null)
        {
            existingItemData.Amount++;
        }
        else
        {
            CurrentItemData currentItemData = new CurrentItemData() { ID = itemSO.Name, Amount = amount };
            CurrentItemDatas.Add(currentItemData);
        }

        gameEventSO.OnBackpackInventoryItemChanged?.Invoke();
    }
}

[System.Serializable]
public class CurrentResourceData
{
    public ResourceType ResourceType;
    public int Amount;
}

[System.Serializable]
public class CurrentItemData
{
    public String ID;
    public int Amount;
}
