using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // Private Field of Type T
    private static T _instance = null;

    // Public Property of Type T
    // What other components will access
    public static T Instance
    {
        //Public Getter
        get
        {
            if (_instance != null) return _instance;

            _instance = FindObjectOfType<T>();

            if (_instance == null)
            {
                _instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }

            // DontDestroyOnLoad(_instance.gameObject);

            return _instance;
        }
    }
    
    public virtual void Awake()
    {
        if (_instance != null) Destroy(gameObject);
    }
}
