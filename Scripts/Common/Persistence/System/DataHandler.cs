using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using System.Threading;
//This class must not contain any game-specific information!
//Before using this class data methods, you must initialize it with Init method,


namespace Persistence.Systems
{
    public static partial class DataHandler
    {
        static string persistentPath;
        static string constPath;
        static DataHandlerInfo info;
        static Queue<DataRequest> asyncRequests = new Queue<DataRequest>();

        public static void Init(DataClassesConfig config)
        {
            var filler = new DataHandlerFiller(config);
            info = filler.GetDataHandlerInfo();


            Debug.Log("Data Handler static initialization running");
            persistentPath = Path.Combine(Application.persistentDataPath, "data", "save");
#if (UNITY_IOS || UNITY_EDITOR)

            constPath = Application.streamingAssetsPath;
#elif (UNITY_ANDROID)
            constPath= "jar:file://" + Application.dataPath + "!/assets/";

#endif

            InitDirectoryAndFiles();
            LaunchAsyncRequestsThread();
            Debug.Log("Initialization finished");


        }



        static void InitDirectoryAndFiles()
        {
            if(!Directory.Exists(persistentPath))
            {
                Directory.CreateDirectory(persistentPath);
            }
            foreach(var pair in info.dataPaths)
            {
               
                string combined = Path.Combine(persistentPath, pair.Value);
                if (!File.Exists(combined))
                {
                    SyncWriter.CreateDefaultDataFile(pair.Key);
                }
            }
        }
        static void LaunchAsyncRequestsThread()
        {
            Debug.Log("Launching thread");
            ThreadStart start =  HandlingAsyncRequests;
            Thread thread = new Thread(start);
            thread.Start();
            Debug.Log("Thread launched");
        }
        public static void SaveNewDataInFileSync<T>(T data)
        {
            Debug.Log($"Saving {data} of type {typeof(T)} synchronously");
            SyncWriter.RewriteDataFile(info.dataPaths[typeof(T)], data);
        }
        public static void SaveNewDataInFileAsync<T>(T data)
        {
            Debug.Log($"Saving {data} of type {typeof(T)} asynchronously");
            asyncRequests.Enqueue(new DataRequest(DataRequest.Type.Write, typeof(T),data));
        }

        
        public async static Task<T> GetDataAsync<T>()
        {
            return await AsyncReader.ReadAsync<T>();
        }
        public static T GetDataSync<T>(T type=default(T))
        {
            return SyncReader.Read<T>();
        }
        public static async void HandlingAsyncRequests()
        {
            Debug.Log("Handling Async Requests is running");
            while(true)
            {
                if(asyncRequests.Count>0)
                {
                    Debug.Log("Detected async request");
                    DataRequest req = asyncRequests.Dequeue();
                    if(req.type== DataRequest.Type.Read)
                    {
                        Debug.Log("It is reading request, which is not made yet");
                    }
                    else
                    {
                        Debug.Log("It is writing request");
                        await AsyncWriter.WriteAsync(req);
                    }
                }
                Thread.Sleep(300);
            }
        }
        public class DataRequest
        {
            public DataRequest(Type type,System.Type objectType)
            {
                this.type = type;
                this.objectType = objectType;
            }
            public DataRequest(Type type, System.Type objectType,object data)
            {
                this.data = data;
                this.type = type;
                this.objectType = objectType;
            }
            public enum Type
            {
                Read,
                Write
            }
            public System.Type objectType;
            public object data;
            //public System.Action callback;
            public Type type;
            public bool finished = false;
            
        }
    }
}