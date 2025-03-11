using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public List<CurrentResourceData> CurrentResourceDatas = new List<CurrentResourceData>();

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
