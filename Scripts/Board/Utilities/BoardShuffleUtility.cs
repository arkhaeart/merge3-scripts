using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BoardShuffleUtility 
{
    public FigureKeyPresetConfig keyPresetConfig;
    int width, height;
    [Inject]
    public BoardShuffleUtility(FigureKeyPresetConfig keyPresetConfig)
    {
        this.keyPresetConfig = keyPresetConfig;
        
    }
    public Figure.Data[,] PlaceVisibleFigures(int width,int height,Board.FigureCollection figureCollection)
    {
        this.width = width;
        this.height = height;
        Figure.Data[,] figureGrid = new Figure.Data[width, height];
        var keyBounds=PlaceKeyAtRandomPosition(figureGrid, GetRandomKey(), figureCollection);
        Debug.Log($"Key bounds: {keyBounds.Item1} {keyBounds.Item2}");
        PlaceFirstLines(figureGrid, keyBounds, figureCollection);
        PlaceBelowKey(keyBounds.Item1.y - 1, figureGrid, figureCollection);
        PlaceAboveKey(keyBounds.Item2.y + 1, figureGrid, figureCollection);
        //TestPlaceAllEmpty(figureGrid, figureCollection);
        this.width = 0;
        this.height = 0;
        return figureGrid;
    }
    public Figure.Data[,] PlaceHiddenFigures(int width, int height, Board.FigureCollection figureCollection)
    {
        this.width = width;
        this.height = height;
        Figure.Data[,] figureGrid = new Figure.Data[width, height];
        TestPlaceAllEmpty(figureGrid,figureCollection);
        this.width = 0;
        this.height = 0;
        return figureGrid;
    }
    int[,] GetRandomKey()
    {
        var keyPreset = keyPresetConfig.presetCollection.figureKeyPresets
            [Random.Range(0, keyPresetConfig.presetCollection.figureKeyPresets.Length)];
        // TODO key rotation
        return keyPreset.key;
    }
    (Vector2Int,Vector2Int) PlaceKeyAtRandomPosition(Figure.Data[,] grid, int[,] key,Board.FigureCollection figureCollection )
    {
        int keyWidth = key.GetLength(0);
        int keyHeight = key.GetLength(1);
        int x = Random.Range(0, width - keyWidth);
        int y = Random.Range(0, height - keyHeight);
        int keyColor = figureCollection.colorWithMaxFigures;
        for (int kx = 0; kx < keyWidth; kx++)
        {
            for (int ky = 0; ky < keyHeight; ky++)
            {
                int color = key[kx, ky] == 1 ? keyColor : GetRandomAvailableColor(figureCollection,keyColor);
                if (TakeColor(color, figureCollection))
                {
                    grid[x + kx, y + ky] = new Figure.Data(color, new Vector2Int(x + kx, y + ky));
                }
                else
                {
                    Debug.LogError($"Failed to take color {color}");
                }
                
            }     
        }
        return (new Vector2Int(x, y), new Vector2Int(x + keyWidth-1, y + keyHeight-1));

    }
    bool TakeColor(int color, Board.FigureCollection figureCollection)
    {
        if (figureCollection.colorCounts[color] > 0)
        {
            figureCollection.colorCounts[color]--;
            //if (figureCollection.colorCounts[color] <= 0) TODO dynamic colors trade if depleted or weighted random
            //{                                                             
            //    figureCollection.colorCounts.Remove(color);
            //}
            return true;
        }
        else
        {
            Debug.Log($"Took depleted color {(Figure.Color)color}, current debt:{figureCollection.colorCounts[color]}");
            figureCollection.colorCounts[color]--;
            return true;
        }
    }
    int GetRandomAvailableColor( Board.FigureCollection figureCollection,params int[] colorsNotToTake)
    {
        List<int> colors = new List<int>(figureCollection.colorCounts.Keys);
        colors.RemoveAll((n) => colorsNotToTake.Contains(n));
        if(colors.Count==0)
        {
            Debug.Log($"Could not find random available color because of depletion");
        }
        int color = colors[Random.Range(0, colors.Count)];
        return color;
    }
    void PlaceFirstLines(Figure.Data[,] grid,(Vector2Int,Vector2Int) keyBounds,Board.FigureCollection figureCollection)
    {
        var lowerBound = keyBounds.Item1;
        var upperBound = keyBounds.Item2;
        for (int y = lowerBound.y; y <= upperBound.y; y++)
        {
            for (int xl = lowerBound.x-1; xl >= 0; xl--)
            {
                var evadedColors = GetEvadedColorsForUpLeftWalk(grid, new Vector2Int(xl, y));
                int color = GetRandomAvailableColor(figureCollection, evadedColors);
                if(TakeColor(color,figureCollection))
                {
                    grid[xl, y] = new Figure.Data(color, new Vector2Int(xl, y));
                }

            }
            for (int xr = upperBound.x+1; xr < width; xr++)
            {
                var evadedColors = GetEvadedColorsForUpRightWalk(grid, new Vector2Int(xr, y));
                int color = GetRandomAvailableColor(figureCollection, evadedColors);
                if (TakeColor(color, figureCollection))
                {
                    grid[xr, y] = new Figure.Data(color, new Vector2Int(xr, y));
                }

            }
        }
    }
    void PlaceBelowKey(int yStart, Figure.Data[,] grid, Board.FigureCollection figureCollection)
    {
        if(yStart<0)
        {
            return;
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = yStart; y >= 0; y--)
            {
                Vector2Int pos = new Vector2Int(x, y);
                var colorsToEvade = GetEvadedColorsForDownRightWalk(grid, pos);
                int color = GetRandomAvailableColor(figureCollection, colorsToEvade);
                if(TakeColor(color,figureCollection))
                    grid[x, y] = new Figure.Data(color, pos);
            }
        }
    }
    void PlaceAboveKey(int yStart,Figure.Data[,]grid,Board.FigureCollection figureCollection)
    {
        
        if (yStart >= height)
        {
            return;
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = yStart; y <height; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                var colorsToEvade = GetEvadedColorsForUpRightWalk(grid, pos);
                int color = GetRandomAvailableColor(figureCollection, colorsToEvade);
                if(TakeColor(color,figureCollection))
                    grid[x, y] = new Figure.Data(color, pos);
            }
        }
    }
    int[] GetEvadedColorsForDownRightWalk(Figure.Data[,]grid, Vector2Int pos)
    {
        int[] colors = new int[2];
        Vector2Int upperFigure = pos + new Vector2Int(0, 1);
        if(IsInsideGridAndExists(upperFigure, grid))
        {
            int upperColor = grid[upperFigure.x, upperFigure.y].color;
            upperFigure += new Vector2Int(0, 1);
            if (IsInsideGridAndExists(upperFigure, grid))
            {
                if(upperColor==grid[upperFigure.x,upperFigure.y].color)
                {
                    colors[0] = upperColor;
                }
            }
        }
        Vector2Int leftFigure = pos + new Vector2Int(-1, 0);
        if (IsInsideGridAndExists(leftFigure, grid))
        {
            int leftColor = grid[leftFigure.x, leftFigure.y].color;
            leftFigure += new Vector2Int(-1, 0);
            if (IsInsideGridAndExists(leftFigure, grid))
            {
                if (leftColor == grid[leftFigure.x, leftFigure.y].color)
                {
                    colors[1] = leftColor;
                }
            }
        }
        return colors;
    }
    int[] GetEvadedColorsForUpRightWalk(Figure.Data[,] grid, Vector2Int pos)
    {
        int[] colors = new int[2];
        Vector2Int lowerFigure = pos + new Vector2Int(0, -1);
        if (IsInsideGridAndExists(lowerFigure,grid))
        {
            int upperColor = grid[lowerFigure.x, lowerFigure.y].color;
            lowerFigure += new Vector2Int(0, -1);
            if (IsInsideGridAndExists(lowerFigure, grid))
            {
                if (upperColor == grid[lowerFigure.x, lowerFigure.y].color)
                {
                    colors[0] = upperColor;
                }
            }
        }
        Vector2Int leftFigure = pos + new Vector2Int(-1, 0);
        if (IsInsideGridAndExists(leftFigure, grid))
        {
            int leftColor = grid[leftFigure.x, leftFigure.y].color;
            leftFigure += new Vector2Int(-1, 0);
            if (IsInsideGridAndExists(leftFigure, grid))
            {
                if (leftColor == grid[leftFigure.x, leftFigure.y].color)
                {
                    colors[1] = leftColor;
                }
            }

        }
        return colors;
    }
    int[] GetEvadedColorsForUpLeftWalk(Figure.Data[,] grid, Vector2Int pos)
    {
        int[] colors = new int[2];
        Vector2Int lowerFigure = pos + new Vector2Int(0, -1);
        if (IsInsideGridAndExists(lowerFigure, grid))
        {
            int upperColor = grid[lowerFigure.x, lowerFigure.y].color;
            lowerFigure += new Vector2Int(0, -1);
            if (IsInsideGridAndExists(lowerFigure, grid))
            {
                if (upperColor == grid[lowerFigure.x, lowerFigure.y].color)
                {
                    colors[0] = upperColor;
                }
            }
        }
        Vector2Int rightFigure = pos + new Vector2Int(1, 0);
        if (IsInsideGridAndExists(rightFigure, grid))
        {
            int rightColor = grid[rightFigure.x, rightFigure.y].color;
            rightFigure += new Vector2Int(1, 0);
            if (IsInsideGridAndExists(rightFigure, grid))
            {
                if (rightColor == grid[rightFigure.x, rightFigure.y].color)
                {
                    colors[1] = rightColor;
                }
            }

        }
        return colors;
    }
    bool IsInsideGridAndExists(Vector2Int pos, Figure.Data[,] grid)
    {
        return (pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height)&&grid[pos.x,pos.y]!=null;
    }

    void TestPlaceAllEmpty(Figure.Data[,]grid,Board.FigureCollection figureCollection)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(grid[x,y]==null)
                {
                    int color = GetRandomAvailableColor(figureCollection);
                    if (TakeColor(color, figureCollection))
                    {
                        grid[x, y] = new Figure.Data(color, new Vector2Int(x, y));
                    }
                }
            }
        }
    }
}
