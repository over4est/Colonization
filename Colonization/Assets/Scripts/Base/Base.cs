using System;
using UnityEngine;

[RequireComponent(typeof(WorkersEmployer), typeof(FlagBuilder), typeof(ResourceSeeker))]
[RequireComponent(typeof(ResourceStorage))]
public class Base : MonoBehaviour
{
    [SerializeField] private int _hireCost = 3;
    [SerializeField] private int _buildCost = 5;

    private ResourceSeeker _resourceSeeker;
    private FlagBuilder _flagBuilder;
    private WorkersEmployer _employer;
    private ResourceStorage _storage;
    private Flag _flag;
    private bool _isFlagPlaced = false;

    public event Action<Vector3, Worker> FlagReached;

    private void Awake()
    {
        _resourceSeeker = GetComponent<ResourceSeeker>();
        _flagBuilder = GetComponent<FlagBuilder>();
        _employer = GetComponent<WorkersEmployer>();
        _storage = GetComponent<ResourceStorage>();
    }

    private void OnEnable()
    {
        _flagBuilder.FlagPlaced += SetFlag;
        _storage.WorkerBack += OnWorkerBack;
    }

    private void OnDisable()
    {
        _flagBuilder.FlagPlaced -= SetFlag;
        _storage.WorkerBack -= OnWorkerBack;

        if (_flag != null)
            _flag.FlagReached -= EraseFlag;
    }

    public void Init(InputReader inputReader, ResourceLocator locator)
    {
        _flagBuilder.Init(inputReader);
        _resourceSeeker.Init(locator);
    }

    public void SpawnWorker() => OnWorkerBack(_employer.HireWorker());

    public void HireWorker(Worker worker)
    {
        _employer.HireWorker(worker);
        OnWorkerBack(worker);
    }

    private void OnWorkerBack(Worker worker)
    {
        if (_isFlagPlaced)
        {
            if (_employer.WorkersCount > 1 && _storage.ResourceCount >= _buildCost)
                SendWorkerToBuild(worker);
            else if (_employer.WorkersCount <= 1 && _storage.ResourceCount >= _hireCost)
            {
                HireWorker();
                FarmResource(worker);
            }
            else
                FarmResource(worker);
        }
        else
        {
            if (_storage.ResourceCount >= _hireCost)
                HireWorker();

            FarmResource(worker);
        }
    }

    private void SetFlag(Flag flag)
    {
        if (_flag == null)
            _flag = flag;

        _flag.FlagReached += EraseFlag;
        _isFlagPlaced = true;
    }

    private void FarmResource(Worker worker)
    {
        Resource resource = _resourceSeeker.GetFreeResource();

        if (resource != null)
            worker.SetResource(resource);
    }

    private void HireWorker()
    {
        _storage.SpendResource(_hireCost);
        OnWorkerBack(_employer.HireWorker());
    }

    private void SendWorkerToBuild(Worker worker)
    {
        worker.SetTargetFlag(_flag);
        _storage.SpendResource(_buildCost);
    }

    private void EraseFlag(Worker worker)
    {
        _isFlagPlaced = false;

        _employer.FireWorker(worker);
        FlagReached?.Invoke(_flag.transform.position, worker);
    }
}