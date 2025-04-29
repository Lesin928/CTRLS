using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionUIManager : MonoBehaviour
{
    public static OptionUIManager Instance { get; private set; }

    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private GameObject optionUIInstance;
    private static bool isInitialized = false;

    public static void Init()
    {
        if (isInitialized) return;
        isInitialized = true;

        GameObject go = new GameObject("OptionUIManager");
        DontDestroyOnLoad(go);
        Instance = go.AddComponent<OptionUIManager>();

        Instance.StartCoroutine(Instance.LoadOptionUI());
    }

    private IEnumerator LoadOptionUI()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("OptionUI");
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            optionUIInstance = Instantiate(handle.Result);
            optionUIInstance.SetActive(false);
            DontDestroyOnLoad(optionUIInstance);
        }
        else
        {
            Debug.LogError("OptionUI �ε� ����!");
        }
    }

    public void ToggleOptionUI()
    {
        if (optionUIInstance == null)
        {
            Debug.LogWarning("Option UI�� ���� �ε����� �ʾҽ��ϴ�.");
            return;
        }

        optionUIInstance.SetActive(!optionUIInstance.activeSelf);
        Debug.Log("Option UI ��۵�: " + optionUIInstance.activeSelf);
    }

    public void CloseOptionUI()
    {
        if (optionUIInstance != null)
        {
            optionUIInstance.SetActive(false);
        }
    }

    public void VolumeControl()
    {
        float volume = volumeSlider.value;

        if (volume == -40f)
            audioMixer.SetFloat("BGM", -80f);
        else
            audioMixer.SetFloat("BGM", volume);
    }
}
