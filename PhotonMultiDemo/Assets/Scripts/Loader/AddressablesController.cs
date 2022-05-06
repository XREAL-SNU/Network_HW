using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

internal class AddressablesController : Singleton
{
    internal static AddressablesController Instance
    {
        get => _instance as AddressablesController;
    }

	[SerializeField]
	private string _label;
	private Transform _parent;
	public List<GameObject> _createdObjs { get; } = new List<GameObject>();

	private void Start()
	{
		_parent = new GameObject("Addressables").transform;
        Instantiate();

		Debug.Log(" Start, after Instantiate call under thread " + Thread.CurrentThread.ManagedThreadId);
	}

	private async void Instantiate()
	{
		await AvatarLoad.InitAssets(_label, _createdObjs, _parent);
		Debug.Log("exiting instantiate under thread# " + Thread.CurrentThread.ManagedThreadId);
	}
}
