using UnityEngine;

public class HideMapController : MonoBehaviour
{
    public GameObject hideMap;
    public static bool shouldShowHideMap = false;
    private void Update()
    {
        if (shouldShowHideMap && !hideMap.activeSelf)
        {
            hideMap.SetActive(true);
        }
        else if (!shouldShowHideMap && hideMap.activeSelf)
        {
            hideMap.SetActive(false);
        }
    }
}
