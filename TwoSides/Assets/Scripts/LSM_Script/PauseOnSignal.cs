using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseOnSignal : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    // 어떤 신호를 받고 있는지 추적
    private bool isPaused = false;
    private SignalType currentSignal;

    // 이미 처리된(첫 호출이 지난) 시그널 기록
    private HashSet<SignalType> handledSignals = new HashSet<SignalType>();

    // Signal마다 대응되는 키맵
    private static readonly Dictionary<SignalType, KeyCode> signalKeyMap = new Dictionary<SignalType, KeyCode>
    {
        { SignalType.Z, KeyCode.Z },
        { SignalType.X, KeyCode.X },
        { SignalType.C, KeyCode.C },
        { SignalType.Space, KeyCode.Space },
        { SignalType.RightArrow, KeyCode.RightArrow },
    };

    // Signal 타입 정의
    public enum SignalType
    {
        Z,
        X,
        C,
        Space,
        RightArrow
    } 

    // Signal Receiver에서 바인딩할 메서드들
    public void OnSignalZ() => HandleSignal(SignalType.Z);
    public void OnSignalX() => HandleSignal(SignalType.X);
    public void OnSignalC() => HandleSignal(SignalType.C);
    public void OnSignalSpace() => HandleSignal(SignalType.Space);
    public void OnSignalRight() => HandleSignal(SignalType.RightArrow);          

    private void HandleSignal(SignalType signal)
    {
        // 첫 호출일 때만 멈춤
        if (!handledSignals.Contains(signal) && director != null)
        {
            var root = director.playableGraph.GetRootPlayable(0);
            // 속도를 0으로 만들어 그래프 평가는 멈추지 않고, 현재 프레임을 유지
            root.SetSpeed(0);
            // 한번 강제 Evaluate 해서 현재 상태를 즉시 반영
            director.Evaluate();

            isPaused = true;
            currentSignal = signal;
            handledSignals.Add(signal);
            Debug.Log($"Signal {signal} received: Timeline frozen. Press '{signalKeyMap[signal]}' to continue.");
        }
    }

    private void Update()
    {
        // 백스페이스 입력 받으면 게임 시작
        if (Input.GetKeyDown(KeyCode.Backspace) && director != null)
        {
            GameManager.Instance.isTutoSkip = true; // 튜토리얼 스킵
            GameManager.Instance.StartNewGame();
        }

        if (isPaused && director != null)
        {
            var desiredKey = signalKeyMap[currentSignal];
            if (Input.GetKeyDown(desiredKey))
            {
                // 속도 복원(1 = 정상 속도)
                director.playableGraph.GetRootPlayable(0).SetSpeed(1);
                isPaused = false;
                Debug.Log($"'{desiredKey}' pressed: Timeline resumed.");
            }
        }
    }

    private void Awake()
    {
        // director 미할당 시 같은 GameObject에서 자동 할당
        if (director == null)
            director = GetComponent<PlayableDirector>();
    }
}
