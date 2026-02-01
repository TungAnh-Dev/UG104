using System.Collections.Generic;
using UnityEngine;

public class LazyPooling : MonoBehaviour
{
    private static LazyPooling instant;

    public static LazyPooling Instant
    {
        get
        {
            if (instant == null)
            {
                if (FindAnyObjectByType<LazyPooling>() != null)
                    instant = FindAnyObjectByType<LazyPooling>();
                else
                    new GameObject().AddComponent<LazyPooling>().name = "Singleton_" + typeof(LazyPooling).ToString();
            }

            return instant;
        }
    }

    private void Awake()
    {
        if (instant != null && instant.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Debug.LogError("Singleton already exist " + instant.gameObject.name);
            Destroy(this.gameObject);
        }
        else
        {
            instant = this.GetComponent<LazyPooling>();
        }
    }

    private Dictionary<GameObject, List<GameObject>> _poolObjects2 = new();

    public GameObject GetObj(GameObject objKey, bool isKeepParent = false)
    {
        if (!this._poolObjects2.ContainsKey(objKey)) this._poolObjects2.Add(objKey, new List<GameObject>());

        foreach (var g in this._poolObjects2[objKey])
        {
            if (g.gameObject.activeSelf)
                continue;

            return g;
        }

        var g2 = Instantiate(objKey);
        this._poolObjects2[objKey].Add(g2);

        if (isKeepParent)
            g2.transform.SetParent(objKey.transform.parent);

        return g2;
    }

    private Dictionary<Component, List<Component>> _poolObjt = new();

    public T getObj<T>(T objKey, bool isKeepParent = false) where T : Component
    {
        if (!this._poolObjt.ContainsKey(objKey)) this._poolObjt.Add(objKey, new List<Component>());

        foreach (T g in this._poolObjt[objKey])
        {
            if (g.gameObject.activeSelf)
                continue;

            return g;
        }

        var g2 = Instantiate(objKey);
        this._poolObjt[objKey].Add(g2);

        if (isKeepParent)
            g2.transform.SetParent(objKey.transform.parent);

        return g2;
    }

    public void resetObj<T>(T objKey) where T : Component
    {
        if (!this._poolObjt.ContainsKey(objKey)) return;

        foreach (T g in this._poolObjt[objKey]) g.gameObject.SetActive(false);
    }

    public void CreatePool<T>(T keyObj, int size) where T : Component
    {
        if (!this._poolObjt.ContainsKey(keyObj)) this._poolObjt.Add(keyObj, new List<Component>());
        for (var i = 0; i < size; i++) this.getObj<T>(keyObj, true).gameObject.SetActive(false);
    }
}