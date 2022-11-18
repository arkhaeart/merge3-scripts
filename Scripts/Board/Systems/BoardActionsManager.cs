using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEngine.ParticleSystem;

public class BoardActionsManager 
{
    private readonly FigurePlacementManager figurePlacementManager;
    private readonly FiguresWithdrawManager figuresWithdrawManager;
    private readonly BoardManager boardManager;
    private readonly StepManager stepManager;
    MyCour <(Tile,Tile)> withdrawing;
    bool editorState = false;
    [Inject]
    public BoardActionsManager(FigurePlacementManager figurePlacementManager,
        FiguresWithdrawManager figuresWithdrawManager,
        BoardManager boardManager, StepManager stepManager,
        CoroutineHelper coroutineHelper)
    {
        this.figurePlacementManager = figurePlacementManager;
        this.figuresWithdrawManager = figuresWithdrawManager;
        this.boardManager = boardManager;
        this.stepManager = stepManager;
        boardManager.OnSwitchedToEditMode = SwitchEditMode;
        withdrawing = new MyCour<(Tile, Tile)>(coroutineHelper, Withdrawing);
    }

    void SwitchEditMode()
    {
        editorState = true;
    }
    public void TrySwapFigures(Tile first,Vector2Int direction)
    {
        if (boardManager.TryGetTile(first.gridPosition + direction, out Tile second))
        { 
            TrySwapFigures(first, second); 
        }
    }
    public void TrySwapFigures(Tile first, Tile second)
    {
        
        figurePlacementManager.SwapFigures(first, second);
        if (!editorState)
        {
            withdrawing.Run((first, second));
        }


    }
    IEnumerator Withdrawing((Tile,Tile)tiles)
    {
        var first = tiles.Item1;
        var second = tiles.Item2;
        int withdrawTurns = 0;
        while (boardManager.TryGetFormCollection(out var formCollection))
        {
            figuresWithdrawManager.WithdrawFormCollection(formCollection, out var withdrawnColumns);
            yield return new WaitForSeconds(0.3f);
            boardManager.FillColumns(withdrawnColumns);
            yield return new WaitForSeconds(0.3f);
            withdrawTurns++;
        }
        if (withdrawTurns == 0)
        {
            OnFailedSwap(first, second);
        }
        else
        {
            stepManager.Step();
        }
    }
    void OnFailedSwap(Tile first, Tile second)
    {
        figurePlacementManager.SwapFigures(first, second);
    }

}
