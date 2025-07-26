using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceMeter))]
public class ResourceLocator : MonoBehaviour
{
    private List<Resource> _freeResources = new List<Resource>();
    private DistanceMeter _distanceMeter;

    private void Awake()
    {
        _distanceMeter = GetComponent<DistanceMeter>();
    }

    public bool TryGetNearestResource(out Resource resource, Vector3 basePosition)
    {
        resource = _distanceMeter.GetNearestResource(_freeResources, basePosition);

        if (resource == null)
            return false;

        _freeResources.Remove(resource);
        return true;
    }

    public void AddFreeResources(Resource resource) => _freeResources.Add(resource);
}