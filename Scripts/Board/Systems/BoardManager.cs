using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoardManager 
{
    MonoBoard monoBoard;
    ColorChoiceUtility colorChoiceUtility;
    BoardShuffleUtility boardShuffleUtility;
    BoardSaveUtility boardSaveUtility;
    BoardLoadUtility boardLoadUtility;
    Board.FigureCollection visibleFigureCollection;
    Board.FigureCollection hiddenFigureCollection;
    FigureCreationManager figureCreationManager;
    FigurePlacementManager figurePlacementManager;
    HashSet<IFigure> visibleFigures;
    HashSet<IFigure> hiddenFigures;
    private readonly FormFinderUtility formFinderUtility;
    ColumnFillerUtility columnFillerUtility;
    public System.Action OnSwitchedToEditMode;
    LevelCollectionConfig levelCollectionConfig;
    [Inject]
    public BoardManager(MonoBoard monoBoard,
        ColorChoiceUtility colorChoiceUtility,
        BoardShuffleUtility boardShuffleUtility,
        FigurePlacementManager figurePlacementManager,
        FigureCreationManager figureCreationManager,
        LevelCollectionConfig levelCollectionConfig)
    {
        this.monoBoard = monoBoard;
        this.colorChoiceUtility = colorChoiceUtility;
        this.boardShuffleUtility = boardShuffleUtility;
        this.figurePlacementManager = figurePlacementManager;
        this.figureCreationManager = figureCreationManager;
        this.levelCollectionConfig = levelCollectionConfig;
        formFinderUtility = new FormFinderUtility();
        columnFillerUtility = new ColumnFillerUtility(figureCreationManager, figurePlacementManager);
        boardSaveUtility = new BoardSaveUtility(monoBoard);
        boardLoadUtility = new BoardLoadUtility(figureCreationManager);
        
    }
    public void CreateField()
    {
        monoBoard.CreateTiles();
    }
    public bool TryGetTile(Vector2Int gridPosition,out Tile tile)
    {
        if (monoBoard.TryGetTile(gridPosition, out tile))
        {
            return true;
        }
        else return false;
    }
    public void PlaceRandomFigures()
    {
        OnSwitchedToEditMode?.Invoke();
        PlaceVisibleFigures();
        PlaceHiddenFigures();
    }
    public void PlaceLevelFigures(int level)
    {
        if (levelCollectionConfig.levelConfigs.Length <= level ||
            levelCollectionConfig.levelConfigs[level] == null||
            levelCollectionConfig.levelConfigs[level].boardData==null)
        {
            Debug.LogWarning("No valid level data present, falling back to random placement");
            PlaceRandomFigures();
            return;
        }
        var figureCollectionsPair = boardLoadUtility.LoadField(levelCollectionConfig.levelConfigs[level].boardData) ;
        visibleFigures = figureCollectionsPair.Item1;
        hiddenFigures = figureCollectionsPair.Item2;
        PlaceFigureCollectionsPair();
    }
    void PlaceFigureCollectionsPair()
    {
        figurePlacementManager.PlaceFigures(visibleFigures, monoBoard.GetVisibleTilesGrid());
        figurePlacementManager.PlaceFigures(hiddenFigures, monoBoard.GetHiddenTilesGrid());
    }
    public bool TryGetFormCollection(out Figure.FormCollection formCollection)
    {

        if(formFinderUtility.TryGetFormCollection(monoBoard.GetVisibleTilesGrid(),out var collection))
        {
            formCollection = collection;
            return true;
        }
        else
        {
            formCollection = null;
            return false;
        }
    }
    void PlaceVisibleFigures()
    {
        var visibleTiles = monoBoard.GetVisibleTilesGrid();
        int tilesCount = visibleTiles.Length;
        if (visibleFigureCollection == null)
        {
            visibleFigureCollection = colorChoiceUtility.GenerateColorCounts(tilesCount); 
        }
        var figureDatas = boardShuffleUtility.PlaceVisibleFigures(visibleTiles.GetLength(0),
            visibleTiles.GetLength(1), visibleFigureCollection);
        visibleFigures = figureCreationManager.CreateFigures(figureDatas);
        figurePlacementManager.PlaceFigures(visibleFigures, visibleTiles);
    }
    void PlaceHiddenFigures()
    {
        var hiddenTiles = monoBoard.GetHiddenTilesGrid();
        int tilesCount = hiddenTiles.Length;
        if (hiddenFigureCollection == null)
        {
            hiddenFigureCollection = colorChoiceUtility.GenerateColorCounts(tilesCount); 
        }
        var figureDatas = boardShuffleUtility.PlaceHiddenFigures(hiddenTiles.GetLength(0),
            hiddenTiles.GetLength(1), hiddenFigureCollection);
        hiddenFigures = figureCreationManager.CreateFigures(figureDatas);
        figurePlacementManager.PlaceFigures(hiddenFigures, hiddenTiles);
    }
    public void ShuffleFigures()
    {
        DeleteAllVisibleMonoFigures();
        PlaceVisibleFigures();
    }
    public void ReplaceFigures()
    {
        ClearFields();
        PlaceRandomFigures();
    }
    void DeleteAllVisibleMonoFigures()
    {
        foreach(var figure in visibleFigures)
        {
            figure.OnDespawned();
        }
        visibleFigures.Clear();
    }
   void ClearFields()
    {
        foreach (var figure in visibleFigures)
        {
            figure.OnDespawned();
        }
        visibleFigures.Clear();
        foreach (var figure in hiddenFigures)
        {
            figure.OnDespawned();
        }
        hiddenFigures.Clear();
    }
    public void FillColumns(HashSet<int> columns)
    {
        columnFillerUtility.FillColumns(columns, monoBoard.GetVisibleTilesGrid(), monoBoard.GetHiddenTilesGrid());
    }
    public void SaveBoardToNewDataObject()
    {
        boardSaveUtility.SaveFieldToNewAsset();
    }
    public void SaveBoardToDataObject(LevelBoardData levelBoardData)
    {
        boardSaveUtility.SaveFieldToAsset(levelBoardData);
    }
    public void LoadBoardFromDataObject(LevelBoardData levelBoardData)
    {
        ClearFields();
        var figureCollectionsPair = boardLoadUtility.LoadField(levelBoardData);
        visibleFigures = figureCollectionsPair.Item1;
        hiddenFigures = figureCollectionsPair.Item2;
        PlaceFigureCollectionsPair();
    }
}
