using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseSpawner : Spawner<Base>
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private int _startWorkersAmount;
    [SerializeField] private float _ySpawnPosition = 0.5f;
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private ResourceLocator _resourceLocator;

    private List<Base> _spawnedBases = new List<Base>();

    private void Start()
    {
        Spawn();

        for (int i = 0; i < _startWorkersAmount; i++)
        {
            _spawnedBases[0].SpawnWorker();
        }
    }

    private void OnDisable()
    {
        foreach (Base @base in _spawnedBases)
        {
            @base.WorkerReachedFlag -= OnFlagReached;
            @base.ResourceNeeded -= GetNearestResource;
        }
    }

    public override void Spawn()
    {
        Base @base = Pool.Get();

        @base.gameObject.SetActive(true);
        _spawnedBases.Add(@base);
        @base.WorkerReachedFlag += OnFlagReached;
        @base.ResourceNeeded += GetNearestResource;
        @base.InitFlagBuilder(_inputReader);

        _spawnPosition.y = _ySpawnPosition;
        @base.transform.position = _spawnPosition;
    }

    private void OnFlagReached(Vector3 newPosition, Worker worker)
    {
        SetSpawnPosition(newPosition);
        Spawn();
        SetupWorker(_spawnedBases.Last(), worker);
    }

    private void SetSpawnPosition(Vector3 newPosition) => _spawnPosition = newPosition;

    private void SetupWorker(Base @base, Worker worker) => @base.SetupWorker(worker);

    private void GetNearestResource(Base @base)
    {
        if (_resourceLocator.TryGetNearestResource(out Resource resource, @base.transform.position))
            @base.SetCurrentResource(resource);
    }
}