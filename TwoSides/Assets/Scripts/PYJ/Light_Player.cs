using UnityEngine;

public class ObjectBlinkerSequence : MonoBehaviour
{
    public float[] blinkIntervals = { 2f, 0.5f, 1f }; // �ݺ��� ���ݵ�
    public float blinkDuration = 0.1f;              // ���� �ִ� �ð�

    private int currentIndex = 0;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkIntervals[currentIndex])
        {
            gameObject.SetActive(false); // ����
            Invoke("Reactivate", blinkDuration);
            timer = 0f;

            currentIndex = (currentIndex + 1) % blinkIntervals.Length; // ���� ��������
        }
    }

    void Reactivate()
    {
        gameObject.SetActive(true); // �ٽ� ����
    }
}
