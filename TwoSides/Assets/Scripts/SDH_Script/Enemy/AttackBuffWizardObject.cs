using UnityEngine;

/// <summary>
/// ���� ������ �����ϴ� ������ �� ������Ʈ�Դϴ�.
/// ���� ���� �� �Ʊ����� ���� ������ �����մϴ�.
/// </summary>
public class AttackBuffWizardObject : EnemyObject
{
    [SerializeField] private float supportRange;    // ���� ����
    [SerializeField] private GameObject buffPrefab; // ���� ������

    #region State
    public EnemySupportState supportState { get; private set; } // ���� ����
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // ���� ���� �ʱ�ȭ (���� Ÿ��: ����)
        supportState = new EnemySupportState(this, stateMachine, "Idle", supportRange, buffPrefab, BuffType.ATTACK);
    }

    protected override void Start()
    {
        base.Start();

        // ���� �� ���� ���·� �ʱ�ȭ
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

    // ���� ���� �ð�ȭ
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}
