using DG.Tweening;
using UnityEngine;

public class ResourcePicker : MonoBehaviour
{
    [SerializeField] private float _pickUpDuration;
    [SerializeField] private Transform _resourceSlot;

    public Resource PickUpResource(Resource resource)
    {
        resource.transform.SetParent(_resourceSlot);
        resource.transform.DOLocalMove(Vector3.zero, _pickUpDuration);
        resource.Rigidbody.isKinematic = true;

        return resource;
    }
}