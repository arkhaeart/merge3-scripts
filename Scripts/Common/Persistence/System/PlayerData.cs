using System.Collections.Generic;

namespace Persistence.Systems {
    [System.Serializable]
    public class PlayerData
    {
        Dictionary<System.Type, ISaveable> dataDict = new Dictionary<System.Type, ISaveable>();

        public PlayerData(params ISaveable[] datas)
        {
            foreach(var data in datas)
            {
                dataDict.TryAdd(data.GetType(), data);
            }
        }
        public T GetDataObject<T>()
            where T:ISaveable
        {
            if (dataDict.TryGetValue(typeof(T), out var value))
            {
                return (T)value;
            }
            else return default(T);
        }
        public void Initialize()
        {
            foreach(var data in dataDict.Values)
            {
                if(data is IInitializable initializable)
                {
                    initializable.Initialize();
                }
            }
        }
    }
}