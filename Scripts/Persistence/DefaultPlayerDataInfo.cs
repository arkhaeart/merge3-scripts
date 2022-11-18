using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Persistence.Systems;
namespace Common.Samples.Persistence
{
    public class DefaultPlayerDataInfo : Saveable<DefaultPlayerDataInfo>
    {
        public DefaultData defaultData;
        public DefaultPlayerDataInfo()
        {
            defaultData = new DefaultData();
        }
    }
    [System.Serializable]
    public class DefaultData:SaveableData
    {
        public int count;
        public string name;
    }
}