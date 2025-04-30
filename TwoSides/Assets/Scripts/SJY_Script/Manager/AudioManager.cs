using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource BGM;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BGM = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RestartBGM()
    {
        if (BGM != null)
        {
            BGM.Stop();
            BGM.Play(); // 다시 처음부터 재생
        }
    }

    public void ChangeBGM(string bgmName)
    {
        BGM.clip = Resources.Load<AudioClip>("AudioSource/" + bgmName);
        if (BGM.clip != null)
        {
            BGM.Play();
            BGM.loop = true;
        }
    }
}
