using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Singleton: MonoBehaviour
{
    protected static Singleton _instance = null;
    protected void Awake()
    {
        // singleton
        if (_instance == null)
        {
            _instance = this;

        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

}
