using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Tilemap tilemapGround;  // Reference to the ground Tilemap
    public Tilemap tilemapInteract;  // Reference to the interactable Tilemap

    public float moveSpeed = 5f;  // Speed of movement

    private Vector3Int currentGridPosition;  // Current position in grid coordinates
    public Vector3Int lastMoveDirection { get; private set; } = Vector3Int.zero; // Last movement direction

    private Dictionary<string, bool> walkableTiles;  // Dictionary to store walkable tile information

    void Start()
    {
		
        // Initialize player's position on the grid
        currentGridPosition = tilemapGround.WorldToCell(transform.position);
        transform.position = tilemapGround.GetCellCenterWorld(currentGridPosition);

        // Initialize walkable tile information
        InitializeWalkableTiles();
    }
 
    void Update()
    {
        // Handle player movement input
        Vector3Int moveDirection = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = new Vector3Int(0, 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = new Vector3Int(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveDirection = new Vector3Int(0, -1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = new Vector3Int(1, 0, 0);
        }


        if (moveDirection != Vector3Int.zero)
        {
            // Rotate the player to face the move direction
            lastMoveDirection = moveDirection; // Update last move direction
            RotatePlayer(moveDirection);

            // Check if the tile is walkable
            if (IsTileWalkable(currentGridPosition + moveDirection))
            {
                MovePlayer(moveDirection);
                LogTileUnderPlayer();  // Log tile only when player moves
            }

            // Check the tile in front of the player for interaction
            LogTileInFrontOfPlayer();
        }
    }

    void MovePlayer(Vector3Int moveDirection)
    {
        // Calculate the new grid position
        Vector3Int newGridPosition = currentGridPosition + moveDirection;

        // Update the player's position
        currentGridPosition = newGridPosition;
        transform.position = tilemapGround.GetCellCenterWorld(currentGridPosition);
    }

    void RotatePlayer(Vector3Int direction)
    {
        if (direction != Vector3Int.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void LogTileUnderPlayer()
    {
        TileBase tileGround = tilemapGround.GetTile(currentGridPosition);
        TileBase tileInteract = tilemapInteract.GetTile(currentGridPosition);

        if (tileGround != null)
        {
            Debug.Log("Tile below player (Ground): " + tileGround.name);
        }
        else
        {
            Debug.Log("No ground tile below player");
        }

        if (tileInteract != null)
        {
            Debug.Log("Tile below player (Interact): " + tileInteract.name);
        }
        else
        {
            Debug.Log("No interact tile below player");
        }
    }

    void LogTileInFrontOfPlayer()
    {
        Vector3Int gridPositionInFront = currentGridPosition + lastMoveDirection;
        TileBase tileInteractInFront = tilemapInteract.GetTile(gridPositionInFront);

        if (tileInteractInFront != null)
        {
            Debug.Log("Tile in front of player (Interact): " + tileInteractInFront.name);
        }
        else
        {
            Debug.Log("No interact tile in front of player");
        }
    }

    void InitializeWalkableTiles()
    {
        // Initialize walkable tile information
        walkableTiles = new Dictionary<string, bool>();

        // Add walkable tiles to the dictionary (true for walkable, false for non-walkable)
        walkableTiles["grass"] = true;
        walkableTiles["dirt"] = true;
        walkableTiles["stone"] = true; // Example of a non-walkable tile
        walkableTiles["Rock"] = false;
        walkableTiles["highGrass"] = true;
		walkableTiles["Log"] = false;
		walkableTiles["Bush"] = false;
		walkableTiles["Tree"] = false;
        // Add more tiles as needed
    }

    bool IsTileWalkable(Vector3Int gridPosition)
    {
        TileBase tileGround = tilemapGround.GetTile(gridPosition);
        TileBase tileInteract = tilemapInteract.GetTile(gridPosition);

        // Check ground tile
        bool isWalkableGround = tileGround != null && walkableTiles.ContainsKey(tileGround.name) ? walkableTiles[tileGround.name] : true;

        // Check interact tile
        bool isWalkableInteract = tileInteract != null && walkableTiles.ContainsKey(tileInteract.name) ? walkableTiles[tileInteract.name] : true;

        // Both must be walkable
        return isWalkableGround && isWalkableInteract;
    }

    public Vector3Int GetCurrentGridPosition()
    {
        return currentGridPosition;
    }

	
}
