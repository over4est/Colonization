using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>
{
    [SerializeField] private int _startSpawnAmount;
    [SerializeField] private float _spawnDistance;

    private List<Resource> _resources = new List<Resource>();

    private void Start()
    {
        for (int i = 0; i < _startSpawnAmount; i++)
        {
            Spawn();
        }
    }

    private void OnDisable()
    {
        foreach (Resource resource in _resources)
        {
            resource.DispawnNeeded -= Release;
        }
    }

    public override void Spawn()
    {
        Resource obj = Pool.Get();
        Vector3 spawnPosition = Random.onUnitSphere * _spawnDistance + SpawnPosition.position;

        obj.transform.position = new Vector3(spawnPosition.x, SpawnPosition.position.y, spawnPosition.z);
        obj.transform.rotation = Random.rotation;
        obj.gameObject.SetActive(true);
        obj.DispawnNeeded += Release;
        _resources.Add(obj);
    }

    public void Release(Resource resource)
    {
        resource.Rigidbody.isKinematic = false;
        resource.transform.SetParent(transform);
        Pool.Release(resource);
    }
}