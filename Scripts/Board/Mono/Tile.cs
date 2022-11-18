using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public IFigure currentFigure;
    public Vector2Int gridPosition { get; private set; }
    public int Color { get => currentFigure.Data.color; }
    public void SetGridPosition(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;
    }
    public void PlaceFigure(IFigure monoFigure)
    {
        currentFigure = monoFigure;
        currentFigure.Data.position = gridPosition;
        currentFigure.SetDespawnCallback(RemoveFigure);
    }
    public void RemoveFigure()
    {
        
        currentFigure = null;
    }
    public bool IsEmpty => currentFigure == null;
    public bool IsAdjacentTo(Tile second)
    {
        Vector2Int deltaPos = gridPosition - second.gridPosition;
        if (Mathf.Abs(deltaPos.x) > 1 || Mathf.Abs(deltaPos.y) > 1)
        {
            return false;
        }
        else return true;
    }
}
