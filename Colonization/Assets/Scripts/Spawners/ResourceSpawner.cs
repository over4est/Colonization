using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ResourceLocator))]
public class ResourceSpawner : Spawner<Resource>
{
    [SerializeField] private int _startSpawnAmount;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _spawnDistance;

    private ResourceLocator _resourceLocator;

    protected new void Awake()
    {
        base.Awake();

        _resourceLocator = GetComponent<ResourceLocator>();
    }

    private void Start()
    {
        for (int i = 0; i < _startSpawnAmount; i++)
            Spawn();

        StartCoroutine(DelaySpawn(_spawnDelay));
    }

    private void OnDisable()
    {
        foreach (Resource resource in Pool.GetAllObjects())
            resource.DispawnNeeded -= Release;
    }

    public override Resource Spawn()
    {
        Resource obj = Pool.Get();
        Vector3 spawnPosition = Random.onUnitSphere * _spawnDistance + _spawnPosition.position;

        obj.transform.position = new Vector3(spawnPosition.x, _spawnPosition.position.y, spawnPosition.z);
        obj.transform.rotation = Random.rotation;
        obj.gameObject.SetActive(true);
        obj.DispawnNeeded += Release;
        _resourceLocator.AddFreeResources(obj);

        return obj;
    }

    private IEnumerator DelaySpawn(float spawnDelay)
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        while (enabled)
        {
            yield return wait;

            Spawn();
        }
    }

    private void Release(Resource resource)
    {
        resource.Rigidbody.isKinematic = false;
        resource.transform.SetParent(transform);
        Pool.Release(resource);
    }
}