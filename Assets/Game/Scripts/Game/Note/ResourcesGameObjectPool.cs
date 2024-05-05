using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ResourcesGameObjectPool
{
    private ObjectPool<GameObject> _gameObjectPool;
    private string _prefabPath;

    public ResourcesGameObjectPool(string prefabPath)
    {
        _prefabPath = prefabPath;
        _gameObjectPool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private GameObject CreateFunc()
    {
        var prefab = Resources.Load<GameObject>(_prefabPath);
        var gob = UnityEngine.Object.Instantiate(prefab);
        gob.SetActive(true);
        return gob;
    }

    private void ActionOnGet(GameObject @object)
    {

    }

    private void ActionOnRelease(GameObject @object)
    {
        @object.SetActive(false);
    }

    private void ActionOnDestroy(GameObject @object)
    {
        UnityEngine.Object.Destroy(@object);
    }

    public GameObject Get(Transform parent = null)
    {
        var gob = _gameObjectPool.Get();
        if (parent != null)
        {
            gob.transform.SetParent(parent);
            gob.transform.localPosition = Vector3.zero;
            gob.transform.localScale = Vector3.one;
            gob.transform.localRotation = Quaternion.identity;
        }
        return gob;
    }

    public void Release(GameObject @object)
    {
        _gameObjectPool.Release(@object);
    }
}
