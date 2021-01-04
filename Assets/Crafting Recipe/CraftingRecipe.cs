using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct itemAmount
{
    public Item item;
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<itemAmount> Materials;
    public GameObject Result;
}
