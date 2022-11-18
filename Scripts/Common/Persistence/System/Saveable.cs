using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Persistence.Systems
{
    public abstract class Saveable<T>:ISaveable where T : Saveable<T>
    {
        public SaveableData saveableData;
        public virtual void Save()
        {
            DataHandler.SaveNewDataInFileSync((T)this);
        }
        public virtual void SaveAsync()
        {
            DataHandler.SaveNewDataInFileAsync((T)this);
        }
        public virtual void Append(IAppendable toAppend)
        {
            if(toAppend is DefaultAppend append)
            {
                saveableData = append.saveableData;
                Save();
            }
        }
        public virtual void AppendAsync(IAppendable toAppend)
        {
            if (toAppend is DefaultAppend append)
            {
                saveableData = append.saveableData;
                SaveAsync();
            }
        }

    }
    [System.Serializable]
    public abstract class SaveableData
    {

    }
    public interface IAppendable
    {

    }
    public interface ISaveable
    {
        void Save();
        void SaveAsync();
        void Append(IAppendable toAppend);
        void AppendAsync(IAppendable toAppend);
    }
    public interface IInitializable
    {
        void Initialize();
    }
}
