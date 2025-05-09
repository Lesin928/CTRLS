using UnityEngine;
using System.Collections;

public class ParryAnimPlag : MonoBehaviour
{
    public ParryZoom parryZoom;

    [Header("패링 연출 설정")]
    public float slowTimeScale = 0.01f; // 느려질 배율
    public float slowDuration = 0.2f;  // 느린 상태 지속 시간
    public float restoreDuration = 0.4f; // 원래 속도로 복원되는 시간
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
            parryZoom.OnParry(); //카메라 확대
            StartCoroutine(SlowMotionCoroutine());//느려지는 연출
        }
    }

    public void ParryFinished()
    {
        if (playerObject.parryCollider.activeSelf)
        {
            playerObject.parryCollider.SetActive(false);
        }
        playerObject.IsCanParry = false; //패링 불가
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
