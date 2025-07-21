using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CameraMover), typeof(SmoothZoomer), typeof(ClickScanner))]
public class InputReader : MonoBehaviour
{
    private ClickScanner _scanner;
    private PlayerInput _playerInput;
    private Camera _camera;

    public event Action<float> ZoomNeeded;
    public event Action<Vector2> MoveNeeded;
    public event Action<Vector3> WorldPointerMoved;
    public event Action ClickDetected;

    private void Awake()
    {
        _scanner = GetComponent<ClickScanner>();
        _camera = Camera.main;
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Mouse.Click.performed += OnClick;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Mouse.Click.performed -= OnClick;
        _playerInput.Disable();
    }

    private void Update()
    {
        ReadMoveInput();
        ReadZoomInput();
        ReadPointerMove();
    }

    private void ReadMoveInput()
    {
        Vector2 direction = _playerInput.Camera.Movement.ReadValue<Vector2>();

        if (direction != Vector2.zero)
        {
            MoveNeeded?.Invoke(direction);
        }
    }

    private void ReadZoomInput()
    {
        Vector2 scrollValue = _playerInput.Camera.Zoom.ReadValue<Vector2>();

        if (scrollValue.y != 0f)
        {
            ZoomNeeded?.Invoke(scrollValue.y);
        }
    }

    private void ReadPointerMove()
    {
        Ray ray = GetCameraRay();

        if (_scanner.TryGetMouseWorldPosition(ray, out Vector3 position))
        {
            WorldPointerMoved?.Invoke(position);
        }
    }

    private void OnClick(InputAction.CallbackContext _)
    {
        Ray ray = GetCameraRay();

        _scanner.Scan(ray);
        ClickDetected?.Invoke();
    }

    private Ray GetCameraRay() => _camera.ScreenPointToRay(_playerInput.Mouse.MousePosition.ReadValue<Vector2>());
}