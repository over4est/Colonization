using Cinemachine;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class SmoothZoomer : MonoBehaviour
{
    [SerializeField] private float _zoomDuration;
    [SerializeField] private float _minFOV;
    [SerializeField] private float _maxFOV;
    [SerializeField] private float _zoomSpeed;

    private InputReader _input;
    private CinemachineVirtualCamera _camera;
    private Coroutine _coroutine;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _input = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _input.ZoomNeeded += Zoom;
    }

    private void OnDisable()
    {
        _input.ZoomNeeded -= Zoom;
    }

    private void Zoom(float value)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        float changedValue = _camera.m_Lens.FieldOfView - value * _zoomSpeed;
        float targetFOV = Mathf.Clamp(changedValue, _minFOV, _maxFOV);

        _coroutine = StartCoroutine(SmoothZoom(targetFOV));
    }

    private IEnumerator SmoothZoom(float targetFOV)
    {
        float elapsedTime = 0f;
        float currentFov = _camera.m_Lens.FieldOfView;

        while (elapsedTime < _zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            _camera.m_Lens.FieldOfView = Mathf.Lerp(currentFov, targetFOV, elapsedTime / _zoomDuration);
            yield return null;
        }

        _camera.m_Lens.FieldOfView = targetFOV;
    }
}