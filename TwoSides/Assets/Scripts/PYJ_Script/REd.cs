using UnityEngine;

public class ToggleImageOnEnter : MonoBehaviour
{
    public GameObject image2; // Inspector에서 Image2 오브젝트를 연결하세요.

    void Start()
    {
        if (image2 != null)
        {
            image2.SetActive(false); // 처음에 꺼진 상태로 시작
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Enter 키
        {
            if (image2 != null)
            {
                image2.SetActive(true); // Enter 키를 누르면 켜짐
            }
        }
    }
}
