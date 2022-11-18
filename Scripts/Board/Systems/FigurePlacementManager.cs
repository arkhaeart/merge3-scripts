using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurePlacementManager 
{
    public void PlaceFigures(HashSet<IFigure> monoFigures,Tile[,] grid)
    {
        foreach(var figure in monoFigures)
        {
            Tile tile = grid[figure.Data.position.x, figure.Data.position.y];
            if(tile.IsEmpty)
            {
                PlaceFigure(figure, tile);
                
            }
        }
    }

    void MoveFigure(IFigure figure,Vector2 position)
    {
        if (figure is MonoFigure monoFigure)
        {
            //todo lean tween or coroutine
            monoFigure.transform.position = position;
        }
    }
    void MoveFigureInstant(IFigure figure, Vector2 position)
    {
        if (figure is MonoFigure monoFigure)
        {
            monoFigure.transform.position = position;
        }
    }
    public void MoveFigureFromTile(Tile from,Tile to)
    {
        IFigure figure = from.currentFigure;
        from.RemoveFigure();
        PlaceFigure(figure, to);
    }
    public void PlaceFigure(IFigure figure, Tile tile, bool instant=false)
    {
        tile.PlaceFigure(figure);
        if (instant)
        { 
            MoveFigureInstant(figure, tile.transform.position); 
        }
        else
        {
            MoveFigure(figure, tile.transform.position);
        }
    }
    public void SwapFigures(Tile first,Tile second)
    {
        IFigure firstFigure = first.currentFigure;
        IFigure secondFigure = second.currentFigure;
        PlaceFigure(firstFigure, second);
        PlaceFigure(secondFigure, first);

    }
}
