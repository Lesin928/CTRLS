using UnityEngine;

/// <summary>
/// 적에게 버프(공격력 증가, 이동 속도 증가, 회복)를 적용하는 클래스입니다.
/// 애니메이션 이벤트를 통해 버프가 적용됩니다.
/// </summary>
public class EnemyBuff : MonoBehaviour
{
    // 버프 종류 열거형
    enum BuffType { Attack, Speed, Heal }

    [SerializeField] private BuffType buffType; // 적용할 버프 타입
    [SerializeField] private float amount;      // 버프의 수치

    private EnemyObject enemy; // 버프를 받을 적 오브젝트 참조

    bool isBuffApplied = false; // 중복 적용 방지 플래그

    // 원래의 능력치 저장용 변수
    private float originalAttack;
    private float originalMoveSpeed;
    private float originalDefaultMoveSpeed;
    private float originalChaseSpeed;

    // 시작 시 EnemyObject 참조 및 원래 능력치 저장
    private void Start()
    {
        enemy = GetComponentInParent<EnemyObject>();

        if (enemy != null)
        {
            originalAttack = enemy.Attack;
            originalMoveSpeed = enemy.MoveSpeed;
            originalDefaultMoveSpeed = enemy.defaultMoveSpeed;
            originalChaseSpeed = enemy.chaseSpeed;
        }
    }

    // 애니메이션 이벤트에서 호출되어 버프를 실제로 적용
    // 중복 적용은 방지
    private void buffAnimationTrigger()
    {
        switch (buffType)
        {
            case BuffType.Attack:
                if (isBuffApplied || enemy == null) return;

                // 공격력 증가
                originalAttack = enemy.Attack;
                enemy.Attack = originalAttack + amount;
                break;

            case BuffType.Speed:
                if (isBuffApplied || enemy == null) return;

                // 이동 속도 관련 값들을 각각 증가
                originalMoveSpeed = enemy.MoveSpeed;
                enemy.MoveSpeed = originalMoveSpeed + amount;

                originalDefaultMoveSpeed = enemy.defaultMoveSpeed;
                enemy.defaultMoveSpeed = originalDefaultMoveSpeed + amount;

                originalChaseSpeed = enemy.chaseSpeed;
                enemy.chaseSpeed = originalChaseSpeed + amount;
                break;

            case BuffType.Heal:
                // 최대 체력을 초과하지 않도록 회복
                if (enemy.CurrentHp + amount > enemy.MaxHp)
                    enemy.CurrentHp = enemy.MaxHp;
                else
                    enemy.CurrentHp += amount;
                break;
        }

        isBuffApplied = true; // 버프 적용 완료 표시
    }

    private void OnDestroy()
    {
        if (!isBuffApplied || enemy == null) return;

        switch (buffType)
        {
            case BuffType.Attack:
                enemy.Attack = originalAttack;
                break;
            case BuffType.Speed:
                enemy.MoveSpeed = originalMoveSpeed;
                enemy.defaultMoveSpeed = originalDefaultMoveSpeed;
                enemy.chaseSpeed = originalChaseSpeed;
                break;
        }
    }
}
