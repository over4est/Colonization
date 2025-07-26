using UnityEngine;

public class ResourceSeeker : MonoBehaviour
{
    private ResourceLocator _locator;

    public Resource GetFreeResource()
    {
        if (_locator.TryGetNearestResource(out Resource resource, transform.position))
            return resource;

        return null;
    }

    public void Init(ResourceLocator locator) => _locator = locator;
}