using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonUtility 
{
    public static Dictionary<string,T> GetNamedScriptableAssets<T>(string path) where T:ScriptableObject
    {
        var assets = Resources.LoadAll<T>(path);
        var dict = new Dictionary<string, T>();
        foreach (var asset in assets)
        {
            if (!dict.ContainsKey(asset.name))
            {
                dict.Add(asset.name, asset);
            }
            else
            {
                Debug.LogError($"Database contains items with duplicate names {asset.name}");
            }
        }
        return dict;
    }
    public static void MoveSkinnedMeshRenderer(Transform[] bones, SkinnedMeshRenderer to)
    {

        Transform[] newBones = to.rootBone.GetComponentsInChildren<Transform>();
        for (int i = 0; i < bones.Length; i++)
        {
            for (int a = 0; a < newBones.Length; a++)
            {
                if (bones[i].name == newBones[a].name)
                {
                    bones[i] = newBones[a];
                    break;
                }
            }
        }
        to.bones = bones;

    }
}
