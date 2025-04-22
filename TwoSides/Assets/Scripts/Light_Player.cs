using UnityEngine;

public class ObjectBlinkerSequence : MonoBehaviour
{
    public float[] blinkIntervals = { 2f, 0.5f, 1f }; // 반복할 간격들
    public float blinkDuration = 0.1f;              // 꺼져 있는 시간

    private int currentIndex = 0;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkIntervals[currentIndex])
        {
            gameObject.SetActive(false); // 꺼짐
            Invoke("Reactivate", blinkDuration);
            timer = 0f;

            currentIndex = (currentIndex + 1) % blinkIntervals.Length; // 다음 간격으로
        }
    }

    void Reactivate()
    {
        gameObject.SetActive(true); // 다시 켜짐
    }
}
