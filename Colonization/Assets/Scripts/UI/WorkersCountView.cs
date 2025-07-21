using UnityEngine;

public class WorkersCountView : View
{
    [SerializeField] private Base _base;

    private readonly string _firstTextHalf = "Рабочих - ";

    private new void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        _base.WorkerValueChanged += ChangeText;
    }

    private void OnDisable()
    {
        _base.WorkerValueChanged -= ChangeText;
    }

    protected override void ChangeText(int newValue)
    {
        Text.text = _firstTextHalf + newValue.ToString();
    }
}