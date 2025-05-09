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

    // Flip()이 보통 Update에서 실행되므로 LateUpdate()에서 처리해야 UI의 방향이 마지막에 고정되어 깔끔하게 동작
    private void LateUpdate()
    {
        LockUIFacing();
        UpdateHealthUI();
    }

    private void LockUIFacing()
    {
        if (entity != null)
        {
            // 부모가 오른쪽 보고 있으면 그대로, 왼쪽 보고 있으면 Y축만 뒤집음
            myTransform.localRotation = entity.facingDir == 1
                ? Quaternion.identity
                : Quaternion.Euler(0, 180, 0);
        }
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = entity.MaxHp;
        slider.value = entity.CurrentHp;

        if (entity.CurrentHp <= 0)
            gameObject.SetActive(false);
    }
}