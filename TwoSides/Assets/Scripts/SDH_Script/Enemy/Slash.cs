using UnityEngine;

/// <summary>
/// ���� Slash ���� ����Ʈ�� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� Ȱ��ȭ�Ǹ�, �÷��̾�� �浹 �� ó�� ������ �����մϴ�.
/// </summary>
public class Slash : MonoBehaviour
{
    /// <summary>
    /// �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ�, ���� �ٶ󺸴� ���⿡ ���� ����Ʈ ������ �����մϴ�.
    /// </summary>
    /// <param name="facingDir">���� �ٶ󺸴� ���� (1 �Ǵ� -1)</param>
    public void Active(int facingDir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir;
        transform.localScale = scale;
    }

    // �÷��̾�� �浹 �� ȣ��
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerObject player = collision.GetComponent<PlayerObject>();
            if (player != null)
            {
                Debug.Log("���� ����");
            }
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� ����Ʈ ������Ʈ�� �ı�
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
