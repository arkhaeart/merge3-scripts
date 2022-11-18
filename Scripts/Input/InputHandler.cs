using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputHandler 
{
    Tile currentTile;
    MonoFigure currentDraggedFigure;
    BoardActionsManager boardActionsManager;
    InputManager inputManager;
    [Inject]
    public InputHandler(BoardActionsManager boardActionsManager, InputManager inputManager)
    {
        this.boardActionsManager = boardActionsManager;
        this.inputManager = inputManager;
        SetCallbacks();
    }
    void SetCallbacks()
    {
        inputManager.OnClick = OnClick;
        inputManager.OnDragStarted = OnDragStart;
        inputManager.OnDragEnded = OnDragEnd;
        inputManager.OnDragResult = OnDragResult;
        inputManager.OnDragContinues = OnDrag;
    }
    void OnClick(Tile tile)
    {
        if (tile.IsEmpty)
            return;
        else
        {
            if(currentTile==null)
                currentTile = tile;
            else if(currentTile!=tile)
            {
                if(currentTile.IsAdjacentTo(tile))
                {
                    boardActionsManager.TrySwapFigures(currentTile, tile);
                    currentTile = null;
                }
            }
        }
    }
    
    void OnDragStart(Tile tile)
    {
        if(!tile.IsEmpty)
        {
            currentTile = tile;
            currentDraggedFigure = tile.currentFigure as MonoFigure;
            currentDraggedFigure.StartDraggedMovement();
        }
    }
    void OnDragEnd(Tile tile)
    {
        currentTile = null;
        if (currentDraggedFigure != null)
        {
            currentDraggedFigure.CancelDraggedMovement(); 
        }
        currentDraggedFigure = null;
    }
    void OnDragResult(Vector2Int direction)
    {
        if(currentTile!=null)
        {
            currentDraggedFigure.CancelDraggedMovement();
            boardActionsManager.TrySwapFigures(currentTile, direction);
            currentTile = null;
            currentDraggedFigure = null;
        }
    }
    void OnDrag(Vector2 positionDelta)
    {
        if(currentDraggedFigure!=null)
        {
            positionDelta = positionDelta.StripLowerAxis();
            currentDraggedFigure.OnDraggedMovement(positionDelta);
        }
    }
}
