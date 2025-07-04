public class ResourceCountView : View
{
    private readonly string _firstTextHalf = "Рeсурсов - ";

    private ResourceStorage _resourceStorage;

    private new void Awake()
    {
        base.Awake();
        _resourceStorage = GetComponentInParent<ResourceStorage>();
    }

    private void Start()
    {
        _text.text = _firstTextHalf + 0.ToString();
    }

    private void OnEnable()
    {
        _resourceStorage.ValueChanged += ChangeText;
    }

    private void OnDisable()
    {
        _resourceStorage.ValueChanged -= ChangeText;
    }

    protected override void ChangeText(int newValue)
    {
        _text.text = _firstTextHalf + newValue.ToString();
    }
}