using System.Collections;
using UnityEngine;

public class BossButton : MonoBehaviour
{
    public GameObject map;

    public string sceneName;
    [Header("사운드")]
    public AudioSource audioSource;   // 인스펙터에서 연결
    public AudioClip clickSound;      // 클릭 시 재생할 사운드
    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // 이름 정확히 일치해야 함
    }
    public void Onclick()
    {
        sceneName = "Boss";

        GameManager.Instance.isClear = false;
        Mapbutton.Instance.activeButton = false;
        Map.Instance.doorConnected = false;

        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogError("비상사태 불러박자박사 (AudioSource or Clip 없음)");
        }
        StartCoroutine(PlayerAndLoad());
    }

    private IEnumerator PlayerAndLoad()
    {
        yield return new WaitForSeconds(clickSound.length);
        map.SetActive(false); //로딩이끝났을때로 걸면 가능할듯
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}