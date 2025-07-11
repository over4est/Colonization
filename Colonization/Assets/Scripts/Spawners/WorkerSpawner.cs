using System;

public class WorkerSpawner : Spawner<Worker>
{
    public event Action<Worker> WorkerSpawned;

    public override void Spawn()
    {
        Worker worker = Pool.Get();

        worker.transform.position = SpawnPosition.position;

        worker.gameObject.SetActive(true);
        WorkerSpawned?.Invoke(worker);
    }
}