using UnityEngine;

public class ResourceCountView : View
{
    [SerializeField] private ResourceStorage _resourceStorage;

    private readonly string _firstTextHalf = "Рeсурсов - ";

    private new void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Text.text = _firstTextHalf + 0.ToString();
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
        Text.text = _firstTextHalf + newValue.ToString();
    }
}