using UnityEngine;

/// <summary>
/// 적의 Smash 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 공격 범위 내에 플레이어가 있으면 공격 성공 메시지를 출력합니다.
/// </summary>
public class EnemyBossSmashAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject smashPrefab; // Smash 프리팹
    [SerializeField] private Transform firePoint;    // 공격 발사 위치

    private GameObject smash; // 생성된 smash 오브젝트 저장

    /// <summary>
    /// Smash를 활성화하는 트리거입니다.
    /// </summary>
    private void SmashAttackTrigger()
    {
        // 부모 EnemyObject를 가져옴
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Smash 프리팹을 발사 지점에 생성
        smash = Instantiate(smashPrefab, firePoint.position, Quaternion.identity);

        // Slash 객체의 스크립트를 가져와서 활성화
        EnemyBossSmash smashScript = smash.GetComponent<EnemyBossSmash>();
        smashScript.SetAttacker(enemy); // 발사자 전달
        smashScript.SetDirection(enemy.facingDir);
    }

    private void SmashDestroyTrigger()
    {
        if (smash != null)
        {
            Destroy(smash); // 애니메이션 끝날 때 삭제
            smash = null;
        }
    }

    private void Collider2DOff()
    {
        GetComponentInParent<Collider2D>().enabled = false;
    }

    private void Collider2DOn()
    {
        GetComponentInParent<Collider2D>().enabled = true;
    }

    private void SmashAnimationTrigger()
    {
        // 적의 애니메이션이 완료되었음을 알리는 메서드를 호출
        enemy.AnimationFinishTrigger();
    }
}
