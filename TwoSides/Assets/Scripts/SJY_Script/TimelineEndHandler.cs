using UnityEngine;
using UnityEngine.Playables;

// 튜토리얼 타임라인 관리하는 스크립트트
public class TimelineEndHandler : MonoBehaviour
{
    public PlayableDirector director;
    private void Awake()
    {
        if (director == null)
            director = GetComponent<PlayableDirector>();
    }

    void Start()
    {
        director.stopped += OnTimelineStopped;
    }

    // 타임라인이 끝나면 StartNewGame을 호출출
    void OnTimelineStopped(PlayableDirector pd)
    {
        if (!GameManager.Instance.isTutoSkip)
        {
            GameManager.Instance.StartNewGame();
        }        
    }

    void OnDestroy()
    {
        director.stopped -= OnTimelineStopped;
    }

}