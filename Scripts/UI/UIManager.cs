using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class UIManager : MonoBehaviour
{
    BoardManager boardManager;
    LevelManager levelManager;
    public GameObject victoryScreen, failScreen;
    [Inject]
    public void Initialize(BoardManager boardManager,LevelManager levelManager)
    {
        this.boardManager = boardManager;
        levelManager.SetCallbacks(OnVictory, OnDefeat);
    }
    void OnVictory()
    {
        victoryScreen.SetActive(true);
    }
    void OnDefeat()
    {
        failScreen.SetActive(true);

    }
    public void TestRespawnField()
    {

    }
    public void TestReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void TestRespawnFigures()
    {

    }
}
