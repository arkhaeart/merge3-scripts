using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Configs/FigureKeyPreset")]
public class FigureKeyPresetConfig : ScriptableObject
{
    public string separator = "/";
    public FigureKeyPresetRawCollection presetRawCollection;
    public FigureKeyPresetCollection presetCollection;
    public void Initialize()
    {
        FillPresetCollection();
    }
    void FillPresetCollection()
    {
        FigureKeyPresetCollection collection = new FigureKeyPresetCollection();
        collection.figureKeyPresets = new FigureKeyPreset[presetRawCollection.presetsRaw.Length];
        for (int i = 0; i < presetRawCollection.presetsRaw.Length; i++)
        {
            FigureKeyPresetRaw figurePresetRaw = presetRawCollection.presetsRaw[i];
            var firstLineDigits = figurePresetRaw.rows[0].keys.Split(separator);
            int[,] grid = new int[firstLineDigits.Length,figurePresetRaw.rows.Length];
            for (int y = 0; y < figurePresetRaw.rows.Length; y++)
            {
                var row = figurePresetRaw.rows[y];
                var digits = row.keys.Split(separator);

                for (int x = 0; x < firstLineDigits.Length; x++)
                {
                    int digit = 0;
                    if (int.TryParse(digits[x],out int result))
                    {
                        digit = result;
                    }
                    else
                    {
                        Debug.LogError($"Cannot parse {digits[x]} to int! Please enter 0 or 1");
                    }
                        grid[x, y] = digit;
                }
            }

            FigureKeyPreset keyPreset = new FigureKeyPreset(grid);
            collection.figureKeyPresets[i] = keyPreset;
        }
        presetCollection = collection;
    }
}
[System.Serializable]
public class FigureKeyPresetRawCollection
{
    public FigureKeyPresetRaw[] presetsRaw;
}
[System.Serializable]
public class FigureKeyPresetRaw
{
    public Row[] rows;
    [System.Serializable]
    public class Row
    {
        public string keys;
    }
}
public class FigureKeyPresetCollection
{
    public FigureKeyPreset[] figureKeyPresets;
}
public class FigureKeyPreset
{
    public int[,] key;

    public FigureKeyPreset(int[,] key)
    {
        this.key = key;
    }
}
