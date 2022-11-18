using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnFillerUtility 
{
    FigureCreationManager figureCreationManager;
    FigurePlacementManager figurePlacementManager;

    public ColumnFillerUtility(FigureCreationManager figureCreationManager, FigurePlacementManager figurePlacementManager)
    {
        this.figureCreationManager = figureCreationManager;
        this.figurePlacementManager = figurePlacementManager;
    }
    public void FillColumns(HashSet<int> columns, Tile[,] visible, Tile[,] hidden)
    {
        foreach(var column in columns)
        {
            FillColumn(column, visible, hidden);
        }
    }
    void FillColumn(int x, Tile[,] visible,Tile[,]hidden)
    {
        Queue<Tile> emptyTiles = new Queue<Tile>();
        FillTilesWithMovedFigures(x, visible, emptyTiles);
        FillTilesWithMovedFigures(x, hidden, emptyTiles);
        FillTilesWithNewFigures(emptyTiles);
    }
    private void FillTilesWithNewFigures(Queue<Tile> tiles)
    {
        while(tiles.Count>0)
        {
            var tile = tiles.Dequeue();
            var figure = figureCreationManager.CreateNewRandomFigure(tile.gridPosition);
            figurePlacementManager.PlaceFigure(figure, tile, true);
        }
    }
    private void FillTilesWithMovedFigures(int x, Tile[,] grid, Queue<Tile> emptyTiles)
    {
        int height = grid.GetLength(1);
        for (int y = 0; y < height; y++)
        {
            var tile = grid[x, y];
            if (tile.IsEmpty)
            {
                emptyTiles.Enqueue(tile);
            }
            else if (emptyTiles.Count > 0)
            {
                var newTile = emptyTiles.Dequeue();
                figurePlacementManager.MoveFigureFromTile(tile, newTile);
                emptyTiles.Enqueue(tile);
            }
        }
    }
}
