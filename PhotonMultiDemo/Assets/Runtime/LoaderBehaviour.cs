using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class LoaderBehaviour : MonoBehaviour
{
	void OnAssemblyLoaded(WWWAssembly loadedAssembly)
	{
		Debug.Log("LoaderBehaviour: " + "Assembly " + loadedAssembly.URL + "\n");

		System.Type type = loadedAssembly.Assembly.GetType("XTOWN.SampleLibrary.Class1");
		Debug.Log("type " + type.AssemblyQualifiedName);
		// method info
		MethodInfo method = type.GetMethod("CallPublic");
		Debug.Log($"has method {method.GetParameters().ToString()} ");


		// component
		gameObject.AddComponent(type);

		
		
	}

	void OnAssemblyLoadFailed(string url)
	{
		Debug.Log("Failed to load assembly at " + url);
	}
}
