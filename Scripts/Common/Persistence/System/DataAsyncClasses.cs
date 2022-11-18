using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;
using System;
using UnityEngine.Networking;
using System.Threading;

namespace Persistence.Systems
{
    public static partial class DataHandler
    {
        class AsyncReader
        {
            public static async Task<T> ReadAsync<T>()
            {
                
                if (info.dataPaths.ContainsKey(typeof(T)))
                {
                    using (StreamReader sr = new StreamReader(Path.Combine(persistentPath, info.dataPaths[typeof(T)])))
                    {
                        string json = await sr.ReadToEndAsync();
                        return JsonUtility.FromJson<T>(json);
                    }
                }
                else return default;
            }
            //public static IEnumerator ReadFromSampleAsync(string path,System.Action<string> callback)
            //{
            //    var req = UnityWebRequest.Get(Path.Combine(constPath, path));
            //    Debug.Log("Request send");
            //    yield return req.SendWebRequest();


            //    callback?.Invoke( req.downloadHandler.text);
            //}
        }
        class AsyncWriter
        {
            public static async Task WriteAsync(DataRequest request)
            {
                string json = JsonUtility.ToJson(request.data);
                using (StreamWriter sr = new StreamWriter(Path.Combine(persistentPath, info.dataPaths[request.objectType]), false))
                {
                    await sr.WriteAsync(json);
                }

            }
        }

    }
}
