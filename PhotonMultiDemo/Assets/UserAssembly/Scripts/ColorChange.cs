using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XTOWN.SampleLibrary;

public class ColorChange : MonoBehaviour
{
    
    public Class1 myDllClass;
    // Start is called before the first frame update
    void Start()
    {
        myDllClass = GetComponent<Class1>();
        myDllClass.CallPublic();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            myDllClass.CallPublic();
        }
    }
}
