using System.Collections.Generic;
using UnityEngine;

public abstract class GenericSingletonPoolBase<T, TPool> : MonoSingleton<TPool>
    where T : Component
    where TPool : MonoSingleton<TPool>
{
    [SerializeField] private T prefab;
    [SerializeField] private int initialSize = 10;
    private Queue<T> pool = new Queue<T>();

    protected virtual void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            T obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public T GetFromPool()
    {
        T obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Instantiate(prefab, transform);
        }
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void SendToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
