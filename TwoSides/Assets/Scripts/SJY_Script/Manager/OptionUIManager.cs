using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;

public class OptionUIManager : MonoBehaviour
{
    public static OptionUIManager Instance { get; private set; }

    private static bool isInitialized = false;

    private GameObject optionUIInstance;

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
    }

    public void CloseOptionUI()
    {
        if (optionUIInstance != null)
        {
            optionUIInstance.SetActive(false);
        }
    }
}
