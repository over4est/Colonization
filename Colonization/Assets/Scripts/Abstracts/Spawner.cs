using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected Transform SpawnPosition;
    [SerializeField] protected T Prefab;

    [SerializeField] private int _poolCapacity;

    protected ObjectPool<T> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<T>(Prefab, _poolCapacity, transform);
    }

    public abstract void Spawn();
}
