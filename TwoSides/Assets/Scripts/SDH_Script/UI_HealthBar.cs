using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private EnemyObject entity;
    private RectTransform myTransform;
    private Slider slider;


    private void Start()
    {
        myTransform = GetComponent<RectTransform>();

        slider = GetComponentInChildren<Slider>();

        entity = GetComponentInParent<EnemyObject>();
    }

    // Flip()�� ���� Update���� ����ǹǷ� LateUpdate()���� ó���ؾ� UI�� ������ �������� �����Ǿ� ����ϰ� ����
    private void LateUpdate()
    {
        LockUIFacing();
        UpdateHealthUI();
    }
    private void LockUIFacing()
    {
        if (entity != null)
        {
            // �θ� ������ ���� ������ �״��, ���� ���� ������ Y�ุ ������
            myTransform.localRotation = entity.facingDir == 1
                ? Quaternion.identity
                : Quaternion.Euler(0, 180, 0);
        }
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = entity.MaxHp;
        slider.value = entity.CurrentHp;
    }
}