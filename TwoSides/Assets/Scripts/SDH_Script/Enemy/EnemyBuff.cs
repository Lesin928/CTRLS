using UnityEngine;

/// <summary>
/// ������ ����(���ݷ� ����, �̵� �ӵ� ����, ȸ��)�� �����ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� ������ ����˴ϴ�.
/// </summary>
public class EnemyBuff : MonoBehaviour
{
    // ���� ���� ������
    enum BuffType { Attack, Speed, Heal }

    [SerializeField] private BuffType buffType; // ������ ���� Ÿ��
    [SerializeField] private float amount;      // ������ ��ġ

    private EnemyObject enemy; // ������ ���� �� ������Ʈ ����

    bool isBuffApplied = false; // �ߺ� ���� ���� �÷���

    // ������ �ɷ�ġ ����� ����
    private float originalAttack;
    private float originalMoveSpeed;
    private float originalDefaultMoveSpeed;
    private float originalChaseSpeed;

    // ���� �� EnemyObject ���� �� ���� �ɷ�ġ ����
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

    // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� ������ ������ ����
    // �ߺ� ������ ����
    private void buffAnimationTrigger()
    {
        switch (buffType)
        {
            case BuffType.Attack:
                if (isBuffApplied || enemy == null) return;

                // ���ݷ� ����
                originalAttack = enemy.Attack;
                enemy.Attack = originalAttack + amount;
                break;

            case BuffType.Speed:
                if (isBuffApplied || enemy == null) return;

                // �̵� �ӵ� ���� ������ ���� ����
                originalMoveSpeed = enemy.MoveSpeed;
                enemy.MoveSpeed = originalMoveSpeed + amount;

                originalDefaultMoveSpeed = enemy.defaultMoveSpeed;
                enemy.defaultMoveSpeed = originalDefaultMoveSpeed + amount;

                originalChaseSpeed = enemy.chaseSpeed;
                enemy.chaseSpeed = originalChaseSpeed + amount;
                break;

            case BuffType.Heal:
                // �ִ� ü���� �ʰ����� �ʵ��� ȸ��
                if (enemy.CurrentHp + amount > enemy.MaxHp)
                    enemy.CurrentHp = enemy.MaxHp;
                else
                    enemy.CurrentHp += amount;
                break;
        }

        isBuffApplied = true; // ���� ���� �Ϸ� ǥ��
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
