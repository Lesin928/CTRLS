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
    #endregion 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerObject = GetComponent<PlayerObject>();
    }
    private void FixedUpdate() //나중에 무브 스테이트로 이동
    {
        //대쉬 상태, 공격 상태가 아닐 때만 이동
        if (!playerObject.IsDashing && !playerObject.IsAttack)
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

        bool shouldCheckGround = groundIgnoreTimer <= 0;
    } 
    public void OnMove(InputAction.CallbackContext context)
    {
        playerObject.MoveInput = context.ReadValue<Vector2>();
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
        if (!playerObject.IsDashing && !playerObject.IsAttack)
        {
            playerAnimation.stateMachine.ChangeState(playerAnimation.dashState);
            StartCoroutine(Dash());
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    { 
        if (context.phase != InputActionPhase.Performed) return; 
        if (!playerObject.IsDashing)
        {
            // 만약 공격중이 아닐경우 공격 상태
            if (!playerObject.IsAttack) 
            {
                playerAnimation.stateMachine.ChangeState(playerAnimation.attackState);
                StartCoroutine(Attack());
            }
            else if (playerObject.IsAttack)
            {
                StartCoroutine(Combo()); 
            } 
        }
    }
    public void OnShift(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        //Debug.Log("Shift! (Shift 버튼)");
        Debug.Log("플레이어 복제");
        GameObject hit = Instantiate(player, transform.position + new Vector3(1f,0,0), Quaternion.identity);
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.isClear)
            Debug.Log("Interact! (F 버튼)");
    }
    private IEnumerator Dash()
    {
        if (dashCooldownTimer > 0)
            yield break; // 대쉬 쿨타임이 남아있으면 대쉬를 하지 않음
        dashCooldownTimer = dashCooldownDuration;
        playerObject.IsDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(playerAnimation.Getfacing() * playerObject.DashForce, 0f);
        yield return new WaitForSeconds(0.2f);
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