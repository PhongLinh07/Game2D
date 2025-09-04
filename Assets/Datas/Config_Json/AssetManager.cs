using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
public enum EAsset
{
    CharacterMale = 1,
    SkillConfigSO = 2,
    ItemConfigSO = 3,
    CharacterConfigSO = 4,
    ItemUserConfigSO = 5

}

public class AssetManager
{
    private Dictionary<EAsset, AsyncOperationHandle> loadedAssets = new();

    // Load asset (Prefab, ScriptableObject, v.v.)
    public void LoadAsset<T>(EAsset key, Action<T> onLoaded) where T : class
    {
        if (loadedAssets.ContainsKey(key))
        {
            onLoaded?.Invoke(loadedAssets[key].Result as T);
            return;
        }

        var handle = Addressables.LoadAssetAsync<T>(key.ToString());
        handle.Completed += (AsyncOH) =>
        {
            if (AsyncOH.Status == AsyncOperationStatus.Succeeded)
            {
                loadedAssets[key] = AsyncOH;
                onLoaded?.Invoke(AsyncOH.Result);
            }
        };
    }


    // Release 1 asset
    public void ReleaseAsset(EAsset key)
    {
        if (loadedAssets.TryGetValue(key, out var handle))
        {
            Addressables.Release(handle);
            loadedAssets.Remove(key);

            Debug.Log($"ReleaseAsset: {key.ToString()}");
        }
    }

    // Release tất cả
    public void ReleaseAll()
    {
        foreach (var kvp in loadedAssets)
        {
            Addressables.Release(kvp.Value);

            Debug.Log($"ReleaseAll: {kvp.ToString()}");
        }
        loadedAssets.Clear();
    }
}

