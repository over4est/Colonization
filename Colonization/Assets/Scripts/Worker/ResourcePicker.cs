using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(ResourceScaner))]
public class ResourcePicker : MonoBehaviour
{
    [SerializeField] private float _pickUpDuration;
    [SerializeField] private Transform _resourceSlot;

    private ResourceScaner _scaner;

    private void Awake()
    {
        _scaner = GetComponent<ResourceScaner>();
    }

    public Resource PickUpResource(Vector3 position)
    {
        Resource resource = _scaner.Scan(position);

        resource.transform.SetParent(_resourceSlot);
        resource.transform.DOLocalMove(Vector3.zero, _pickUpDuration);
        resource.Rigidbody.isKinematic = true;

        return resource;
    }
}