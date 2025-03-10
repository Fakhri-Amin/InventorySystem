using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "MySO/Item")]
public class ItemSO : ScriptableObject
{
    public String Name;
    public Sprite Sprite;
    public ItemCategory Category;
    public String Description;
    public int Size;
    public int RepairCost;
    public List<ResourceRequirementData> ResourceRequirementDatas = new List<ResourceRequirementData>();
}
