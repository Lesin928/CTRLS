using UnityEngine;

public class DefenseWizard : EnemyObject
{
    [SerializeField] private float supportRange = 5f; // ���� ����
    [SerializeField] private GameObject magicCirclePrefab; // ������ ������
    #region State
    public EnemySupportState supportState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        supportState = new EnemySupportState(this, stateMachine, "Idle", supportRange, magicCirclePrefab);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(supportState);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}
