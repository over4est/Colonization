using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DistanceMeter : MonoBehaviour
{
    public Resource GetNearestResource(List<Resource> resources, Vector3 basePosition) => resources.OrderBy(r => GetSqrDistance(r.transform.position, basePosition)).FirstOrDefault();

    public float GetSqrDistance(Vector3 target)
    {
        return (target - transform.position).sqrMagnitude;
    }

    private float GetSqrDistance(Vector3 target, Vector3 startPosition)
    {
        return (target - startPosition).sqrMagnitude;
    }
}