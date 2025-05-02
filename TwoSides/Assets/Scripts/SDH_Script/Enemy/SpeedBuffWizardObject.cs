using UnityEngine;

/// <summary>
/// ���� ������ �ο��ϴ� ������ �� ������Ʈ Ŭ�����Դϴ�.
/// EnemyObject�� ����ϸ�, ���� ���� �� �Ʊ����� ���� ������ �ο��ϴ� Support ���¸� �����ϴ�.
/// </summary>
public class SpeedBuffWizardObject : EnemyObject
{
    [SerializeField] private float supportRange;    // ���� ����
    [SerializeField] private GameObject buffPrefab; // ���� ������

    #region State
    public EnemySupportState supportState { get; private set; } // ���� ����
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // ���� ���� �ν��Ͻ� �ʱ�ȭ (���� ���� Ÿ��)
        supportState = new EnemySupportState(this, stateMachine, "Idle", supportRange, buffPrefab, BuffType.DEFENSE);
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

    protected override void OnDrawGizmos()
    {
        // ���� ���� �ð�ȭ
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}
