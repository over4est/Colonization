using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab;

    [SerializeField] private int _poolCapacity;

    protected ObjectPool<T> Pool;

    protected void Awake()
    {
        Pool = new ObjectPool<T>(Prefab, _poolCapacity, transform);
    }

    public abstract T Spawn();
}