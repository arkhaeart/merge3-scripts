using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Boss 
{
    [System.Serializable]
    public class Data
    {
        public Sprite sprite;
        public int maxHP;

    }
    [System.Serializable]
    public class Collection
    {
        public Data[] bosses;
    }
}
