using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public event Action DisableNeeded;

    public Material Material => _meshRenderer.material;

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void CallDisable() => DisableNeeded?.Invoke();
}