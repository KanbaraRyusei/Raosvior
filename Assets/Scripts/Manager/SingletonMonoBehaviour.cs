using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                Type t = typeof(T);
                _instance = FindObjectOfType(t) as T;
                if(_instance == null)
                {
                    Debug.LogError(t + "‚Ínull‚Å‚·");
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            return;
        }
        else if (Instance == this)
        {
            return;
        }
        Destroy(this);
    }
}
