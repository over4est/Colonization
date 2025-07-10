using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private InputReader _input;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _input.MoveNeeded += Move;
    }

    private void OnDisable()
    {
        _input.MoveNeeded -= Move;
    }

    private void Move(Vector2 direction)
    {
        Vector3 editedDirection = new Vector3(direction.x, 0f, direction.y);

        transform.Translate(editedDirection * _movementSpeed * Time.deltaTime, Space.World);
    }
}