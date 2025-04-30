using UnityEngine;

/// <summary>
/// �� ������Ʈ�� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼��� �Ϸ�Ǹ� ���� �ൿ�� Ʈ�����մϴ�.
/// </summary>
public class EnemyAnimationTrigger : MonoBehaviour
{
    // �θ� ������Ʈ�� EnemyObject ������Ʈ�� ������
    protected EnemyObject enemy => GetComponentInParent<EnemyObject>();

    // �ִϸ��̼��� �Ϸ�Ǿ��� �� ȣ��Ǵ� �޼���
    protected virtual void AnimationTrigger()
    {
        // ���� �ִϸ��̼��� �Ϸ�Ǿ����� �˸��� �޼��带 ȣ��
        enemy.AnimationFinishTrigger();
    }
}
