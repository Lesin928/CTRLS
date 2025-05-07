using UnityEngine;
using System.Collections;
// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾� �ǰ� �ִϸ��̼��� �����ϴ� Ŭ����
/// </summary>
public class HitAnim : MonoBehaviour
{ 
    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private int flashCount = 4; 
    [SerializeField] private float maxDuration = 0.2f;
    [SerializeField] private float minDuration = 0.05f;

    /// <summary>
    /// �ǰ� �ִϸ��̼��� �����ϴ� �޼���
    /// </summary>
    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    /// <summary>
    /// �÷��� ȿ���� �����ϴ� �ڷ�ƾ
    /// ���� �����ϴ� �������� ��������Ʈ �������� Ȱ��ȭ ���¸� ����
    /// �����δٴ� ��
    /// </summary>
    private IEnumerator FlashCoroutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            // ���� �پ��� ���� ��� (���� ����)
            float t = (float)i / (flashCount - 1);  
            float duration = Mathf.Lerp(maxDuration, minDuration, t);

            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(duration);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(duration);
        }
    }
}
