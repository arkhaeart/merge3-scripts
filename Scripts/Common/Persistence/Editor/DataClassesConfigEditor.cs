using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Persistence.Systems;
[CustomEditor( typeof(DataClassesConfig))]
public class DataClassesConfigEditor:Editor
{
    DataClassesConfig Obj => target as DataClassesConfig;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(GUILayout.Button("Create Samples"))
        {
            Obj.CreateSampleFiles();
        }
    }
}
