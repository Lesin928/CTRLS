using System.Collections;
using UnityEngine;

public class MysteryButton : MonoBehaviour
{
    public GameObject map;
    private bool[] isUsed = new bool[10];

    public string sceneName;
    [Header("사운드")]
    public AudioSource audioSource;   // 인스펙터에서 연결
    public AudioClip clickSound;      // 클릭 시 재생할 사운드

    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // 이름 정확히 일치해야 함

        isUsed[0] = true;
    }
    public void Onclick()
    {
        sceneName = "Mystery";

        // 랜덤으로 0부터 9까지의 숫자 중에서 사용되지 않은 숫자를 선택
        // MysteryButton이 DontDestroy가 아니라
        // isUsed 배열이 생성될때마다 초기화되서 사실상 랜덤이 아님...
        int rand = Random.Range(0, 10);
        while (isUsed[rand])
        {
            rand = Random.Range(0, 10);
        }
        isUsed[rand] = true;

        sceneName += rand.ToString();

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
