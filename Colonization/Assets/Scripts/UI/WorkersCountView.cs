public class WorkersCountView : View
{
    private readonly string _firstTextHalf = "Рабочих - ";

    private Base _base;

    private new void Awake()
    {
        base.Awake();
        _base = GetComponentInParent<Base>();
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
        _text.text = _firstTextHalf + newValue.ToString();
    }
}
