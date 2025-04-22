using UnityEngine;

// TODO: 점프 기능 추가 필요 (추가할일 적는부분)
// FIXME: 공격 범위 버그 수정 예정 (고칠거 적는부분)
// NOTE : 공격 방식 리펙토링, 직접 스탯 수정에서 함수 호출로 (기타 작성)

//모든 스크립트는 아래와 같이 <summary>로 클래스에 대한 설명을 작성함
/// <summary>
/// 스크립트들의 기본 구조를 작성한 클래스.
/// </summary>
public class LSM_NAMEING_RULE : MonoBehaviour
{
    //public 변수 먼저 선언
    public float moveSpeed = 5f;

    //private 변수는 아래
    private Rigidbody2D rb;

    //초기화 함수
    void Awake() { }

    void Start() { }

    //업데이트 함수
    void Update() { }

    void FixedUpdate() { }

    //사용자 정의 메서드 + 주석
    /// <summary>
    /// 플레이어가 목표 지점으로 이동합니다.
    /// </summary>
    private void Move() { }
}