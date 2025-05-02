using UnityEngine;

public class EnemyBuff : MonoBehaviour
{
    enum BuffType { Attack, Speed, Heal }

    [SerializeField] BuffType buffType;
    [SerializeField] float amount; // ¹öÇÁ ¾ç

    private EnemyObject enemy;

    bool isBuffApplied = false;

    float originalAttack;
    float originalMoveSpeed;
    float originalDefaultMoveSpeed;
    float originalChaseSpeed;

    void Start()
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

    private void buffAnimationTrigger()
    {
        switch (buffType)
        {
            case BuffType.Attack:
                if (isBuffApplied || enemy == null) return;
                originalAttack = enemy.Attack;
                enemy.Attack = originalAttack + amount;
                break;
            case BuffType.Speed:
                if (isBuffApplied || enemy == null) return;
                originalMoveSpeed = enemy.MoveSpeed;
                enemy.MoveSpeed = originalMoveSpeed + amount;
                originalDefaultMoveSpeed = enemy.defaultMoveSpeed;
                enemy.defaultMoveSpeed = originalDefaultMoveSpeed + amount;
                originalChaseSpeed = enemy.chaseSpeed;
                enemy.chaseSpeed = originalChaseSpeed + amount;
                break;
            case BuffType.Heal:
                if (enemy.CurrentHp + amount > enemy.MaxHp)
                    enemy.CurrentHp = enemy.MaxHp;
                else
                    enemy.CurrentHp += amount;
                break;
        }

        isBuffApplied = true;
    }
}
