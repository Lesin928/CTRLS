using UnityEngine;

/// <summary>
/// 공격 버프를 지원하는 마법사 적 오브젝트입니다.
/// 일정 범위 내 아군에게 공격 버프를 제공합니다.
/// </summary>
public class AttackBuffWizardObject : EnemyObject
{
    [SerializeField] private float supportRange;    // 지원 범위
    [SerializeField] private GameObject buffPrefab; // 버프 프리팹

    #region State
    public EnemySupportState supportState { get; private set; } // 지원 상태
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 지원 상태 초기화 (버프 타입: 공격)
        supportState = new EnemySupportState(this, stateMachine, "Idle", supportRange, buffPrefab, BuffType.ATTACK);
    }

    protected override void Start()
    {
        base.Start();

        // 시작 시 지원 상태로 초기화
        stateMachine.Initialize(supportState);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDrawGizmos()
    {
        // 지원 범위 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}
