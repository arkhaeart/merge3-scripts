using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Persistence.Systems;
namespace Common.Samples.Persistence
{
    public class NotDefaultPlayerDataInfo : Saveable<NotDefaultPlayerDataInfo>
    {
        public NotDefaultData notDefaultData;
        public NotDefaultPlayerDataInfo()
        {
            notDefaultData = new NotDefaultData();
        }
    }
    [System.Serializable]
    public class NotDefaultData : SaveableData
    {
        public int count;
        public string name;
    }
}