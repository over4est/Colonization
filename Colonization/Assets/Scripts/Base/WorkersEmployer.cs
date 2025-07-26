using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorkerSpawner))]
public class WorkersEmployer : MonoBehaviour
{
    private WorkerSpawner _workerSpawner;
    private List<Worker> _workers = new List<Worker>();

    public event Action<int> WorkersAmountChanged;

    public int WorkersCount => _workers.Count;

    private void Awake()
    {
        _workerSpawner = GetComponent<WorkerSpawner>();
    }

    public Worker HireWorker()
    {
        Worker newWorker = _workerSpawner.Spawn();

        _workers.Add(newWorker);
        WorkersAmountChanged?.Invoke(WorkersCount);
        newWorker.SetStoragePosition(transform.position);

        return newWorker;
    }

    public void HireWorker(Worker worker)
    {
        _workers.Add(worker);
        WorkersAmountChanged?.Invoke(WorkersCount);
        worker.SetStoragePosition(transform.position);
    }

    public void FireWorker(Worker worker)
    {
        _workers.Remove(worker);
        WorkersAmountChanged?.Invoke(WorkersCount);
    }
}