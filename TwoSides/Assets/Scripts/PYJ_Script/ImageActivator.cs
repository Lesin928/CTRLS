using UnityEngine;

public class ImageActivator : MonoBehaviour
{
    public ToggleImageOnEnter imageToggleScript; // ToggleImageOnEnter 스크립트를 Inspector에서 할당

    void Start()
    {
        if (imageToggleScript != null)
        {
            imageToggleScript.ShowImage(); // 게임 시작 시 image2 켜기
        }
        else
        {
            Debug.LogWarning("imageToggleScript가 할당되지 않았습니다.");
        }
    }
}
