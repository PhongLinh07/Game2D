using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EObjectPool
{
    None,

    // đạn 
    FlyingSword,
    ProjectileChicken,

    // item
    FallingSword
}

public interface IPoolableObject
{
    void OnSpawn();
    void OnReturn();
    void SetPoolReference(ObjectPoolManager manager); // Gán key

    void ReturnToPool(GameObject obj);
    EObjectPool GetKey();
}

[System.Serializable]
public class PoolableObject : MonoBehaviour, IPoolableObject
{
    [SerializeField]
    private EObjectPool _poolKey = EObjectPool.None;
    private ObjectPoolManager _poolManager;

    
    public virtual void OnSpawn() { }

    public virtual void OnReturn() { }
    public void SetPoolReference(ObjectPoolManager manager)
    {
        _poolManager = manager;
    }
    public void ReturnToPool(GameObject obj)
    {
        
        _poolManager.ReturnToPool(obj);
    }
    public EObjectPool GetKey() 
    {
        return _poolKey;
    }
}
