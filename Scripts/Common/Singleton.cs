using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T: Singleton<T>
{
    static T instance;
    public static T Instance
    {
        get
        {
            if(instance ==null)
            {
                
                //Debug.LogError("Not initialised singleton requested!");
                Init(new Singleton<T>()as T);
            }
            return instance;
        }
    }
    public static void Init(T singleton)
    {
        //Debug.Log($"{singleton} is initialising as singleton");
        instance = singleton;
    }
    public Singleton()
    {
        instance = (T)this;
    }
}

