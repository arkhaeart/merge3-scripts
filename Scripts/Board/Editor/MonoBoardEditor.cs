using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MonoBoard))]
public class MonoBoardEditor : Editor
{
    MonoBoard Obj => target as MonoBoard;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("CreateTiles"))
        {
            Obj.CreateTiles();
        }
    }
}
