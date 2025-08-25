using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hệ thống Save/Load JSON tiện ích cho mọi loại data runtime.
/// File mapping được quản lý trực tiếp trong SaveSystem.
/// </summary>
public static class SaveSystem
{
    private static readonly Dictionary<string, string> fileMap = new()
    {
        { "player", "player_save.json" },
        { "SkillConfig", "SkillConfig.json" }
    };

    public static void Save<T>(string key, T data)
    {
        if (!fileMap.ContainsKey(key))
        {
            Debug.LogError($"[SaveSystem] No file mapped for key '{key}'");
            return;
        }

        try
        {
            string path = Path.Combine(Application.persistentDataPath, fileMap[key]);
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
            Debug.Log($"[SaveSystem] Saved '{key}': {json}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[SaveSystem] Save '{key}' failed: {ex}");
        }
    }

    /// <summary>
    /// Load dữ liệu bất kỳ theo key.
    /// Nếu file chưa tồn tại, tự tạo từ defaultData và lưu.
    /// </summary>
    public static T Load<T>(string key) where T : new()
    {
        if (!fileMap.ContainsKey(key))
        {
            Debug.LogError($"[SaveSystem] No file mapped for key '{key}'");
            return default;
        }

        string path = Path.Combine(Application.persistentDataPath, fileMap[key]);

        try
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                T runtime = JsonUtility.FromJson<T>(json);
                Debug.Log($"[SaveSystem] Loaded '{key}': {json}");
                return runtime;
            }
            else
            {
                Debug.Log($"[SaveSystem] '{key}' not found → creating new save from default data.");
                T runtime = new T();
                Save(key, runtime);
                return runtime;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[SaveSystem] Load '{key}' failed: {ex}");
            T runtime = new T();
            Save(key, runtime);
            return runtime;
        }
    }

    /// <summary>
    /// Thêm hoặc cập nhật mapping key → file
    /// </summary>
    public static void SetFileMapping(string key, string fileName)
    {
        fileMap[key] = fileName;
    }

    /// <summary>
    /// Lấy path thực tế của file theo key
    /// </summary>
    public static string GetFilePath(string key)
    {
        if (fileMap.ContainsKey(key))
        {
            return Path.Combine(Application.persistentDataPath, fileMap[key]);
        }
        return null;
    }

    /// <summary>
    /// Xóa file save theo key
    /// </summary>
    public static void Delete(string key)
    {
        if (!fileMap.ContainsKey(key))
        {
            Debug.LogError($"[SaveSystem] No file mapped for key '{key}'");
            return;
        }

        string path = Path.Combine(Application.persistentDataPath, fileMap[key]);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"[SaveSystem] Deleted save file '{path}' for key '{key}'.");
        }
        else
        {
            Debug.LogWarning($"[SaveSystem] No save file found for key '{key}' to delete.");
        }
    }
}
