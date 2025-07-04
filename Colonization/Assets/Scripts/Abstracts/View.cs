using TMPro;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    protected TextMeshProUGUI _text;

    protected void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    protected abstract void ChangeText(int newValue);
}