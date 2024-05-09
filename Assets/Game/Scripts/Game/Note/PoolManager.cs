using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PoolManager();
            }
            return _instance;
        }
    }

    private Dictionary<string, ResourcesGameObjectPool> _pools;

    private PoolManager()
    {
        _pools = new Dictionary<string, ResourcesGameObjectPool>();
    }

    public GameObject Get(string path, Transform parent)
    {
        var pool = GetPool(path);
        return pool.Get(parent);
    }

    public void Release(string path, GameObject @object)
    {
        var pool = GetPool(path);
        pool.Release(@object);
    }

    private ResourcesGameObjectPool GetPool(string path)
    {
        if (!_pools.TryGetValue(path, out var pool))
        {
            pool = new ResourcesGameObjectPool(path);
            _pools.Add(path, pool);
        }
        return pool;
    }
}
