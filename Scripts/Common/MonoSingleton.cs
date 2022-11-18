using UnityEngine;
using Zenject;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    const bool createInstanceIfNotExist = false;
    static T instance; 

    public static T Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<T>();
                if (instance == null&&createInstanceIfNotExist) {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    instance = singleton.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake() {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance!=this)
        {
            Debug.Log($"There is already another instance of {this} and it is {instance.gameObject} so deleting");

            Destroy(gameObject);
        }
    }
    public class Factory:PlaceholderFactory<T>
    {
    }
}