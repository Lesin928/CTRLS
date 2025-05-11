using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class PauseOnSignal : MonoBehaviour, INotificationReceiver
{
    public PlayableDirector director;
    private bool isPaused;

    // Signal이 발생하면 호출
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is SignalEmitter)  // 또는 커스텀 시그널 타입
        {
            director.Pause();              // 타임라인 일시정지  
            isPaused = true;
        }
    }

    void Update()
    {
        if (isPaused && Input.GetKeyDown(KeyCode.P))
        {
            director.Resume();             // 키 입력 시 재개  
            isPaused = false;
        }
    }
}
