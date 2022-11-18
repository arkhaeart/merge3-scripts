using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Figure;

public class PotentialFormFinderUtility
{
    public bool TryGetPotentialFormCollection(Tile[,] grid, out Figure.PotentialFormCollection potentialFormCollection)
    {
        Dictionary<Vector2Int, HashSet<Vector2Int>> figureMovesDict = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = grid[x, y];
                //if(formFigures.Contains(tile.currentFigure))
                //{
                //    continue;
                //}
                int x1 = x + 1;
                int y1 = y + 1;

                int color = tile.Color;
                Dictionary<Vector2Int, HashSet<Vector2Int>> approvedMovesDict = new Dictionary<Vector2Int, HashSet<Vector2Int>>();

                Dictionary<Vector2Int, HashSet<Vector2Int>> currentMovesDict = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
                int currentFigures = 1;
                while (x1 < width)
                {
                    var newFigure = grid[x1, y];
                    if (newFigure.Color == color)
                    {
                        currentFigures++;
                    }
                    else if (currentFigures >= 3)
                    {


                        ConcatDictionaries(approvedMovesDict, currentMovesDict);
                        currentMovesDict.Clear();
                        if(x1+1<width)
                        {
                            color = grid[x1 + 1, y].Color;
                        }
                    }
                    else
                    {
                        if(currentFigures==2)
                        {
                            if(TryGetPossibleMoveForTile( grid,new Vector2Int(x1,y),new Vector2Int(x1+1,y),color, out var move))
                            {
                                AddMoveToDictionary(approvedMovesDict,move);
                            }
                            if(TryGetPossibleMoveForTile(grid,new Vector2Int(x1,y),new Vector2Int(),color,out var move1))
                            {

                            }    
                        }
                        var dict = GetPossibleDiagonalMovesForTile(grid, new Vector2Int(x1, y), new Vector2Int(1, 0), color);
                        if(dict.Count>0)
                        {
                            ConcatDictionaries(currentMovesDict, dict);
                            currentFigures++;
                        }
                    }
                    x1++;
                }
                if (currentFigures>= 3)
                {
                    ConcatDictionaries(approvedMovesDict, currentMovesDict);
                    currentMovesDict.Clear();
                }
                ///y

                currentFigures = 1;
                while (y1 < height)
                {
                    var newFigure = grid[x, y1];
                    if (newFigure.Color == color)
                    {
                        currentFigures++;
                    }
                    else
                    {
                        if (currentFigures >= 3)
                        {

                        }
                        break;
                    }
                    y1++;
                }
                if (currentFigures >= 3)
                {

                }
            }
        }


        if (figureMovesDict.Count > 0)
{
            potentialFormCollection = new Figure.PotentialFormCollection(figureMovesDict);
            return true;
        }
        else
{
            potentialFormCollection = null;
            return false;
        }
    }
    void ConcatDictionaries(Dictionary<Vector2Int, HashSet<Vector2Int>>first, 
        Dictionary<Vector2Int, HashSet<Vector2Int>>second)
    {
        foreach(var pair in second)
        {
            if(first.ContainsKey(pair.Key))
            {
                var set = first[pair.Key];
                foreach(var value in pair.Value)
                {
                    if (!set.Contains(value))
                        set.Add(value);
                }
            }
            else
            {
                first.Add(pair.Key, new HashSet<Vector2Int>(pair.Value));
            }
        }
    }
    void AddMoveToDictionary(Dictionary<Vector2Int,HashSet<Vector2Int>>dict ,(Vector2Int,Vector2Int) move)
    {
        if(dict.ContainsKey(move.Item1))
        {
            dict[move.Item1].Add(move.Item2);
        }
        else
        {
            dict.Add(move.Item1, new HashSet<Vector2Int> { move.Item2 });
        }
    }

    bool TryGetTile(Tile[,] grid,Vector2Int position,out Tile tile)
    {
        if(position.x>=0&&position.y>=0&&position.x<grid.GetLength(0)&&position.y<grid.GetLength(1))
        {
            tile = grid[position.x, position.y];
            return true;
        }
        else
        {
            tile = null;
            return false;
        }
    }
    Dictionary<Vector2Int, HashSet<Vector2Int>> GetPossibleDiagonalMovesForTile(Tile[,] grid, Vector2Int position,Vector2Int direction, int color)
    {
        var dict = new Dictionary<Vector2Int, HashSet<Vector2Int>>();

        return dict;
    }
    bool TryGetPossibleMoveForTile(Tile[,] grid, Vector2Int position, Vector2Int direction,int color, out (Vector2Int, Vector2Int) move)
    {
        throw new System.NotImplementedException();
    }

}