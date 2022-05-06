using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

// let's define all extensions here.
public static class CommonExtensions
{
    public static void BindEvent(this GameObject go, Action<PointerEventData> action, 
        XReal.XTown.UI.UIEvents.UIEvent eventType = XReal.XTown.UI.UIEvents.UIEvent.Click)
    {
        XReal.XTown.UI.UIBase.BindEvent(go, action, eventType);
    }
    
    public static T PasteComponent<T>(this Transform destination, T original) where T:Component
    {
        System.Type type = original.GetType();
        Component copy = destination.gameObject.AddComponent(type);
        // reflections are super powerful
        System.Reflection.FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }

    public static void PasteComponent(this Component comp, GameObject target)
    {
        System.Type type = comp.GetType();
        Component copy = target.gameObject.AddComponent(type);
        // reflections are super powerful
        //  BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy
        System.Reflection.FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(comp));
        }
    }

}
