#if (UNITY_EDITOR)
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class MenuMethods 
{
    static string DataPath=>Path.Combine(Application.persistentDataPath, "data", "save");
    [MenuItem("Edit/ClearSaves")]
    public static void ClearSaves()
    {
        string[] files = Directory.GetFiles(DataPath);
        foreach(var file in files)
        {
            File.Delete(file);
        }        
        PlayerPrefs.DeleteAll();
    }
    [MenuItem("Edit/OpenSaveFolder")]
    public static void OpenSaveFolder()
    {
        EditorUtility.RevealInFinder(DataPath);
    }
    [MenuItem("Edit/ClearSavesAndPrefs")]
    public static void ClearSavesAndPrefs()
    {
        ClearSaves();
        PlayerPrefs.DeleteAll();
    }
    [MenuItem("Edit/SelectAll/Buttons")]
    public static void SelectAllButtons()
    {
        var buttons = GameObject.FindObjectsOfType<Button>(true);
        var gameObjects = buttons.Select((obj) => obj.gameObject as Object).ToArray();
        Selection.objects = gameObjects;
    }
    [MenuItem("Edit/SelectAll/Text")]
    public static void SelectAllText()
    {
        var texts = GameObject.FindObjectsOfType<Text>(true);
        var gameObjects = texts.Select((obj) => obj.gameObject as Object).ToArray();
        Selection.objects = gameObjects;
    }
}
#endif