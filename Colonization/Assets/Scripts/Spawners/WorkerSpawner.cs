using UnityEngine;

public class WorkerSpawner : Spawner<Worker>
{
    [SerializeField] private Transform _spawnPosition;

    public override Worker Spawn()
    {
        Worker worker = Pool.Get();

        worker.transform.position = _spawnPosition.position;

        worker.gameObject.SetActive(true);

        return worker;
    }
}