using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Data/LevelBoard")]
public class LevelBoardData : ScriptableObject
{
    public Row[] visibleGrid;
    public Row[] hiddenGrid;
    [System.Serializable]
    public class Row
    {
        public int[] colors;

        public Row(int[] colors)
        {
            this.colors = colors;
        }
    }
}
