using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    public GameObject target;
    public ColorChange.MyColors col;
    public ColorChange colorChange;
    
    private void Start()
    {
        colorChange = target.GetComponent<ColorChange>();
        Debug.Log($"at c#_assembly: color = {colorChange.mycol}");
    }
}
