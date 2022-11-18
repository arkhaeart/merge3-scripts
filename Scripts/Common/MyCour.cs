using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCour
{
    System.Func<IEnumerator> func;
    Coroutine cour, subcour;
    MonoBehaviour mono;
    public MyCour(MonoBehaviour mono, System.Func<IEnumerator> func)
    {
        this.mono = mono;
        this.func = func;
    }
    public void Run()
    {
        Stop();

        cour = mono.StartCoroutine(Process());
    }
    IEnumerator Process()
    {
        subcour = mono.StartCoroutine(func?.Invoke());
        yield return subcour;
        cour = null;
        subcour = null;
    }
    public void Stop()
    {
        if (IsActive)
        {
            mono.StopCoroutine(cour);
            cour = null;
        }
        if (subcour != null)
        {
            mono.StopCoroutine(subcour);
            subcour = null;
        }
    }

    public bool IsActive => cour != null;
}
public class MyCour<T1>
{
    readonly System.Func<T1,IEnumerator> func;
    Coroutine cour, subcour;
    readonly MonoBehaviour mono;

    public MyCour(MonoBehaviour mono, System.Func<T1, IEnumerator> func)
    {
        this.mono = mono;
        this.func = func;
    }
    public void Run(T1 param)
    {
        Stop();

        cour = mono.StartCoroutine(Process(param));
    }
    IEnumerator Process(T1 param)
    {
        subcour = mono.StartCoroutine(func?.Invoke(param));
        yield return subcour;
        cour = null;
        subcour = null;
    }
    public void Stop()
    {
        if (IsActive)
        {
            mono.StopCoroutine(cour);
            cour = null;
        }
        if (subcour != null)
        {
            mono.StopCoroutine(subcour);
            subcour = null;
        }
    }
    public bool IsActive => cour != null;
}