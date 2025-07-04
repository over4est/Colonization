using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ResourceScaner), typeof(ResourceSorter), typeof(WorkerSpawner))]
[RequireComponent(typeof(ResourceStorage))]
public class Base : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Resource _resource;
    [SerializeField] private int _startWorkersAmount;
    [SerializeField] private Transform _resourceStorage;
    [SerializeField] private int _workerCost;

    private WorkerSpawner _workersSpawner;
    private ResourceScaner _scaner;
    private ResourceSorter _sorter;
    private ResourceStorage _storage;
    private Queue<Resource> _scannedResources = new Queue<Resource>();
    private List<Worker> _workers = new List<Worker>();

    public event Action<int> WorkerValueChanged;

    private void Awake()
    {
        _scaner = GetComponent<ResourceScaner>();
        _sorter = GetComponent<ResourceSorter>();
        _workersSpawner = GetComponent<WorkerSpawner>();
        _storage = GetComponent<ResourceStorage>();
    }

    private void OnEnable()
    {
        _workersSpawner.WorkerSpawned += SetupWorker;
    }

    private void OnDisable()
    {
        _workersSpawner.WorkerSpawned -= SetupWorker;

        foreach (Worker worker in _workers)
        {
            worker.ResourceDelivered -= AddResource;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _startWorkersAmount; i++)
        {
            _workersSpawner.Spawn();
        }
    }

    private void Update()
    {
        if (_storage.ResourceCount >= _workerCost)
        {
            _storage.SpendResource(_workerCost);
            _workersSpawner.Spawn();
        }

        if (_scannedResources.Count == 0)
        {
            return;
        }

        Worker worker = _workers.FirstOrDefault(w => w.CurrentState == WorkerState.Wait);

        if (worker != null)
        {
            Vector3 resource = _scannedResources.Dequeue().transform.position;

            worker.SetResourcePosition(resource);
            worker.SetState(WorkerState.MoveToResource);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_scaner.Scan(_resource, out List<Resource> scanResults))
        {
            _scannedResources = _sorter.SortByDistance(scanResults);
        }
    }

    private void SetupWorker(Worker worker)
    {
        _workers.Add(worker);
        WorkerValueChanged?.Invoke(_workers.Count);
        worker.SetStoragePosition(_resourceStorage.position);
        worker.SetState(WorkerState.Wait);
        worker.ResourceDelivered += AddResource;
    }

    private void AddResource()
    {
        _storage.AddResource();
    }
}