using System.Collections.Generic;
using UnityEngine;

public class EnemySupportState : EnemyState
{
    private float supportRange;
    private LayerMask enemyLayer;
    private GameObject magicCirclePrefab;

    // 지원한 적 저장 (적 Transform -> 생성된 마법진 GameObject)
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

        // 현재 범위 안에 있는 적들 기록
        HashSet<Transform> currentEnemies = new HashSet<Transform>();
        foreach (var enemy in enemiesInRange)
        {
            if (enemy.gameObject != enemyBase.gameObject)
            {
                currentEnemies.Add(enemy.transform);

                // 아직 지원하지 않은 적이면 지원
                if (!supportedEnemies.ContainsKey(enemy.transform))
                {
                    Support(enemy.transform);
                }
            }
        }

        // 현재 범위에서 벗어난 적 찾기
        List<Transform> enemiesToRemove = new List<Transform>();
        foreach (var supported in supportedEnemies)
        {
            if (!currentEnemies.Contains(supported.Key))
            {
                RemoveSupport(supported.Key);
                enemiesToRemove.Add(supported.Key);
            }
        }

        // 리스트에서 제거
        foreach (var enemy in enemiesToRemove)
        {
            supportedEnemies.Remove(enemy);
        }
    }

    void Support(Transform targetEnemy)
    {
        Debug.Log($"{targetEnemy.name} 을(를) 지원합니다.");
        if (magicCirclePrefab != null)
        {
            GameObject magicCircle = Object.Instantiate(magicCirclePrefab, targetEnemy.position, Quaternion.identity);
            magicCircle.transform.SetParent(targetEnemy);

            // 콜라이더 크기에 맞게 마법진 크기 조정
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
        Debug.Log($"{targetEnemy.name} 지원 해제");
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

        // 상태 종료할 때 생성했던 마법진 정리
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
