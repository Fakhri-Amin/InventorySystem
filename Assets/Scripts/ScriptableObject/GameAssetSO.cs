using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameAssetSO", menuName = "MySO/Game Asset")]
public class GameAssetSO : ScriptableObject
{
    [Serializable]
    public class ResourceAsset
    {
        public ResourceType ResourceType;
        public Sprite Sprite;
    }

    public List<ResourceAsset> ResourceAssets = new List<ResourceAsset>();
}
