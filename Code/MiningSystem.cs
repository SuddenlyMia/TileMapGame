using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningSystem : MonoBehaviour
{
    public Tilemap tilemapInteract;  // Reference to the interactable Tilemap
    public InventoryObject inventory;
    private PlayerController playerController;
    public ItemObject rockItem;  // Reference to the Rock ItemObject
	public ItemObject logItem;
	public ItemObject twigItem;
	public ItemObject leafesItem;

    void Start()
    {
        // Get the PlayerController component
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        // Check if the player presses the mining key (e.g., E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryMine();
        }
    }

    void TryMine()
    {
        // Get the position of the tile in front of the player
        Vector3Int gridPositionInFront = playerController.GetCurrentGridPosition() + playerController.lastMoveDirection;

        // Get the tile at the position
        TileBase tileInFront = tilemapInteract.GetTile(gridPositionInFront);

        // Check if the tile is a rock
        if (tileInFront != null && tileInFront.name == "Rock")
        {
            // Remove the rock tile
            tilemapInteract.SetTile(gridPositionInFront, null);

            // Add the rock item to the inventory
            inventory.AddItem(new Item(rockItem), 1);
            Debug.Log("Rock mined and added to inventory at position: " + gridPositionInFront);
        }
		else if (tileInFront != null && tileInFront.name == "Log")
        {
            // Remove the rock tile
            tilemapInteract.SetTile(gridPositionInFront, null);

            // Add the rock item to the inventory
            inventory.AddItem(new Item(logItem), 1);
            Debug.Log("Log mined and added to inventory at position: " + gridPositionInFront);
        }
		else if (tileInFront != null && tileInFront.name == "Bush")
        {
            // Remove the rock tile
            tilemapInteract.SetTile(gridPositionInFront, null);

            // Add the rock item to the inventory
			int x = Random.Range(0, 2);
			if (x != 0)
			{
				inventory.AddItem(new Item(twigItem), x);
			}
			inventory.AddItem(new Item(leafesItem), Random.Range(1, 3));
            Debug.Log("Bush mined and added to inventory at position: " + gridPositionInFront);
        }
		else if (tileInFront != null && tileInFront.name == "Tree")
        {
            // Remove the rock tile
            tilemapInteract.SetTile(gridPositionInFront, null);
            inventory.AddItem(new Item(twigItem), Random.Range(1, 3));
            inventory.AddItem(new Item(leafesItem), Random.Range(3, 5));
            inventory.AddItem(new Item(logItem), Random.Range(2, 3));
            Debug.Log("Tree Chopped and added to inventory at position: " + gridPositionInFront);
        }
    }
	
	private void OnApplicationQuit()
	{
		inventory.Container.Items = new InventorySlot[28];
	}
}
