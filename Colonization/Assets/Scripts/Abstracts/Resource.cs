using System;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public abstract class Resource : MonoBehaviour
{
    public event Action<Resource> DispawnNeeded;

    private Rigidbody _attachedRigidbody;

    public Rigidbody Rigidbody => _attachedRigidbody;

    private void Awake()
    {
        _attachedRigidbody = GetComponent<Rigidbody>();
    }

    public void CallDispawn()
    {
        DispawnNeeded?.Invoke(this);
    }
}