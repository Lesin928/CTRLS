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

        /*
        GameObject buff = Object.Instantiate(buffPrefab, transform.position + new Vector3(0f, 0.35f, 0f), Quaternion.identity);
        buff.transform.SetParent(transform);

        // ����� �ݶ��̴��� ���� ������ ũ�� ����
        Collider2D collider = rb.GetComponent<Collider2D>();
        if (collider != null)
        {
            float targetScale = collider.bounds.size.y * 1.1f;
            buff.transform.localScale = new Vector3(targetScale, targetScale, 1f);
        }
        */

        // ���� �� ���� ���·� �ʱ�ȭ
        stateMachine.Initialize(supportState);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDrawGizmos()
    {
        // ���� ���� �ð�ȭ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}
