using UnityEngine;

/// <summary>
/// SpeedBuffWizardObject는 특정 범위 내에서 플레이어에게 속도 버프를 부여하는 적 객체입니다.
/// 이 객체는 Support 상태를 통해 속도 버프를 발동시키며, 일정 범위 내의 플레이어를 감지하고 버프를 제공합니다.
/// </summary>
public class SpeedBuffWizardObject : EnemyObject
{
    [SerializeField] private float supportRange;    // 버프를 주는 범위
    [SerializeField] private GameObject buffPrefab; // 버프 오브젝트 프리팹

    #region State
    public EnemySupportState supportState { get; private set; } // 지원 상태 (버프 상태)
    #endregion

    // 초기화 작업을 수행
    protected override void Awake()
    {
        base.Awake();

        // 지원 상태를 초기화하고, 상태 머신에 전달하여 'Idle' 상태에서 시작합니다.
        supportState = new EnemySupportState(this, stateMachine, "Idle", supportRange, buffPrefab, BuffType.SPEED);
    }

    // 객체의 시작 시 호출되며, 상태 머신을 초기화
    protected override void Start()
    {
        base.Start();

        // 상태 머신을 사용하여 지원 상태를 초기화합니다.
        stateMachine.Initialize(supportState);
    }

    // 매 프레임마다 호출
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 플레이어가 감지 영역을 벗어났을 때 호출됩니다.
    /// </summary>
    public override void ExitPlayerDetection()
    {
        // 지원 상태로 상태를 변경하여 버프를 활성화합니다.
        stateMachine.ChangeState(supportState);
    }

    // 지원 범위를 시각적으로 표시
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan; // 시각적 표시 색상 설정
        Gizmos.DrawWireSphere(transform.position, supportRange); // 지원 범위 원을 그립니다.
    }
}
