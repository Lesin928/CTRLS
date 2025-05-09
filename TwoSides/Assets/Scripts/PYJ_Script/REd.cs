using UnityEngine;

public class ToggleImageOnEnter : MonoBehaviour
{
    public GameObject image2; // Inspector에서 Image2 오브젝트를 연결하세요.

    // 외부에서 호출할 함수
    public void ShowImage()
    {
        if (image2 != null)
        {
            image2.SetActive(true); // 이미지 켜기
        }
    }

    public void HideImage()
    {
        if (image2 != null)
        {
            image2.SetActive(false); // 이미지 끄기
        }
    }
}
