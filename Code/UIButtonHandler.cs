using UnityEngine;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour
{
    public Button saveButton;  // Reference to the save button
    public Button loadButton;  // Reference to the load button
    public MiningSystem miningSystem;  // Reference to the MiningSystem

    private void Start()
    {
        // Add the Save method to the save button's onClick event
        saveButton.onClick.AddListener(OnSaveButtonPressed);
        // Add the Load method to the load button's onClick event
        loadButton.onClick.AddListener(OnLoadButtonPressed);
    }

    public void OnSaveButtonPressed()
    {
        if (miningSystem != null)
        {
            miningSystem.inventory.Save();
            Debug.Log("Inventory saved.");
        }
    }

    public void OnLoadButtonPressed()
    {
        if (miningSystem != null)
        {
            miningSystem.inventory.Load();
            Debug.Log("Inventory loaded.");
        }
    }
}
 