using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLoadUtility 
{
    FigureCreationManager figureCreationManager;
    
    public BoardLoadUtility(FigureCreationManager figureCreationManager)
    {
        this.figureCreationManager = figureCreationManager;

    }
    public (HashSet<IFigure>,HashSet<IFigure>) LoadField(LevelBoardData boardData)
    {
        var visibleDatas = ParseSavedData(boardData.visibleGrid);
        var hiddenDatas = ParseSavedData(boardData.hiddenGrid);
        var visibleFigures = figureCreationManager.CreateFigures(visibleDatas);
        var hiddenFigures = figureCreationManager.CreateFigures(hiddenDatas);
        return (visibleFigures, hiddenFigures);
    }
    Figure.Data[,] ParseSavedData(LevelBoardData.Row[] rows)
    {
        Figure.Data[,] datas = new Figure.Data[rows[0].colors.Length, rows.Length];
        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].colors.Length; x++)
            {
                datas[x, y] = new Figure.Data(rows[y].colors[x], new Vector2Int(x, y));
            }
        }
        return datas;
    }
}
