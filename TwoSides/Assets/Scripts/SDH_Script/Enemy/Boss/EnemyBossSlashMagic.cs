using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 적의 Slash 공격 이펙트를 처리하는 클래스입니다.
/// 애니메이션 이벤트를 통해 활성화되며, 플레이어와 충돌 시 처리 로직을 수행합니다.
/// </summary>
public class EnemyBossSlashMagic : MonoBehaviour
{
    // 애니메이션 이벤트에서 호출되어 이펙트 오브젝트를 파괴
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
