using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mapbutton : MonoBehaviour
{
    public GameObject map;
    public float animationDuration = 0.3f;
    public static Mapbutton Instance;

    public bool isVisible = false;
    private Coroutine animationCoroutine;
    public Button aButton; // 인스펙터에서 연결
    public bool activeButton = false;

    [Header("사운드")]
    public AudioSource audioSource; // 인스펙터에서 연결
    public AudioClip openSound;     // 맵 열릴 때
    public AudioClip closeSound;    // 맵 닫힐 때

    private void Awake()
    {
        if (Instance != null && Instance != this)
            return;

        Instance = this;
    }

    private void Start()
    {
        if (map == null) return;

        isVisible = map.activeSelf;
        RectTransform rect = map.GetComponent<RectTransform>();
        rect.localScale = isVisible ? Vector3.one : new Vector3(0f, 1f, 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapsetting();
        }

        GameClearAutoButton();
    }

    public void GameClearAutoButton()
    {
        /*activeButton = true;
        GameManager.Instance.isClear = true;*/
        if (GameManager.Instance.isClear && activeButton && !map.activeSelf)
        {
            aButton.onClick.Invoke();
            activeButton = false;
        }
    }
    public void onclickivoke()
    {

        aButton.onClick.Invoke();
    }
    public void mapsetting()
    {
        if (map == null) return;

        if (!map.activeSelf)
        {
            isVisible = false;
            map.SetActive(true);
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
        if (audioSource != null && openSound != null)
            audioSource.PlayOneShot(openSound); // 열리는 소리 재생

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
        if (audioSource != null && closeSound != null)
            audioSource.PlayOneShot(closeSound); // 닫히는 소리 재생

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
        map.SetActive(false);
    }

    public void HideMapNoAnim()
    {
        map.SetActive(false);
    }
}