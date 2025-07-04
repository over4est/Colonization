using System;

public class WorkerSpawner : Spawner<Worker>
{
    public event Action<Worker> WorkerSpawned;

    public override void Spawn()
    {
        if (_pool.TryGet(out Worker worker))
        {
            worker.transform.position = _spawnPosition.position;

            worker.gameObject.SetActive(true);
            WorkerSpawned?.Invoke(worker);
        }
    }
}