using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public enum MyColors
    {
        Pink, Yellow, Violet
    }

    public MyColors mycol = MyColors.Pink;
    public BoxScript boxScript;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("my color:" + mycol);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            boxScript.ScaleDouble();
        }
    }
}
