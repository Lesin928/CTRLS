using UnityEngine;

public class ObjectBlinkerSequence : MonoBehaviour
{
    public float[] blinkIntervals = { 2f, 0.5f, 1f }; // 반복할 간격들

    public float blinkDuration = 0.1f;              // 꺼져 있는 시간


    private int currentIndex = 0;

    private float timer = 0f;




    /// <summary>
    /// 매 프레임마다 호출되며, 타이머를 증가시키고 설정된 간격마다 오브젝트를 비활성화시킴
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime; 

        if (timer >= blinkIntervals[currentIndex])
        {
            gameObject.SetActive(false); // 오브젝트를 꺼짐 상태로 만들기
            Invoke("Reactivate", blinkDuration); // 일정 시간 후 다시 켜는 함수 호출
            timer = 0f; // 타이머 초기화

            currentIndex = (currentIndex + 1) % blinkIntervals.Length; // 다음 깜빡임 간격으로 순환
        }
    }

    /// <summary>
    /// 비활성화된 오브젝트를 다시 켜는 함수
    /// </summary>
    void Reactivate()
    {
        gameObject.SetActive(true); // 오브젝트를 다시 활성화
    }

}
