#if(UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardSaveUtility 
{
    const string boardSavesPath = "Assets/Resources/Data/BoardGrids/";
    const string dataObjectName = "LevelBoardData";
    MonoBoard monoBoard;

    public BoardSaveUtility(MonoBoard monoBoard)
    {
        this.monoBoard = monoBoard;
    }

    public void SaveFieldToNewAsset()
    {
        var levelBoardData = ScriptableObject.CreateInstance<LevelBoardData>();
        levelBoardData.visibleGrid = GetBoardDataRows(monoBoard.GetVisibleTilesGrid());
        levelBoardData.hiddenGrid = GetBoardDataRows(monoBoard.GetHiddenTilesGrid());
        var names = AssetDatabase.FindAssets(dataObjectName, new string[] { boardSavesPath });
        int count = names.Length;
        string newObjName = boardSavesPath + dataObjectName + (count + 1).ToString()+".asset";
        AssetDatabase.CreateAsset(levelBoardData, newObjName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Selection.activeObject = levelBoardData;
    }
    public void SaveFieldToAsset(LevelBoardData levelBoardData)
    {
        levelBoardData.visibleGrid = GetBoardDataRows(monoBoard.GetVisibleTilesGrid());
        levelBoardData.hiddenGrid = GetBoardDataRows(monoBoard.GetHiddenTilesGrid());
        EditorUtility.SetDirty(levelBoardData);
        Selection.activeObject = levelBoardData;
    }
    LevelBoardData.Row[] GetBoardDataRows(Tile[,] tileGrid)
    {
        LevelBoardData.Row[] rows= new LevelBoardData.Row[tileGrid.GetLength(1)];
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = new LevelBoardData.Row(new int[tileGrid.GetLength(0)]);
        }
        foreach(var tile in tileGrid)
        {
            rows[tile.gridPosition.y].colors[tile.gridPosition.x] = tile.Color;
        }
        return rows;
    }
}
#endif