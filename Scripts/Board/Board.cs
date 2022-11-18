using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board 
{
    public Tile[,] visibleBoard;
    public Tile[,] hiddenBoard;
    public class FigureCollection
    {
        
        public Dictionary<int, int> colorCounts;
        public int colorWithMaxFigures;

        public FigureCollection(Dictionary<int, int> colorCounts, int colorWithMaxFigures)
        {
            this.colorCounts = colorCounts;
            this.colorWithMaxFigures = colorWithMaxFigures;
        }
    }
}
