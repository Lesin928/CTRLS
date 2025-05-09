using UnityEngine;

public class ToggleImageOnEnter : MonoBehaviour
{
    public GameObject image2; // Inspector에서 Image2 오브젝트를 연결하세요.

    // void Start()
    // {
    //     if (image2 != null)
    //     {
    //         image2.SetActive(false); // 이 줄을 제거 또는 주석 처리
    //     }
    // }

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
