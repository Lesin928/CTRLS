//using UnityEngine;

//public class Needle : MonoBehaviour
//{
//    [SerializeField] private int damage = 1; // �ν����Ϳ��� ���� ������ ������ ��ġ

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        // Player �±׿� �浹�� ���
//        if (collision.CompareTag("Player"))
//        {
//            // �÷��̾�� TakeDamage �Լ��� ���� ��� ȣ��
//            var player = collision.GetComponent<PlayerState>(); // ��: PlayerHealth ��ũ��Ʈ�� TakeDamage(int) ����

//            if (player != null)
//            {
//                player.TakeDamage(damage);
//            }
//        }
//    }
//}
