using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Configs/Colors")]
public class ColorConfig : ScriptableObject
{
    public Pair[] pairs;
    public Dictionary<int, Color> colorDict;
    public void Initialize()
    {
        colorDict = new Dictionary<int, Color>();
        foreach (var pair in pairs)
        {
            colorDict.TryAdd((int)pair.figureColor, pair.color);
        }
    }
    [System.Serializable]
    public class Pair
    {
        public Figure.Color figureColor;
        public Color color;
    }
}
