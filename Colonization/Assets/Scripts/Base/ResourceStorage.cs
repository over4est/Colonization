using System;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private int _resourceCount = 0;

    public event Action<int> ValueChanged;
    public int ResourceCount => _resourceCount;

    public void AddResource()
    {
        _resourceCount++;
        ValueChanged?.Invoke(_resourceCount);
    }

    public void SpendResource(int count)
    {
        if (_resourceCount - count >= 0)
        {
            _resourceCount -= count;

            ValueChanged?.Invoke(_resourceCount);
        }
    }
}