using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
#if(UNITY_EDITOR)
using UnityEditor;
#endif
public class DevPanel : MonoBehaviour
{
    public GameObject editModeButton, editModeText;

    private void Start()
    {
#if (!UNITY_EDITOR)
        gameObject.SetActive(false);
#endif
    }
#if (UNITY_EDITOR)

    BoardManager boardManager;
    [Inject]
    public void Initialize(BoardManager boardManager)
    {
        this.boardManager = boardManager;
    }
    public void RespawnFigures()
    {
        boardManager.ReplaceFigures();
    }
    public void ShuffleVisibleFigures()
    {
        boardManager.ShuffleFigures();

    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SaveToNew()
    {
        boardManager.SaveBoardToNewDataObject();
    }
    public void SaveToSelected()
    {
        var objects = Selection.objects;
        if(objects.Length==0)
        {
            Debug.LogWarning("No LevelBoardData objects selected! Please select one");
            return;
        }    
        else if(objects.Length>1)
        {
            Debug.LogWarning("More than one object was selected. Only one random LevelBoardData will be used, if any found");
        }
        foreach(var obj in objects)
        {
            if(obj is LevelBoardData levelBoardData)
            {
                Debug.Log($"{levelBoardData.name} is used to save current board");
                boardManager.SaveBoardToDataObject(levelBoardData);
                return;
            }
        }
        Debug.LogWarning("No LevelBoardData was found in selection!");

    }
    public void LoadSelected()
    {
        var objects = Selection.objects;
        if (objects.Length == 0)
        {
            Debug.LogWarning("No LevelBoardData objects selected! Please select one");
            return;
        }
        else if (objects.Length > 1)
        {
            Debug.LogWarning("More than one object was selected. Only one random LevelBoardData will be used, if any found");
        }
        foreach (var obj in objects)
        {
            if (obj is LevelBoardData levelBoardData)
            {
                Debug.Log($"{levelBoardData.name} is used to load board");
                boardManager.LoadBoardFromDataObject(levelBoardData);
                return;
            }
        }
        Debug.LogWarning("No LevelBoardData was found in selection!");

    }
    public void EnterEditMode()
    {
        editModeButton.SetActive(false);
        editModeText.SetActive(true);
        boardManager.PlaceRandomFigures();
    }
#endif
}
