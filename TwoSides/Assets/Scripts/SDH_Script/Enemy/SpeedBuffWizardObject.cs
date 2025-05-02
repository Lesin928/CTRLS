using UnityEngine;

/// <summary>
/// 방어력 버프를 부여하는 마법사 적 오브젝트 클래스입니다.
/// EnemyObject를 상속하며, 지원 범위 내 아군에게 방어력 버프를 부여하는 Support 상태를 가집니다.
/// </summary>
public class SpeedBuffWizardObject : EnemyObject
{
    [SerializeField] private float supportRange;    // 지원 범위
    [SerializeField] private GameObject buffPrefab; // 버프 프리팹

    #region State
    public EnemySupportState supportState { get; private set; } // 지원 상태
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 지원 상태 인스턴스 초기화 (방어력 버프 타입)
        supportState = new EnemySupportState(this, stateMachine, "Idle", supportRange, buffPrefab, BuffType.DEFENSE);
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

    /// <summary>
    /// Hit -> Support
    /// </summary>
    public override void ExitPlayerDetection()
    {
        stateMachine.ChangeState(supportState);
    }

    protected override void OnDrawGizmos()
    {
        // 지원 범위 시각화
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}
