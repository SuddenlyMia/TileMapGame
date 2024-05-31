using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testingremovebutton : MonoBehaviour
{
    public InventoryObject Playerinventory;
    public InventoryObject RecipeInventory;
    public GameObject inventoryPrefab;
    public CraftingRecipe[] Recipes;

    private void Start()
    {
        PopulateRecipes();
    }

    private void PopulateRecipes()
    {
        foreach (var recipe in Recipes)
        {
            GameObject recipeUI = Instantiate(inventoryPrefab, transform);
            Image itemImage = recipeUI.transform.GetChild(0).GetComponent<Image>();
            itemImage.sprite = recipe.craftedItem.uiDisplay;

            // Add an onClick listener to the recipe UI element
            Button recipeButton = recipeUI.GetComponent<Button>();
            recipeButton.onClick.AddListener(() => CraftItem(recipe));
        }
    }

    private void CraftItem(CraftingRecipe recipe)
    {
        if (CanCraft(recipe))
        {
            foreach (var requiredItem in recipe.requiredItems)
            {
                Playerinventory.AddItem(new Item(requiredItem.item), -requiredItem.amount);
            }
            Playerinventory.AddItem(new Item(recipe.craftedItem), recipe.craftedAmount);
        }
        else
        {
            Debug.Log("Cannot craft " + recipe.craftedItem.name + ". Required items are missing.");
        }
    }

    private bool CanCraft(CraftingRecipe recipe)
    {
        foreach (var requiredItem in recipe.requiredItems)
        {
            if (Playerinventory.CheckAmount(new Item(requiredItem.item)) < requiredItem.amount)
            {
                return false;
            }
        }
        return true;
    }
}