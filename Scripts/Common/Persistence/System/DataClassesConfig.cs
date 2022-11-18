using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Persistence.Systems
{
    [CreateAssetMenu(menuName = "Configs/DataClasses",fileName ="DataClassesConfig")]
    public class DataClassesConfig : ScriptableObject, IDropDownFiller
    {
        const string folderPath1 = "Assets/";
        const string folderPath2 = "Resources";
        public string namespaceName = "Persistence";
        public List<DataPair> dataPairs = new List<DataPair>();
        public string saveFilesExtension = ".sav";
        const string sampleFilesExtension = ".asset";
        public string sampleFilesFolder = "Samples";
        public string[] GetEntries()
        {
            System.Type[] types = NamespaceClassesUtility.GetTypes(namespaceName);
            string[] entries = new string[types.Length];
            for (int i = 0; i < types.Length; i++)
            {

                entries[i] = types[i].ToString();
            }
            return entries;
        }
#if(UNITY_EDITOR)
        public void CreateSampleFiles()
        {
            if (UnityEditor.AssetDatabase.IsValidFolder(folderPath1 + folderPath2 + "/"+sampleFilesFolder))
            {
                UnityEditor.AssetDatabase.DeleteAsset(folderPath1 + folderPath2 + "/" + sampleFilesFolder);
            }
            var newlyguid=UnityEditor.AssetDatabase.CreateFolder(folderPath1+folderPath2, sampleFilesFolder);
            //Debug.Log(newlyguid);
            
            foreach (var dataPair in dataPairs)
            {
                System.Type type = System.Type.GetType(dataPair.classType);
                var emptyObject = System.Activator.CreateInstance(type);
                string sampleString = JsonUtility.ToJson(emptyObject);
                
                UnityEditor.AssetDatabase.CreateAsset(new TextAsset(sampleString),
                    folderPath1 + folderPath2 +"/" +sampleFilesFolder + "/" + dataPair.saveFileName+sampleFilesExtension);
            }
        }
#endif
        [System.Serializable]
        public class DataPair
        {
            [StringDropDown(path = "Configs/DataClassesConfig")]
            public string classType;
            public string saveFileName;
        }
    }
}