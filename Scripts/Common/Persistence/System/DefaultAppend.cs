using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Persistence.Systems
{
    public class DefaultAppend : IAppendable
    {
        public SaveableData saveableData;

        public DefaultAppend(SaveableData saveableData)
        {
            this.saveableData = saveableData;
        }
    }
}