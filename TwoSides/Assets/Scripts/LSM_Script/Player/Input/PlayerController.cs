using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;
using UnityEngine.Rendering;
// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)
public class PlayerController : MonoBehaviour
{

    #region Components //PlayerAnimation 스크립트의 인스펙터에 있는 컴포넌트들
    protected PlayerStateMachine stateMachine;
    protected PlayerAnimation playerAnimation;
    protected PlayerObject playerObject;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    #endregion

    //임시 코드
    [Header("플레이어 조작 세팅")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float dashForce = 10f;

    #region Dillay
    private float groundIgnoreTimer = 0f;
    private float groundIgnoreDuration = 0.1f; // 딜레이 시간

    private float dashCooldownTimer = 0f;
    private float dashCooldownDuration = 1f; // 쿨타임 시간
    #endregion 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerObject = GetComponent<PlayerObject>(); 
    }
    private void FixedUpdate() //나중에 무브 스테이트로 이동
    {
        //플레이어가 대쉬중이 아니면 이동
        if (!playerObject.GetDashing())
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

            //또한 플레이어가 이동중일 때 방향 전환
            if (moveInput.x != 0)
            {
                playerAnimation.FlipController(moveInput.x);
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

        bool shouldCheckGround = groundIgnoreTimer <= 0;

        //플레이어가 바닥에 있고 대쉬중이 아닐 떄
        if (!playerObject.GetDashing() && playerObject.IsGroundDetected() && shouldCheckGround)
        {
            //플레이어 입력이 없으면 idle, 있으면 move
            if (moveInput.x == 0 )
            {
                playerAnimation.stateMachine.ChangeState(playerAnimation.idleState);
            }
            else if (moveInput.x != 0)
            {
                playerAnimation.stateMachine.ChangeState(playerAnimation.moveState);
            }
        }  
    }

    public void OnMove(InputAction.CallbackContext context)
    {  
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (playerObject.IsGroundDetected())
        {
            groundIgnoreTimer = groundIgnoreDuration;
            Debug.Log("점프");
            playerAnimation.stateMachine.ChangeState(playerAnimation.jumpState);
        }            
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!playerObject.GetDashing())
        {
            playerAnimation.stateMachine.ChangeState(playerAnimation.dashState);
            StartCoroutine(Dash());
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack! (X 버튼)");
        if (!playerObject.GetDashing() && playerObject.IsGroundDetected())
        {
            playerAnimation.stateMachine.ChangeState(playerAnimation.attackState);
        }
    }

    public void OnShift(InputAction.CallbackContext context)
    {
        Debug.Log("Shift! (Shift 버튼)");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact! (F 버튼)");
    }
    private IEnumerator Dash()
    {
        if (dashCooldownTimer > 0)
            yield break; // 대쉬 쿨타임이 남아있으면 대쉬를 하지 않음
        dashCooldownTimer = dashCooldownDuration;
        playerObject.SetDashing(true); 
        float originalGravity = rb.gravityScale; 
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(playerAnimation.Getfacing() * dashForce, 0f);
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = originalGravity;
        playerObject.SetDashing(false);
    }
}
