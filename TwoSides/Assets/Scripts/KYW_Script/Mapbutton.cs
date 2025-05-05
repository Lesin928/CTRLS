using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mapbutton : MonoBehaviour
{
    public GameObject map;
    public float animationDuration = 0.3f;

    private bool isVisible = false;
    private Coroutine animationCoroutine;
    public Button aButton; // 인스펙터에서 연결
    private void Start()
    {
        if (map == null) return;

        // 실제 활성화 상태를 반영해서 스케일 세팅
        isVisible = map.activeSelf;

        RectTransform rect = map.GetComponent<RectTransform>();
        rect.localScale = isVisible ? Vector3.one : new Vector3(0f, 1f, 1f);

    }
    

    void Update()
    {
        GameClearAutoButton();
    }

    private void GameClearAutoButton()
    {
        //조건이 만족되면 A 버튼 클릭
        if (GameManager.Instance.isClear)
        {
            aButton.onClick.Invoke(); //코드에서 클릭 실행
        }
    }

    public void mapsetting()
    {
        if (map == null) return;

        // map의 실제 활성화 상태 기반으로 갱신 (씬 로드 후 상태 꼬임 방지)
        if (!map.activeSelf)
        {
            isVisible = false;
            map.SetActive(true); // 반드시 켜고 애니메이션 시작
        }

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        if (isVisible)
        {
            animationCoroutine = StartCoroutine(HideMap());
        }
        else
        {
            animationCoroutine = StartCoroutine(ShowMap());
        }

        isVisible = !isVisible;
    }

    private IEnumerator ShowMap()
    {
        RectTransform rect = map.GetComponent<RectTransform>();
        float time = 0f;
        Vector3 startScale = new Vector3(0f, 1f, 1f);
        Vector3 endScale = Vector3.one;

        rect.localScale = startScale;
        yield return null;

        while (time < animationDuration)
        {
            float t = time / animationDuration;
            rect.localScale = Vector3.Lerp(startScale, endScale, t);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        rect.localScale = endScale;
    }

    private IEnumerator HideMap()
    {
        RectTransform rect = map.GetComponent<RectTransform>();
        float time = 0f;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = new Vector3(0f, 1f, 1f);

        while (time < animationDuration)
        {
            float t = time / animationDuration;
            rect.localScale = Vector3.Lerp(startScale, endScale, t);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        rect.localScale = endScale;
        map.SetActive(false); // 확실히 꺼준다
    }
}
