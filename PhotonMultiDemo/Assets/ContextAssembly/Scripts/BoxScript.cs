using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    
    void Start()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void ScaleDouble()
    {
        transform.localScale *= 1.5f;
    }

    public void ScaleInit()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
