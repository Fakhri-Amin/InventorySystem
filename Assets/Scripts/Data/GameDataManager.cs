using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public List<CurrentResourceData> CurrentResourceDatas = new List<CurrentResourceData>();
    public List<CurrentItemData> CurrentItemDatas = new List<CurrentItemData>();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
