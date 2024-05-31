using UnityEngine;
using UnityEngine.Tilemaps;

public class IslandGenerator : MonoBehaviour
{ 
    public Tilemap tilemapGround;
    public Tilemap tilemapInteract;
    public TileBase verydeepWaterTile;
    public TileBase deepWaterTile;
    public TileBase undeepWaterTile;
    public TileBase sandTile;
    public TileBase grassTile;
    public TileBase highGrassTile;
    public TileBase stoneTile;
    public TileBase RockTile;
    public TileBase LogTile;
    public TileBase BushTile;    
    public TileBase TreeTile;
    public TileBase Sand;
    public TileBase stone;

    public int islandWidth = 200;
    public int islandHeight = 200;
    public float scale = 3f;

    public float verydeepWaterThreshold = 0.1f;
    public float deepWaterThreshold = 0.28f;
    public float undeepWaterThreshold = 0.4f;
    public float sandThreshold = 0.45f;
    public float grassThreshold = 0.6f;
    public float highGrassThreshold = 0.75f;
    public float stoneThreshold = 0.95f;

    public int seed = 0;

    void Start()
    {
        if (seed == 0)
        {
            seed = Random.Range(1, 10000);
        }

        GenerateIsland();
        GenerateNature();
    }

    void GenerateIsland()
    {
        float xOffset = seed;
        float yOffset = seed;

        for (int x = -islandWidth / 2; x < islandWidth / 2; x++)
        {
            for (int y = -islandHeight / 2; y < islandHeight / 2; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                float xCoord = (float)x / scale + seed;
                float yCoord = (float)y / scale + seed; 
                float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                if (noiseValue < verydeepWaterThreshold)
                {
                    tilemapGround.SetTile(tilePosition, verydeepWaterTile);
                }
                else if (noiseValue < deepWaterThreshold)
                {
                    tilemapGround.SetTile(tilePosition, deepWaterTile);
                }
                else if (noiseValue < undeepWaterThreshold)
                {
                    tilemapGround.SetTile(tilePosition, undeepWaterTile);
                }
                else if (noiseValue < sandThreshold)
                {
                    tilemapGround.SetTile(tilePosition, sandTile);
                }
                else if (noiseValue < grassThreshold)
                {
                    tilemapGround.SetTile(tilePosition, grassTile);
                }
                else if (noiseValue < highGrassThreshold)
                {
                    tilemapGround.SetTile(tilePosition, highGrassTile);
                }
                else
                {
                    tilemapGround.SetTile(tilePosition, stoneTile);
                }
            }
        }
    }

    void GenerateNature()
    {
        float xOffset = seed;
        float yOffset = seed;

        for (int x = -islandWidth / 2; x < islandWidth / 2; x++)
        {
            for (int y = -islandHeight / 2; y < islandHeight / 2; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemapGround.GetTile(tilePosition);

                if (tile == grassTile || tile == highGrassTile)
                {
                    float xCoord = (float)x / scale + seed;
                    float yCoord = (float)y / scale + seed;
                    float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                    if (Random.Range(0f, 1f) < 0.02f)
                    {
                        tilemapInteract.SetTile(tilePosition, BushTile);
                    }
                    if (Random.Range(0f, 1f) < 0.02f)
                    {
                        tilemapInteract.SetTile(tilePosition, TreeTile);
                    }
                }
                else if (tile == Sand)
                {
                    float xCoord = (float)x / scale + xOffset;
                    float yCoord = (float)y / scale + yOffset;
                    float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                    if (Random.Range(0f, 1f) < 0.01f)
                    {
                        tilemapInteract.SetTile(tilePosition, LogTile);
                    }
                }                
                else if (tile == stone)
                {
                    float xCoord = (float)x / scale + xOffset;
                    float yCoord = (float)y / scale + yOffset;
                    float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                    if (Random.Range(0f, 1f) < 0.1f)
                    {
                        tilemapInteract.SetTile(tilePosition, RockTile);
                    }
                }
            }
        }
    }
}
