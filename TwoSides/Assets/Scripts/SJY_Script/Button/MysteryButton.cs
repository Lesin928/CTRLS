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
        //map.SetActive(false); //로딩이끝났을때로 걸면 가능할듯
    }

    private IEnumerator PlayerAndLoad()
    {
        yield return new WaitForSeconds(clickSound.length);
        map.SetActive(false); //로딩이끝났을때로 걸면 가능할듯
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
