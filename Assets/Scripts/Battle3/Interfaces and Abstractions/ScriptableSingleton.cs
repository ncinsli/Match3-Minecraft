using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject{
    private static T _instance;   
    public static T instance{
        get{
            if (_instance == null) _instance = Resources.Load<T>(typeof(T).Name);
            return _instance;
        }
    }
}
