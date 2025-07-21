using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceStorage), typeof(BaseStateMachineFactory), typeof(FlagBuilder))]
[RequireComponent(typeof(WorkerSpawner))]
public class Base : MonoBehaviour, IClickable
{
    [SerializeField] private int _hireCost = 3;
    [SerializeField] private int _buildCost = 5;

    private Transform _resourceStorageTransform;
    private Resource _currentResource;
    private FlagBuilder _flagBuilder;
    private BaseStateMachine _stateMachine;
    private WorkerSpawner _workersSpawner;
    private ResourceStorage _storage;
    private List<Worker> _workers = new List<Worker>();
    private Flag _flag;
    private bool _isFlagPlaced = false;

    public event Action<Base> ResourceNeeded;
    public event Action<int> WorkerValueChanged;
    public event Action<Vector3, Worker> WorkerReachedFlag;

    public bool IsFlagPlaced => _isFlagPlaced;

    private void Awake()
    {
        _stateMachine = GetComponent<BaseStateMachineFactory>().Create(this);
        _flagBuilder = GetComponent<FlagBuilder>();
        _workersSpawner = GetComponent<WorkerSpawner>();
        _storage = GetComponent<ResourceStorage>();
        _resourceStorageTransform = GetComponentInChildren<ResourceStoragePoint>().transform;
    }

    private void OnEnable()
    {
        _flagBuilder.FlagPlaced += SetFlag;
        _workersSpawner.WorkerSpawned += SetupWorker;
    }

    private void OnDisable()
    {
        _flagBuilder.FlagPlaced -= SetFlag;
        _workersSpawner.WorkerSpawned -= SetupWorker;

        foreach (Worker worker in _workers)
        {
            worker.ResourceDelivered -= AddResource;
            worker.FlagReached -= OnFlagReached;
        }
    }

    private void Update()
    {
        _stateMachine.Update();
        FarmResource();
    }

    public void HireWorker()
    {
        if (_storage.ResourceCount >= _hireCost)
        {
            _storage.SpendResource(_hireCost);
            SpawnWorker();
        }
    }

    public void SendWorkerToBuild()
    {
        if (_storage.ResourceCount >= _buildCost && TryGetFreeWorker(out Worker worker))
        {
            worker.SetTargetFlag(_flag);
            _storage.SpendResource(_buildCost);

            _isFlagPlaced = false;
        }
    }

    public void OnClick()
    {
        if (_workers.Count <= 1)
            return;

        if (_flagBuilder.IsBuildModeEnable)
            _flagBuilder.DisableBuildMode();
        else
            _flagBuilder.EnableBuildMode();
    }

    public void InitFlagBuilder(InputReader inputReader) => _flagBuilder.Init(inputReader);

    public void SpawnWorker() => _workersSpawner.Spawn();

    public void SetCurrentResource(Resource resource) => _currentResource = resource;

    public void SetupWorker(Worker worker)
    {
        _workers.Add(worker);
        WorkerValueChanged?.Invoke(_workers.Count);
        worker.SetStoragePosition(_resourceStorageTransform.position);
        worker.ResourceDelivered += AddResource;
        worker.FlagReached += OnFlagReached;
    }

    private void SetFlag(Flag flag)
    {
        _flag = flag;
        _isFlagPlaced = true;
    }

    private void FarmResource()
    {
        if (TryGetFreeWorker(out Worker worker))
        {
            if (_currentResource == null)
                ResourceNeeded?.Invoke(this);
            else
            {
                worker.SetResource(_currentResource);

                _currentResource = null;
            }
        }
    }

    private void AddResource() => _storage.AddResource();

    private void OnFlagReached(Vector3 position, Worker worker)
    {
        _workers.Remove(worker);
        WorkerValueChanged?.Invoke(_workers.Count);

        worker.ResourceDelivered -= AddResource;
        worker.FlagReached -= OnFlagReached;

        WorkerReachedFlag?.Invoke(position, worker);
    }

    private bool TryGetFreeWorker(out Worker worker)
    {
        worker = _workers.FirstOrDefault(w => w.IsFree);

        return worker != null;
    }
}