using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBoard : MonoBehaviour
{
    public int width = 10;
    public int height = 8;
    public float hiddenRelativeHeight = 2.5f;
    public Tile tilePrefab;
    public Transform visibleBoard, hiddenBoard;
    public float tileStep;
    Tile[,] visibleTiles;
    Tile[,] hiddenTiles;
    public void CreateTiles()
    {
        visibleTiles = new Tile[width, height];
        int hiddenHeight = Mathf.RoundToInt(height * hiddenRelativeHeight);
        hiddenTiles = new Tile[width, hiddenHeight];
        visibleBoard.DestroyAllChildren(true);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile newTile = Instantiate(tilePrefab,
                    (Vector2)visibleBoard.position + new Vector2(x * tileStep, y * tileStep),
                    Quaternion.identity, visibleBoard);
                visibleTiles[x, y] = newTile;
                newTile.SetGridPosition(new Vector2Int(x, y));
            }
        }
        hiddenBoard.DestroyAllChildren(true);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < hiddenHeight; y++)
            {
                Tile newTile = Instantiate(tilePrefab,
                    (Vector2)hiddenBoard.position + new Vector2(x * tileStep, y * tileStep),
                    Quaternion.identity, hiddenBoard);
                hiddenTiles[x, y] = newTile;
                newTile.SetGridPosition(new Vector2Int(x, y));

            }
        }

    }
    public bool TryGetTile(Vector2Int gridPosition,out Tile tile)
    {
        if (IsWithinGrid(gridPosition))
        {
            tile =visibleTiles[gridPosition.x, gridPosition.y];
            return true;
        }
        else
        {
            tile = null;
            return false;
        }


    }
    bool IsWithinGrid(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height;
    }
    public Tile[,] GetVisibleTilesGrid()
    {
        return visibleTiles;
    }
    public Tile[,] GetHiddenTilesGrid()
    {
        return hiddenTiles;
    }
}
