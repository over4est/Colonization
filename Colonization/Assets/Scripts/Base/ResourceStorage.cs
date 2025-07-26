using System;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private int _resourceCount = 0;

    public event Action<int> ValueChanged;
    public event Action<Worker> WorkerBack;

    public int ResourceCount => _resourceCount;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Worker worker) && worker.CurrentBasePosition == transform.position)
        {
            if (worker.HaveResourceInHand)
            {
                AddResource();
                worker.FreeHands();
            }

            if (worker.IsFree == false)
                return;

            WorkerBack?.Invoke(worker);
        }
    }

    public void SpendResource(int value)
    {
        if (_resourceCount - value >= 0)
        {
            _resourceCount -= value;

            ValueChanged?.Invoke(_resourceCount);
        }
    }

    private void AddResource()
    {
        _resourceCount++;

        ValueChanged?.Invoke(_resourceCount);
    }
}