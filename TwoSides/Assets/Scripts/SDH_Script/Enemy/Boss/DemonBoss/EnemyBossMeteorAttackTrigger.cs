using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 적의 Meteor 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 공격 범위 내에 플레이어가 있으면 공격 성공 메시지를 출력합니다.
/// </summary>
public class EnemyBossMeteorAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject meteorPrefab; // Meteor 프리팹
    [SerializeField] private Transform firePoint;     // 공격 발사 위치

    /// <summary>
    /// 실제 Meteor를 발사하는 트리거입니다.
    /// 애니메이션 이벤트로 호출됩니다.
    /// </summary>
    private void MeteorAttackTrigger()
    {
        // 부모 EnemyObject를 가져옴
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Meteor 프리팹을 발사 지점에 생성
        GameObject meteor = Instantiate(meteorPrefab, firePoint.position, Quaternion.identity);

        // Meteor 객체의 스크립트를 가져와서 활성화
        EnemyBossMeteor meteorScript = meteor.GetComponent<EnemyBossMeteor>();
        meteorScript.SetAttacker(enemy); // 발사자 전달
        meteorScript.SetDirection(enemy.facingDir);
    }

    private void MeteorAnimationTrigger()
    {
        // 적의 애니메이션이 완료되었음을 알리는 메서드를 호출
        enemy.AnimationFinishTrigger();
    }
}
