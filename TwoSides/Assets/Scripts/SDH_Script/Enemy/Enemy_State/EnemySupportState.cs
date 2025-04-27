using System.Collections.Generic;
using UnityEngine;

public class EnemySupportState : EnemyState
{
    private float supportRange;
    private LayerMask enemyLayer;
    private GameObject magicCirclePrefab;

    // ������ �� ���� (�� Transform -> ������ ������ GameObject)
    private Dictionary<Transform, GameObject> supportedEnemies = new Dictionary<Transform, GameObject>();

    public EnemySupportState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName, float supportRange, GameObject magicCirclePrefab)
        : base(enemyBase, stateMachine, animBoolName)
    {
        this.supportRange = supportRange;
        this.magicCirclePrefab = magicCirclePrefab;
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    public override void Enter()
    {
        base.Enter();
        supportedEnemies.Clear();
    }

    public override void Update()
    {
        Debug.Log("Support");
        base.Update();

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(enemyBase.transform.position, supportRange, enemyLayer);

        // ���� ���� �ȿ� �ִ� ���� ���
        HashSet<Transform> currentEnemies = new HashSet<Transform>();
        foreach (var enemy in enemiesInRange)
        {
            if (enemy.gameObject != enemyBase.gameObject)
            {
                currentEnemies.Add(enemy.transform);

                // ���� �������� ���� ���̸� ����
                if (!supportedEnemies.ContainsKey(enemy.transform))
                {
                    Support(enemy.transform);
                }
            }
        }

        // ���� �������� ��� �� ã��
        List<Transform> enemiesToRemove = new List<Transform>();
        foreach (var supported in supportedEnemies)
        {
            if (!currentEnemies.Contains(supported.Key))
            {
                RemoveSupport(supported.Key);
                enemiesToRemove.Add(supported.Key);
            }
        }

        // ����Ʈ���� ����
        foreach (var enemy in enemiesToRemove)
        {
            supportedEnemies.Remove(enemy);
        }
    }

    void Support(Transform targetEnemy)
    {
        Debug.Log($"{targetEnemy.name} ��(��) �����մϴ�.");
        if (magicCirclePrefab != null)
        {
            GameObject magicCircle = Object.Instantiate(magicCirclePrefab, targetEnemy.position, Quaternion.identity);
            magicCircle.transform.SetParent(targetEnemy);

            // �ݶ��̴� ũ�⿡ �°� ������ ũ�� ����
            Collider2D collider = targetEnemy.GetComponent<Collider2D>();
            if (collider != null)
            {
                float targetScale = collider.bounds.size.y * 1.3f;
                magicCircle.transform.localScale = new Vector3(targetScale, targetScale, 1f);
            }

            supportedEnemies.Add(targetEnemy, magicCircle);
        }
    }

    void RemoveSupport(Transform targetEnemy)
    {
        Debug.Log($"{targetEnemy.name} ���� ����");
        if (supportedEnemies.TryGetValue(targetEnemy, out GameObject magicCircle))
        {
            if (magicCircle != null)
            {
                Object.Destroy(magicCircle);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        // ���� ������ �� �����ߴ� ������ ����
        foreach (var supported in supportedEnemies)
        {
            if (supported.Value != null)
            {
                Object.Destroy(supported.Value);
            }
        }
        supportedEnemies.Clear();
    }
}
