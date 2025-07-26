using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _conteiner;
    private Stack<T> _pool;
    private List<T> _objects = new List<T>();

    public ObjectPool(T prefab, int objectCount, Transform container)
    {
        _prefab = prefab;
        _conteiner = container;

        InitPool(objectCount);
    }

    public T Get()
    {
        if (_pool.TryPop(out var element))
        {
            return element;
        }

        return CreateObject();
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }

    public List<T> GetAllObjects() => new List<T>(_objects);

    private void InitPool(int count)
    {
        _pool = new Stack<T>();

        for (int i = 0; i < count; i++)
        {
            T obj = CreateObject();

            _pool.Push(obj);
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var newObject = Object.Instantiate(_prefab, _conteiner);

        newObject.gameObject.SetActive(isActiveByDefault);
        _objects.Add(newObject);

        return newObject;
        
    }
}