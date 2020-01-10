using UnityEngine;
using System.Collections.Generic;

public class SingletonTypeAliveChecker
{
    static SingletonTypeAliveChecker _instance;

    public Dictionary<System.Type, GameObject> typeAliveCheckers = new Dictionary<System.Type, GameObject>();

    protected SingletonTypeAliveChecker()
    {

    }

    public static SingletonTypeAliveChecker Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SingletonTypeAliveChecker();
            }
            return _instance;
        }
    }
}

public enum SingletonType
{
    GlobalInstance,
    PerSceneInstance,
}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static object _lock = new object();

    private static bool dontDestroyFindObjOnLoad = false;	// Instance from FindObjectOfType.
    private static bool dontDestroyNewObjOnLoad = true;		// Instance from AddComponent.

    public SingletonType singletonType
    {
        set
        {
            switch (value)
            {
                case SingletonType.GlobalInstance:
                    dontDestroyFindObjOnLoad = true;
                    dontDestroyNewObjOnLoad = true;
                    break;

                case SingletonType.PerSceneInstance:
                    dontDestroyFindObjOnLoad = false;
                    dontDestroyNewObjOnLoad = false;
                    break;
            }
        }
    }

    public virtual void Awake()
    {
        if (FindObjectsOfType(typeof(T)).Length > 1)
        {
            Destroy(this.gameObject);
        }
        if (!SingletonTypeAliveChecker.Instance.typeAliveCheckers.ContainsKey(typeof(T)))
        {
            SingletonTypeAliveChecker.Instance.typeAliveCheckers.Add(typeof(T), gameObject);
        }
    }

    public virtual void OnDestroy()
    {
        if (SingletonTypeAliveChecker.Instance.typeAliveCheckers.ContainsKey(typeof(T)) && SingletonTypeAliveChecker.Instance.typeAliveCheckers[typeof(T)] == gameObject)
        {
            SingletonTypeAliveChecker.Instance.typeAliveCheckers.Remove(typeof(T));
        }
    }

    public static bool IsAlive
    {
        get
        {
            return !applicationIsQuitting && SingletonTypeAliveChecker.Instance.typeAliveCheckers.ContainsKey(typeof(T));
        }
    }

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        if (Debug.isDebugBuild) Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopenning the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        if (dontDestroyNewObjOnLoad)
                        {
                            DontDestroyOnLoad(singleton);
                        }

                    }
                    else
                    {
                        if (dontDestroyFindObjOnLoad)
                        {
                            DontDestroyOnLoad(_instance.transform.root);
                        }
                    }
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;

    public void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }
}
