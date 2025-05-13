using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using Unity.Collections;

// 씬 로드를 관리하는 스크립트트
public class LoadingSceneController : MonoBehaviour
{
    private static LoadingSceneController instance;

    [SerializeField]
    private CanvasGroup cg;
    [SerializeField]
    private RectTransform Spinner;
    private string loadSceneName;

    public static LoadingSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindAnyObjectByType<LoadingSceneController>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }
    }

    private static LoadingSceneController Create()
    {
        return Instantiate(Resources.Load<LoadingSceneController>("Prefabs/LoadingUI"));
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);

        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        yield return StartCoroutine(Fade(true));

        // 비동기로 씬을 로딩하는 함수
        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
        // 씬 로딩이 완료되어도 로딩장면을 유지하기 위해 자동으로 씬이 전환되지 않게 설정
        op.allowSceneActivation = false;

        // 로딩이 완료될때까지 대기
        while (op.progress < 0.9f)
        {
            yield return null;
        }

        // 로딩이 완료된 후 로딩장면을 1.5초간 보여줌
        yield return new WaitForSecondsRealtime(1.5f);

        op.allowSceneActivation = true;
    }

    // 씬이 로드되자마자 가장 먼저 호출되는 함수수
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == loadSceneName)
        {
            if (GameManager.Instance != null && loadSceneName != "Title"
                && loadSceneName != "GameClear" && loadSceneName != "GameOver"
                && loadSceneName != "TimeLine")
            {
                //InitStageData에서 Stage마다 몇마리의 적이 있는지 설정하고 플레이어 스폰 위치 설정
                GameManager.Instance.InitStageData(loadSceneName);
            }

            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;

            // isClear 초기화화
            Debug.Log(GameManager.Instance.isClear);
            if (GameManager.Instance.isClear)
            {
                GameManager.Instance.isClear = false;
            }

            if (loadSceneName == "Puzzle0")
            {
                GameManager.Instance.isClear = true;
            }

            if (HUDManager.Instance != null)
                HUDManager.Instance.ShowHUD();
        }
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;

        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 3f;
            cg.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }
}
