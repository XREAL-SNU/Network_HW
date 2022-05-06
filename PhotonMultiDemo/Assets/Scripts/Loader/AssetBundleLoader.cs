using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public static class AssetBundleLoader
{
    public static T LoadBundleAsset<T>(string bundleName, string assetName) where T : Object
    {
        AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName));
        if(assetBundle is null)
        {
            Debug.LogError("failed to load assetbundle from file");
            return null;
        }

        T asset = assetBundle.LoadAsset<T>(assetName);
        assetBundle.Unload(false);

        return asset;
    }

}
