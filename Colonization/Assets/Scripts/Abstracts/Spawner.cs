using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected Transform _spawnPosition;
    [SerializeField] protected T _prefab;

    [SerializeField] private int _poolCapacity;

    protected ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(_prefab, _poolCapacity, transform);
    }

    public abstract void Spawn();
}
