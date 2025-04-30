using UnityEngine;

/// <summary>
/// 레이저 공격과 함께 마법진 이펙트를 나타내는 클래스입니다.
/// 애니메이션 이벤트를 통해 일정 시점에 파괴됩니다.
/// </summary>
public class LaserActive : MonoBehaviour
{
    // 애니메이션 이벤트에서 호출되어 오브젝트를 파괴
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
