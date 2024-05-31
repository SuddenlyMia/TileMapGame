using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Inventory System/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [System.Serializable]
    public class RequiredItem
    {
        public ItemObject item;
        public int amount;
    }

    public RequiredItem[] requiredItems;
    public ItemObject craftedItem;
    public int craftedAmount;
}
