#if UNITY_EDITOR
namespace CasualGameArchitecture.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using UnityEditor;
    using UnityEngine;
    using CasualGameArchitecture.Scripts.Utilities.Extension;

    [Obsolete("Obsolete")]
    public class UserDataEditorWindow : OdinEditorWindow
    {
        private const string PREFS_PREFIX = "USER_DATA_"; 

        [MenuItem("Tools/User Data Manager (Odin)")]
        public static void ShowWindow()
        {
            GetWindow<UserDataEditorWindow>().Show();
        }

        [Title("Data Management Actions", titleAlignment: TitleAlignments.Centered)]
        [HorizontalGroup("Actions", PaddingLeft = 10, PaddingRight = 10)]
        [Button(ButtonSizes.Large), GUIColor(0.5f, 1f, 0.5f)]
        [EnableIf("@UnityEngine.Application.isPlaying")] 
        private void SyncWithRuntime()
        {
            var manager = UserDataManager.Instance;
            var fieldInfo = typeof(UserDataManager).GetField("dataCache", BindingFlags.Instance | BindingFlags.NonPublic);
            
            if (fieldInfo != null)
            {
                var runtimeDict = fieldInfo.GetValue(manager) as Dictionary<Type, ILocalData>;
                if (runtimeDict != null)
                {
                    this.userDataList = runtimeDict.Values.ToList();
                    Debug.Log($"Synced {this.userDataList.Count} items from Runtime.");
                }
            }
            else
            {
                Debug.LogError("Could not find 'dataCache' field in UserDataManager.");
            }
        }

        [HorizontalGroup("Actions")]
        [Button(ButtonSizes.Large)]
        private void LoadFromDisk()
        {
            this.userDataList = new List<ILocalData>();
            var allDataTypes = ReflectionUtils.GetAllDerivedTypes<ILocalData>(); //

            foreach (var type in allDataTypes)
            {
                var key = PREFS_PREFIX + type.Name;
                ILocalData dataInstance;

                if (PlayerPrefs.HasKey(key))
                {
                    var json = PlayerPrefs.GetString(key);
                    try 
                    {
                        dataInstance = (ILocalData)JsonConvert.DeserializeObject(json, type);
                        if (dataInstance != null) dataInstance.OnDataLoaded();
                    }
                    catch
                    {
                        dataInstance = (ILocalData)Activator.CreateInstance(type);
                    }
                }
                else
                {
                    dataInstance = (ILocalData)Activator.CreateInstance(type);
                }

                if (dataInstance != null)
                {
                    this.userDataList.Add(dataInstance);
                }
            }
            Debug.Log("Loaded all data from Disk (PlayerPrefs).");
        }

        [HorizontalGroup("Actions")]
        [Button(ButtonSizes.Large), GUIColor(1f, 0.8f, 0.4f)]
        private void SaveAll()
        {
            if (this.userDataList == null || this.userDataList.Count == 0) return;

            foreach (var data in this.userDataList)
            {
                var key = PREFS_PREFIX + data.GetType().Name;
                var json = data.ToJson(); 
                PlayerPrefs.SetString(key, json);
            }
            PlayerPrefs.Save();
            Debug.Log("Saved all data to Disk.");
        }

        [HorizontalGroup("Actions")]
        [Button(ButtonSizes.Large), GUIColor(1f, 0.5f, 0.5f)]
        private void DeleteAll()
        {
            if (EditorUtility.DisplayDialog("Warning", "Delete ALL PlayerPrefs User Data? This cannot be undone.", "Yes", "Cancel"))
            {
                var allDataTypes = ReflectionUtils.GetAllDerivedTypes<ILocalData>();
                foreach (var type in allDataTypes)
                {
                    PlayerPrefs.DeleteKey(PREFS_PREFIX + type.Name);
                }
                PlayerPrefs.Save();
                this.userDataList.Clear();
                Debug.Log("Deleted all User Data keys.");
            }
        }

        [HorizontalGroup("SubActions")]
        [Button(ButtonSizes.Medium), GUIColor(0.2f, 0.6f, 1f)]
        private void InitDefaultData()
        {
            if (EditorUtility.DisplayDialog("Confirm", "Create default data for all types (Overwrite current view)?", "Yes", "No"))
            {
                this.userDataList = new List<ILocalData>();
                var allDataTypes = ReflectionUtils.GetAllDerivedTypes<ILocalData>();

                foreach (var type in allDataTypes)
                {
                    var instance = (ILocalData)Activator.CreateInstance(type);
                    instance.Init(); 
                    this.userDataList.Add(instance);
                }
                Debug.Log("Initialized Default Data (Not Saved yet). Click 'Save All' to commit.");
            }
        }
        

        [Space(20)]
        [Title("Loaded Data Inspector")]
        [ShowInInspector]
        [ListDrawerSettings(
            Expanded = true, 
            ShowIndexLabels = false, 
            IsReadOnly = true, 
            ShowItemCount = true
        )]
        [Searchable] 
        private List<ILocalData> userDataList = new List<ILocalData>();

        protected override void OnEnable()
        {
            base.OnEnable();
            if (Application.isPlaying)
            {
                SyncWithRuntime();
            }
            else
            {
                LoadFromDisk();
            }
        }
    }
}
#endif