using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Data/Figures/GraphicSet")]
public class FigureGraphicSet : ScriptableObject
{
    public Pair[] pairs;
    public Dictionary<int, Sprite> spriteDict;
    public void Initialize()
    {
        spriteDict = new Dictionary<int, Sprite>();
        foreach(var pair in pairs)
        {
            spriteDict.TryAdd((int)pair.color, pair.sprite);
        }
    }
    [System.Serializable]
    public class Pair
    {
        public Figure.Color color;
        public Sprite sprite;
    }

}

