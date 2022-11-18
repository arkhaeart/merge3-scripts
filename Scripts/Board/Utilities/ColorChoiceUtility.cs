using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
public class ColorChoiceUtility
{
    public static int colorsCount;
    [Inject]
    public ColorChoiceUtility()
    {
        colorsCount = System.Enum.GetNames(typeof(Figure.Color)).Length - 1;
    }
    public Board.FigureCollection GenerateColorCounts(int totalCount)
    {
        Dictionary<int, int> dict = new Dictionary<int, int>();
        for (int i = 1; i < colorsCount; i++)
        {
            dict.Add(i, 0);
        }
        while(totalCount>0)
        {
            int color = GetRandomColor();
            dict[color]++;
            totalCount--;
        }
        int maxColor = 0;
        int maxValue = 0;
        foreach(var pair in dict)
        {
            if(pair.Value>maxValue)
            {
                maxColor = pair.Key;
                maxValue = pair.Value;
            }
        }
        return new Board.FigureCollection(dict,maxColor);
    }
    public int GetRandomColor()
    {
        return Random.Range(1, colorsCount);
    }
}
