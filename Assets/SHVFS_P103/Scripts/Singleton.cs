using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // Private Field of Type T
    private static T instance = null;

    // Public Property of Type T
    // What other components will access
    public static T Instance
    {
        //Public Getter
        get
        {
            if (instance != null) return instance;

            instance = FindObjectOfType<T>();

            if (instance == null)
            {
                instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }

            DontDestroyOnLoad(instance.gameObject);

            return instance;
        }
    }
    public virtual void Awake()
    {
        if (instance != null) Destroy(gameObject);
    }
}
