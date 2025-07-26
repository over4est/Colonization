using UnityEngine;

public class WorkersCountView : View
{
    [SerializeField] private WorkersEmployer _employer;

    private readonly string _firstTextHalf = "Рабочих - ";

    private new void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        _employer.WorkersAmountChanged += ChangeText;
    }

    private void OnDisable()
    {
        _employer.WorkersAmountChanged -= ChangeText;
    }

    protected override void ChangeText(int newValue)
    {
        Text.text = _firstTextHalf + newValue.ToString();
    }
}