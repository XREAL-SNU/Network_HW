using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

internal static class AvatarLoad
{
    internal static async Task InitAssets<T>(string label, List<T> createdObjs, Transform parent)
            where T: Object
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        foreach(var location in locations)
        {
            Debug.Log(location.InternalId);
            createdObjs.Add(await Addressables.InstantiateAsync(location, parent).Task as T);
        }
    }
}
