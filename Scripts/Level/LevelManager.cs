using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager 
{
    
    System.Action OnVictory;
    System.Action OnDefeat;
    public event System.Action<int> OnLevelStart;
    BoardManager boardManager;
    BossManager bossManager;
    int currentLevel=0;
    [Inject]
    public LevelManager(BoardManager boardManager, BossManager bossManager)
    {
        this.boardManager = boardManager;
        this.bossManager = bossManager;
        bossManager.SetBossesDefeatedCallback(Victory);
    }
    public void SetCallbacks(System.Action victoryCallback,System.Action defeatCallback)
    {
        OnVictory = victoryCallback;
        OnDefeat = defeatCallback;
    }
    public void StartEditor()
    {
        boardManager.CreateField();
        boardManager.PlaceRandomFigures();
        
    }
    public void StartLevel()
    {
        boardManager.CreateField();
        boardManager.PlaceLevelFigures(currentLevel);
        bossManager.StartLevel(currentLevel);
        OnLevelStart?.Invoke(currentLevel);
    }
    public void Victory()
    {
        OnVictory?.Invoke();
    }
    public void Defeat()
    {
        OnDefeat?.Invoke();
    }
}
