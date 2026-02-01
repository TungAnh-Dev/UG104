using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using CasualGameArchitecture.Scripts.Utilities.Extension;

public class UserDataManager : Singleton<UserDataManager>
{
    private readonly Dictionary<Type, ILocalData> dataCache    = new();
    private const    string                       PREFS_PREFIX = "USER_DATA_";

    public void LoadAllData()
    {
        Logger.Log("--- START LOADING USER DATA ---");

        this.dataCache.Clear();

        var allDataTypes = ReflectionUtils.GetAllDerivedTypes<ILocalData>();

        foreach (var type in allDataTypes)
            try
            {
                var data = this.LoadInternal(type);
                this.dataCache[type] = data;

                var jsonContent = data.ToJson();
                Logger.LogWithColor($"[Loaded] {type.Name}: {jsonContent}", Color.green);
            }
            catch (Exception e)
            {
                Logger.LogError($"Failed to load {type.Name}: {e.Message}");
            }

        Logger.LogWithColor($"--- FINISH LOADING {this.dataCache.Count} DATA TYPES ---", Color.cyan);
    }

    public T GetData<T>() where T : class
    {
        var type = typeof(T);

        if (this.dataCache.TryGetValue(type, out var cachedData)) return (T)cachedData;

        Logger.LogWarning($"Data {type.Name} not found in cache. Triggering Lazy Load.");
        var data = this.LoadInternal(type);
        this.dataCache[type] = data;

        return (T)data;
    }

    private ILocalData LoadInternal(Type type)
    {
        var key  = PREFS_PREFIX + type.Name;
        var json = PlayerPrefs.GetString(key, string.Empty);

        ILocalData data;

        if (string.IsNullOrEmpty(json))
        {
            data = (ILocalData)Activator.CreateInstance(type);
            data.Init();

            Logger.LogWithColor($"[Created New] {type.Name}", Color.yellow);
        }
        else
        {
            data = (ILocalData)JsonConvert.DeserializeObject(json, type);
            data.OnDataLoaded();
        }

        return data;
    }

    public void Save<T>() where T : class, ILocalData
    {
        if (this.dataCache.TryGetValue(typeof(T), out var data))
            this.SaveInternal(data);
        else
            Logger.LogError($"Cannot save {typeof(T).Name} because it is not loaded yet.");
    }

    public void SaveAll()
    {
        Logger.Log("Saving all data...");
        foreach (var data in this.dataCache.Values) this.SaveInternal(data);
        PlayerPrefs.Save();
        Logger.LogWithColor("Saved All User Data successfully.", Color.green);
    }

    public void ResetAllData()
    {
        Logger.LogWarning("RESETTING ALL USER DATA!");
        this.dataCache.Clear();

        var allDataTypes = ReflectionUtils.GetAllDerivedTypes<ILocalData>();
        foreach (var type in allDataTypes) PlayerPrefs.DeleteKey(PREFS_PREFIX + type.Name);
        PlayerPrefs.Save();

        this.LoadAllData();
    }

    private void SaveInternal(ILocalData data)
    {
        var type = data.GetType();
        var key  = PREFS_PREFIX + type.Name;
        var json = data.ToJson(); //

        PlayerPrefs.SetString(key, json);
    }

#if UNITY_EDITOR
    public Dictionary<Type, ILocalData> GetRawCache() { return this.dataCache; }
#endif
}