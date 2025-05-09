using UnityEngine;
using System.Collections;

public class ParryAnimPlag : MonoBehaviour
{
    public ParryZoom parryZoom;

    [Header("�и� ���� ����")]
    public float slowTimeScale = 0.01f; // ������ ����
    public float slowDuration = 0.2f;  // ���� ���� ���� �ð�
    public float restoreDuration = 0.4f; // ���� �ӵ��� �����Ǵ� �ð�
    private bool isSlowing = false;
    public PlayerObject playerObject { get; private set; }
    private void Awake()
    {
        playerObject = GetComponentInParent<PlayerObject>();
    } 

    public void TriggerParryEffect()
    {
        if (!playerObject.parryCollider.activeSelf)
        {
            playerObject.parryCollider.SetActive(true);
        } 

        if (!isSlowing)
        {
            parryZoom.OnParry(); //ī�޶� Ȯ��
            StartCoroutine(SlowMotionCoroutine());//�������� ����
        }
    }

    public void ParryFinished()
    {
        if (playerObject.parryCollider.activeSelf)
        {
            playerObject.parryCollider.SetActive(false);
        }
        playerObject.IsCanParry = false; //�и� �Ұ�
    }

    private IEnumerator SlowMotionCoroutine()
    {
        isSlowing = true;

        float originalTimeScale = Time.timeScale;
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(slowDuration);

        float elapsed = 0f;
        while (elapsed < restoreDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(slowTimeScale, originalTimeScale, elapsed / restoreDuration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            yield return null;
        }

        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = 0.02f;
        isSlowing = false;
    }

}
