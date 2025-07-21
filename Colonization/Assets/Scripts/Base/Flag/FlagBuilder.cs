using System;
using UnityEngine;

[RequireComponent(typeof(AreaScanner), typeof(FlagColorChanger))]
public class FlagBuilder : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Flag _prefab;

    private Flag _flag;
    private bool _isBuildModeEnable = false;
    private InputReader _inputReader;
    private AreaScanner _areaScanner;
    private bool _isAreaCorrect;
    private Vector3 _currentPosition;
    private FlagColorChanger _colorChanger;
    private bool _isFlagPlaced = false;

    public bool IsBuildModeEnable => _isBuildModeEnable;

    public event Action<Flag> FlagPlaced;

    private void OnDisable()
    {
        if (_flag != null)
            _flag.DisableNeeded -= DisableFlag;
    }

    public void EnableBuildMode()
    {
        _inputReader.ClickDetected += Build;
        _inputReader.WorldPointerMoved += MovePreviewToMouse;
        _isBuildModeEnable = true;
        _isFlagPlaced = false;

        _flag.gameObject.SetActive(true);
    }

    public void DisableBuildMode()
    {
        _inputReader.ClickDetected -= Build;
        _inputReader.WorldPointerMoved -= MovePreviewToMouse;
        _isBuildModeEnable = false;

        if (_isFlagPlaced == false)
            DisableFlag();
    }

    public void Init(InputReader inputReader)
    {
        _flag = InitFlag();
        _flag.DisableNeeded += DisableFlag;
        _colorChanger = GetComponent<FlagColorChanger>();
        _areaScanner = GetComponent<AreaScanner>();
        _inputReader = inputReader;
    }

    private void DisableFlag()
    {
        _flag.gameObject.SetActive(false);
    }

    private void Build()
    {
        if (_isAreaCorrect)
        {
            _flag.transform.position = _currentPosition;
            _isFlagPlaced = true;
            _isAreaCorrect = false;

            DisableBuildMode();
            FlagPlaced?.Invoke(_flag);
        }
    }

    private void MovePreviewToMouse(Vector3 position)
    {
        _currentPosition = position;
        _flag.transform.position = _currentPosition;
        _isAreaCorrect = _areaScanner.IsAreaCorrect(_currentPosition);

        if (_isAreaCorrect)
            _colorChanger.ChangeColor(_flag.Material, Color.blue);
        else
            _colorChanger.ChangeColor(_flag.Material, Color.red);
    }

    private Flag InitFlag()
    {
        Flag flag = Instantiate(_prefab, transform);
        flag.gameObject.SetActive(false);

        return flag;
    }
}