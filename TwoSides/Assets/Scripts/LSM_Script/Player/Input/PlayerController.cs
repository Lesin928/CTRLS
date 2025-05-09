using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어의 입력을 처리하는 클래스
/// </summary>
public class PlayerController : MonoBehaviour
{
    public GameObject player;

    #region Components
    private PlayerStateMachine stateMachine;
    private PlayerAnimation playerAnimation;
    private PlayerObject playerObject;
    private Rigidbody2D rb;
    #endregion

    #region Dillay
    private float groundIgnoreTimer = 0f;
    private float groundIgnoreDuration = 0.1f; // 딜레이 시간

    private float dashCooldownTimer = 0f;
    private float dashCooldownDuration = 1f; // 쿨타임 시간

    private float skillCooldownTimer = 0f; // 스킬 쿨타임 시간    
    private float skillCooldownDuration = 3f; // 스킬 쿨타임 시간    

    private float parryCooldownTimer = 0f; // 패링 쿨타임 시간    
    private float parryCooldownDuration = 3f; // 패링 쿨타임 시간   
    #endregion 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerObject = GetComponent<PlayerObject>();
    }
    private void FixedUpdate() //나중에 무브 스테이트로 이동
    {
        if(playerObject.IsDeath) return; //사망 상태

        //대쉬 상태, 공격 상태가 아닐 때만 이동
        if (!playerObject.IsDashing && !playerObject.IsAttack && !playerObject.IsSkill)
        {
            rb.linearVelocity = new Vector2(playerObject.MoveInput.x * playerObject.MoveSpeed, rb.linearVelocity.y);

            //또한 플레이어가 이동중일 때 방향 전환
            if (playerObject.MoveInput.x != 0)
            {
                playerAnimation.FlipController(playerObject.MoveInput.x);
            }
        }
    }
    private void Update()
    {
        // 점프 상태에서 상태 전이가 바로 일어나지 않도록 딜레이
        if (groundIgnoreTimer > 0)
            groundIgnoreTimer -= Time.deltaTime;

        // 대쉬 쿨타임이 끝나지 않았으면 쿨타임 감소
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        // 스킬 쿨타임이 끝나지 않았으면 쿨타임 감소
        if (skillCooldownTimer > 0)
            skillCooldownTimer -= Time.deltaTime;

        // 패링 쿨타임이 끝나지 않았으면 쿨타임 감소
        if (parryCooldownTimer > 0)
            parryCooldownTimer -= Time.deltaTime;

        bool shouldCheckGround = groundIgnoreTimer <= 0;
    }

    /// <summary>
    /// 키보드 좌우 방향키 입력을 처리하는 메서드
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerObject.IsDeath) return; //사망 상태
        playerObject.MoveInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 키보드 C 입력을 처리하는 메서드
    /// </summary>
    public void OnJump(InputAction.CallbackContext context)
    {
        if (playerObject.IsDeath) return; //사망 상태
        if (playerObject.IsGroundDetected() && !playerObject.IsSkill && !playerObject.IsDashing)
        {
            groundIgnoreTimer = groundIgnoreDuration;
            Debug.Log("점프");
            playerAnimation.stateMachine.ChangeState(playerAnimation.jumpState);
        }
    }

    /// <summary>
    /// 키보드 Space 입력을 처리하는 메서드
    /// </summary>
    public void OnDash(InputAction.CallbackContext context)
    {
        if (playerObject.IsDeath) return; //사망 상태
        if (dashCooldownTimer > 0) return; // 대쉬 쿨타임이 남아있으면 대쉬를 하지 않음
        
        if (!playerObject.IsDashing && !playerObject.IsAttack)
        {
            dashCooldownTimer = dashCooldownDuration;
            playerAnimation.stateMachine.ChangeState(playerAnimation.dashState);
            StartCoroutine(Dash());
        }
    }

    /// <summary>
    /// 키보드 X 입력을 처리하는 메서드
    /// </summary>
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (playerObject.IsDeath) return; //사망 상태
        if (playerObject.IsSkill) return; //스킬 사용중
        if (context.phase != InputActionPhase.Performed) return;                
        if (playerObject.IsDashing && playerObject.IsEvasion && playerObject.IsCanParry)
        {
            if (parryCooldownTimer > 0) return; // 패링 쿨타임이 남아있으면 패링을 하지 않음 
            playerAnimation.stateMachine.ChangeState(playerAnimation.parryState);
            parryCooldownTimer = parryCooldownDuration;
            return;
        }        
        if (!playerObject.IsAttack && !playerObject.IsDashing)
        {
            playerAnimation.stateMachine.ChangeState(playerAnimation.attackState);
            StartCoroutine(Attack());
        }
        else if (playerObject.IsAttack && !playerObject.IsDashing)
        {
            StartCoroutine(Combo());
        }
        
    }
    /// <summary>
    /// 키보드 Shift 입력을 처리하는 메서드
    /// </summary>
    public void OnShift(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        if (playerObject.IsDeath) return; //사망 상태 
        Debug.Log("무적 모드 (Shift 버튼)");  
        if(!playerObject.Isgod)
        {
           playerObject.Isgod = true; // 무적모드
           playerObject.GOD.SetActive(true);
        }
        else if(playerObject.Isgod)
        {
            playerObject.Isgod = false; // 무적모드 해제
            playerObject.GOD.SetActive(false);
        }     
    } 
    /// <summary>
    /// 키보드 F 입력을 처리하는 메서드
    /// </summary>
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (playerObject.IsDeath) return; //사망 상태
        if (context.phase != InputActionPhase.Performed) return;        
        //만약 플레이어의 콜라이더가 Interactive를 상속한 오브젝트와 접해있을 경우
        if(playerObject.IObject != null)
        {
            playerObject.IObject.GetComponent<Interactive>().PressF();
        }
        else
        {
            Debug.Log("Interact! (F 버튼) - 대상 없음");
        }
    }
    /// <summary>
    /// 키보드 Z 입력을 처리하는 메서드
    /// </summary>
    public void OnSkill(InputAction.CallbackContext context)
    {
        if (playerObject.IsDeath) return; //사망 상태
        //프레스 입력인지 확인
        if (context.phase != InputActionPhase.Performed) return;
        if (playerObject.IsSkill) return; // 스킬 사용중이면 리턴
        if (playerObject.IsDashing) return; // 대쉬중이면 리턴
        if (skillCooldownTimer > 0) return; // 쿨타임 남아있으면 리턴
        // 만약 스킬중이 아닐경우 스킬상태
        skillCooldownTimer = skillCooldownDuration;
        playerAnimation.stateMachine.ChangeState(playerAnimation.skillState);
    } 
    private IEnumerator Dash()
    {
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(playerAnimation.Getfacing() * playerObject.DashForce, 0f);
        yield return new WaitForSeconds(0.4f);
        rb.gravityScale = originalGravity;
        playerObject.IsDashing = false;
    }
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.7f); // 콤보 2까지의 딜레이 시간
        if (!playerObject.IsCombo)
        {
            //시간 내에 콤보가 수행되지 않으면 공격 종료
            playerObject.IsAttack = false;
        }
    }
    private IEnumerator Combo()
    {
        // 만약 콤보공격이 수행되고 있는 경우 탈출
        if (playerObject.IsCombo)
        {
            yield break;
        }
        playerObject.IsCombo = true;
        yield return new WaitForSeconds(0.5f);// 다음 공격까지의 딜레이 시간, 추후 플레이어 속성에 추가, 애니메이션 실행 시간보다 낮아질 수 없음  
        playerObject.IsAttack = false;
        playerObject.IsCombo = false;
    } 
}