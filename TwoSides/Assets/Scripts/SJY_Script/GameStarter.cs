using UnityEngine;

// Title 씬에서 프로젝트가 실행될때 필수적인 오브젝트들을 Addressables로 로드하고 초기화하는 스크립트
public class GameStarter : MonoBehaviour
{
    private static bool initialized = false;

    private void Awake()
    {
        if (initialized) return;
        initialized = true;

        GameManager.Init();
        HUDManager.Init();
        StatUIManager.Init();
        OptionUIManager.Init();
    }
}
