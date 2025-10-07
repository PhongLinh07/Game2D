using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class PoolRegister
    {
        public GameObject prefab;
        public int size = 10;
    }

    public static ObjectPoolManager Instance;

    [SerializeField] private List<PoolRegister> poolRegisters;

    private Dictionary<EObjectPool, Queue<GameObject>> poolDict = new();
    private Dictionary<GameObject, EObjectPool> prefabToKey  = new();

    private void Awake()
    {
        Instance = this;

        EObjectPool key = EObjectPool.None;

        foreach(var register in poolRegisters)
        {
            Queue<GameObject> pool = new();
            
            for(int i = 0; i < register.size; i++)
            {
                GameObject obj = Instantiate(register.prefab, transform);
                

                var poolable = obj.GetComponent<PoolableObject>();
                poolable?.SetPoolReference(this);
                key = poolable.GetKey();

                pool.Enqueue(obj);
                obj.SetActive(false);
            }

            poolDict.Add(key, pool);
            prefabToKey[register.prefab] = key;
        }
    }

    public GameObject Spawn(EObjectPool key)
    {
        if(!poolDict.ContainsKey(key))
        {
            Debug.Log($"No poll found with key: {key}");   
            return null;
        }

        var obj = poolDict[key].Dequeue();
        obj.SetActive(true);
        obj.GetComponent<PoolableObject>()?.OnSpawn();

        return obj;
    }

    public GameObject Spawn(GameObject prefab)
    {
        if (!prefabToKey.TryGetValue(prefab, out EObjectPool key))
        {
            Debug.LogWarning($"Prefab not registered in pool: {prefab.name}");
            return null;
        }

        return Spawn(key);
    }
    public void ReturnToPool(GameObject obj)
    {
        var poolable = obj.GetComponent<PoolableObject>();
        poolable?.OnReturn();

        EObjectPool key = poolable.GetKey(); // giả sử object nhớ key của nó
        if (key != EObjectPool.None && poolDict.ContainsKey(key))
        {
            poolDict[key].Enqueue(obj); // ✅ Trả lại vào pool
        }

        obj.SetActive(false);
    }
}
