using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;
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
    private bool isDashing; 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerObject = GetComponent<PlayerObject>(); 
    }
    private void FixedUpdate() //나중에 무브 스테이트로 이동
    {
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }

        if(moveInput.x != 0)        
        {
            playerAnimation.FlipController(moveInput.x);
        }        
    }

    private void Update() //나중에 무브 스테이트로 이동
    {
        //플레이어가 움직이지 않고 바닥에 있으면 대기상태로 전환 
        if (moveInput.x == 0 && playerObject.IsGroundDetected())
            playerAnimation.stateMachine.ChangeState(playerAnimation.idleState);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move"); 
        //플레이어가 땅에 있으면 이동상태로 전환
        playerAnimation.stateMachine.ChangeState(playerAnimation.moveState);
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
        if (!isDashing)
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
        isDashing = true;
        rb.linearVelocity = new Vector2(moveInput.x * dashForce, 0f);
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
    }

}
