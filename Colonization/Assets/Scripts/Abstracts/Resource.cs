using System;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public abstract class Resource : MonoBehaviour
{
    public event Action<Resource> DispawnNeeded;

    private float _distanceFromBase;
    private Rigidbody _attachedRigidbody;

    public float DistanceFromBase => _distanceFromBase;
    public Rigidbody Rigidbody => _attachedRigidbody;

    private void Awake()
    {
        _attachedRigidbody = GetComponent<Rigidbody>();
    }

    public void SetDistance(float distance)
    {
        _distanceFromBase = distance;
    }

    public void CallDispawn()
    {
        DispawnNeeded?.Invoke(this);
    }
}