using System.Collections.Generic;
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
        foreach(Base @base in _spawnedBases)
            @base.FlagReached -= SpawnOnPositionWithWorker;
    }

    public override Base Spawn()
    {
        Base @base = Pool.Get();

        @base.gameObject.SetActive(true);
        _spawnedBases.Add(@base);
        @base.FlagReached += SpawnOnPositionWithWorker;
        @base.Init(_inputReader, _resourceLocator);

        _spawnPosition.y = _ySpawnPosition;
        @base.transform.position = _spawnPosition;

        return @base;
    }

    private void SpawnOnPositionWithWorker(Vector3 position, Worker worker)
    {
        _spawnPosition = position;

        Base @base = Spawn();

        @base.HireWorker(worker);
    }
}