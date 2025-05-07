//using UnityEngine;

//public class Needle : MonoBehaviour
//{
//    [SerializeField] private int damage = 1; // 인스펙터에서 설정 가능한 데미지 수치

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        // Player 태그와 충돌한 경우
//        if (collision.CompareTag("Player"))
//        {
//            // 플레이어에게 TakeDamage 함수가 있을 경우 호출
//            var player = collision.GetComponent<PlayerState>(); // 예: PlayerHealth 스크립트에 TakeDamage(int) 존재

//            if (player != null)
//            {
//                player.TakeDamage(damage);
//            }
//        }
//    }
//}
