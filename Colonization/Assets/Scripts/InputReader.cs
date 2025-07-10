using System;
using UnityEngine;

[RequireComponent(typeof(CameraMover), typeof(SmoothZoomer))]
public class InputReader : MonoBehaviour
{
    private PlayerInput _playerInput;

    public event Action<float> ZoomNeeded;
    public event Action<Vector2> MoveNeeded;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        Vector2 direction = _playerInput.Camera.Movement.ReadValue<Vector2>();

        if (direction != Vector2.zero)
        {
            MoveNeeded?.Invoke(direction);
        }

        Vector2 scrollValue = _playerInput.Camera.Zoom.ReadValue<Vector2>();

        if (scrollValue.y != 0f)
        {
            ZoomNeeded?.Invoke(scrollValue.y);
        }
    }
}
