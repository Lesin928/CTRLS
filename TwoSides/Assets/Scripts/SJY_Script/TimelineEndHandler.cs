using UnityEngine;
using UnityEngine.Playables;

public class TimelineEndHandler : MonoBehaviour
{
    public PlayableDirector director;

    void Start()
    {
        director.stopped += OnTimelineStopped;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && director != null)
        {
            // 타임라인이 재생 중일 때만 처리
            if (director.state == PlayState.Playing)
            {
                double endTime = director.playableAsset.duration;
                director.time = endTime;
                director.Evaluate();  // 즉시 상태 반영
                director.Stop();      // 종료 이벤트(stopped) 호출
            }
        }
    }

    void OnTimelineStopped(PlayableDirector pd)
    {
        GameManager.Instance.StartNewGame();
    }

    void OnDestroy()
    {
        director.stopped -= OnTimelineStopped;
    }
}