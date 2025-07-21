using UnityEngine;

public class FlagColorChanger : MonoBehaviour
{
    public void ChangeColor(Material material, Color newColor) =>material.color = newColor;
}