using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AssemblyLoader : MonoBehaviour
{
	public string AssemblyURL;
	private string m_ErrorString = "";
	private WWW m_WWW;
	private bool m_Complete = true;



	public void Start()
	{
		if (AssemblyURL != "")
		{
			InitAssembly(AssemblyURL);
		}
	}

	public void Update()
	{
		if (!m_Complete)
		{
			if (m_WWW.error != null)
			{
				m_ErrorString = m_WWW.error;
				m_Complete = true;
				SendMessage("OnAssemblyLoadFailed", AssemblyURL);
			}
			else if (m_WWW.isDone)
			{
				Assembly assembly = LoadAssembly();
				m_Complete = true;
				if (assembly != null)
				{
					Debug.Log("Loading assembly from remote path: Done");
					SendMessage("OnAssemblyLoaded", new WWWAssembly(AssemblyURL, assembly));
				}
				else
				{
					Debug.Log("Loading assembly from remote path: Failed");
					SendMessage("OnAssemblyLoadFailed", AssemblyURL);
				}
			}
		}
	}

	public void InitAssembly(string url)
	{
		m_Complete = false;
		m_ErrorString = "";
		AssemblyURL = url;
		m_WWW = new WWW(AssemblyURL);
	}


	// main loader
	private Assembly LoadAssembly()
	{
		try
		{
			return Assembly.Load(m_WWW.bytes);
		}
		catch (System.Exception e)
		{
			m_ErrorString = e.ToString();
			return null;
		}
	}


}


public class WWWAssembly
{
	private string m_URL;
	private Assembly m_Assembly;

	public string URL
	{
		get
		{
			return m_URL;
		}
	}

	public Assembly Assembly
	{
		get
		{
			return m_Assembly;
		}
	}

	public WWWAssembly(string url, Assembly assembly)
	{
		m_URL = url;
		m_Assembly = assembly;
	}
}