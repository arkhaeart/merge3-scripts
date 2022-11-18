using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Configs/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public int stepsToDefeat;
    public Boss.Collection testBossCollection;
    public LevelBoardData boardData;
}
