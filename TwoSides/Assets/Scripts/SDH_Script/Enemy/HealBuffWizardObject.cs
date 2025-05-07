using UnityEngine;

/// <summary>
/// ȸ�� ������ �ο��ϴ� ������ �� ������Ʈ Ŭ�����Դϴ�.
/// EnemyObject�� ����ϸ�, ���� ���� �� �Ʊ����� ȸ�� ������ �ο��ϴ� Support ���¸� �����ϴ�.
/// </summary>
public class HealBuffWizardObject : EnemyObject
{
    [SerializeField] private float supportRange;    // ���� ����
    [SerializeField] private GameObject buffPrefab; // ���� ������

    #region State
    public EnemySupportState supportState { get; private set; } // ���� ����
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // ���� ���� �ν��Ͻ� �ʱ�ȭ (ȸ�� ���� Ÿ��)
        supportState = new EnemySupportState(this, stateMachine, "Idle", supportRange, buffPrefab, BuffType.HEAL);
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

    // ���� ������ �ð�ȭ
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}
