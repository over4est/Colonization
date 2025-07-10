using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DistanceMeter))]
public class ResourceSorter : MonoBehaviour
{
    private DistanceMeter _distanceMeter;

    private void Awake()
    {
        _distanceMeter = GetComponent<DistanceMeter>();
    }

    public Queue<Resource> SortByDistance(List<Resource> resources)
    {
        List<Resource> tempResult = resources.OrderBy(p => _distanceMeter.MeasureDistance(p)).ToList();
        Queue<Resource> result = new Queue<Resource>();

        foreach (Resource resource in tempResult)
        {
            result.Enqueue(resource);
        }

        return result;
    }
}