using System;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private int _resourceCount = 0;

    public event Action<int> ValueChanged;

    public void AddResource()
    {
        _resourceCount++;
        ValueChanged?.Invoke(_resourceCount);
    }
}