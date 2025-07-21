using System;
using UnityEngine;

public class WorkerSpawner : Spawner<Worker>
{
    [SerializeField] private Transform _spawnPosition;

    public event Action<Worker> WorkerSpawned;

    public override void Spawn()
    {
        Worker worker = Pool.Get();

        worker.transform.position = _spawnPosition.position;

        worker.gameObject.SetActive(true);
        WorkerSpawned?.Invoke(worker);
    }
}