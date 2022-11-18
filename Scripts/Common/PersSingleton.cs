using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersSingleton<T> : MonoSingleton<T> where T: PersSingleton<T>
{
    protected override void Awake()
    {
        base.Awake();
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }
}
