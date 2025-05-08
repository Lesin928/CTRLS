using UnityEngine;

/// <summary>
/// 보스의 스매쉬 공격과 관련된 트리거를 담당하는 클래스입니다.
/// 스매쉬 공격이 발생할 때, 공격의 연출 및 후속 처리를 수행합니다.
/// </summary>
public class EnemyBossSmashAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject smashPrefab; // 스매쉬 공격에 사용할 프리팹
    [SerializeField] private Transform firePoint;    // 스매쉬 공격 발사 지점

    private GameObject smash; // 스매쉬 공격 생성된 객체

    /// <summary>
    /// 스매쉬 공격을 활성화시키는 트리거 함수 (애니메이션 이벤트에서 호출)
    private void SmashAttackTrigger()
    {
        // 공격을 수행하는 적 객체를 가져옴
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // 스매쉬 공격을 발사 지점에서 생성
        smash = Instantiate(smashPrefab, firePoint.position, Quaternion.identity);

        // 스매쉬 공격 객체의 스크립트를 가져와 설정
        EnemyBossSmash smashScript = smash.GetComponent<EnemyBossSmash>();
        smashScript.SetAttacker(enemy); // 공격자의 정보 설정
        smashScript.SetDirection(enemy.facingDir); // 공격 방향 설정
    }

    // 스매쉬 공격 객체를 파괴하는 함수 (애니메이션 이벤트에서 호출)
    private void SmashDestroyTrigger()
    {
        if (smash != null)
        {
            Destroy(smash); // 스매쉬 객체 파괴
            smash = null; // 스매쉬 변수 초기화
        }
    }

    // 보스의 콜라이더를 비활성화하는 함수 (점프 애니메이션일 때 비활성화)
    private void Collider2DOff()
    {
        GetComponentInParent<Collider2D>().enabled = false; // 콜라이더 비활성화
    }

    // 보스의 콜라이더를 활성화하는 함수
    private void Collider2DOn()
    {
        GetComponentInParent<Collider2D>().enabled = true; // 콜라이더 활성화
    }

    // 애니메이션이 완료되었을 때 호출되는 메서드 (애니메이션 이벤트에서 호출)
    private void SmashAnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
