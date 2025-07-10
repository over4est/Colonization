using UnityEngine;

public class LookAtMainCamera : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 direction = transform.position - _camera.transform.position;

        transform.rotation = Quaternion.LookRotation(direction);
    }
}