using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public event Action<Worker> FlagReached;

    public Material Material => _meshRenderer.material;

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Worker worker) && worker.HaveTargetFlag)
        {
            worker.SetTargetFlag(null);
            FlagReached?.Invoke(worker);
            gameObject.SetActive(false);
        }
    }
}