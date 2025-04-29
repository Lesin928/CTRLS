using UnityEngine;

public class ObjectBlinkerSequence : MonoBehaviour
{
    public float[] blinkIntervals = { 2f, 0.5f, 1f }; // �ݺ��� ���ݵ�

    public float blinkDuration = 0.1f;              // ���� �ִ� �ð�


    private int currentIndex = 0;

    private float timer = 0f;




    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǹ�, Ÿ�̸Ӹ� ������Ű�� ������ ���ݸ��� ������Ʈ�� ��Ȱ��ȭ��Ŵ
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime; 

        if (timer >= blinkIntervals[currentIndex])
        {
            gameObject.SetActive(false); // ������Ʈ�� ���� ���·� �����
            Invoke("Reactivate", blinkDuration); // ���� �ð� �� �ٽ� �Ѵ� �Լ� ȣ��
            timer = 0f; // Ÿ�̸� �ʱ�ȭ

            currentIndex = (currentIndex + 1) % blinkIntervals.Length; // ���� ������ �������� ��ȯ
        }
    }

    /// <summary>
    /// ��Ȱ��ȭ�� ������Ʈ�� �ٽ� �Ѵ� �Լ�
    /// </summary>
    void Reactivate()
    {
        gameObject.SetActive(true); // ������Ʈ�� �ٽ� Ȱ��ȭ
    }

}
