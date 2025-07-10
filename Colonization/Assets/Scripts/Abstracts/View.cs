using TMPro;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    protected TextMeshProUGUI Text;

    protected void Awake()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    protected abstract void ChangeText(int newValue);
}