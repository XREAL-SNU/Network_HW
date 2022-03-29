using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyNoteActive : MonoBehaviour
{

    public GameObject Creator;

    public void OnClickButton()
    {
        if(!Creator.activeSelf)
        {
            Creator.SetActive(true);
        }
        else
        {
            Creator.SetActive(false);
        }
    }
}
