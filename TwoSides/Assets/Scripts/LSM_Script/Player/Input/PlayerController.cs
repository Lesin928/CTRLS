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
        //플레이어가 바닥에 있고 대쉬중이 아닐 떄
        if(!!playerObject.GetDashing() && playerObject.IsGroundDetected())
        {
            //플레이어가 멈추면 idle, 이동중이면 move
            if (moveInput.x == 0)
            {
                playerAnimation.stateMachine.ChangeState(playerAnimation.idleState);
            }
            else
            {
                playerAnimation.stateMachine.ChangeState(playerAnimation.moveState);
            }
        }  
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move");
        //플레이어가 땅에 있으면 이동상태로 전환
        if (playerObject.IsGroundDetected())
        {
            playerAnimation.stateMachine.ChangeState(playerAnimation.moveState);
        }            
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        playerAnimation.stateMachine.ChangeState(playerAnimation.jumpState); 
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("Dash");
        playerAnimation.stateMachine.ChangeState(playerAnimation.dashState);
        if (!playerObject.GetDashing())
        {
            StartCoroutine(Dash());
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack! (X 버튼)");
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
        playerObject.SetDashing(true);
        rb.linearVelocity = new Vector2( playerAnimation.Getfacing() * dashForce, 0f);
        yield return new WaitForSeconds(0.2f);
        playerObject.SetDashing(false);
    } 
}
