using System.Collections;
using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어의 속성을 관리하는 클래스
/// </summary>
public class PlayerObject : CharacterObject
{
    #region Components     
    protected PlayerStateMachine stateMachine;
    protected PlayerAnimation playerAnimation;
    protected PlayerObject playerObject;
    protected GameObject iObject;
    protected Rigidbody2D rb;    
    public GameObject attackCollider1;
    public GameObject attackCollider2; 
    #endregion

    #region Player Info   
    [Header("플레이어 정보")]
    [SerializeField] private float jumpForce; //추후 스크립터블 오브젝트로 세팅
    [SerializeField] private float dashForce; //추후 스크립터블 오브젝트로 세팅
    [SerializeField] private float invincibilityDuration = 0.5f; //추후 스크립터블 오브젝트로 세팅
    [SerializeField] private bool isCombo = false; //콤보 중
    [SerializeField] private bool isAttack = false; //공격 중
    [SerializeField] private bool isDashing = false; //대쉬 중
    [SerializeField] private bool isinvincibility = false; //무적 중
    [SerializeField] private bool endAttack = false; //공격 종료 
    #endregion

    #region Setter & Getter
    public virtual GameObject IObject
    {
        get => iObject;
        set => iObject = value;
    }
    public virtual bool IsInvincibility
    {
        get => isinvincibility;
        set
        {
            isinvincibility = value;
            if (isinvincibility)
            {
                StartCoroutine(DisableInvincibility(invincibilityDuration)); // 0.5초 후에 무적 해제
            }
        }
    }
    public virtual bool IsCombo
    {
        get => isCombo;
        set => isCombo = value;
    }
    public virtual bool IsAttack
    {
        get => isAttack;
        set => isAttack = value;
    }
    public virtual bool EndAttack
    {
        get => endAttack;
        set => endAttack = value;
    }
    public virtual bool IsDashing
    {
        get => isDashing;
        set => isDashing = value;
    }

    public virtual Vector2 MoveInput
    {
        get => moveInput;
        set
        {
            moveInput = value;
        }
    }
    public virtual float JumpForce
    {
        get => jumpForce;
        set
        {
            jumpForce = value; 
        }
    }
    public virtual float DashForce
    {
        get => dashForce;
        set
        {
            dashForce = value; 
        }
    } 
    #endregion

    #region Collision Info  
    [Header("충돌 정보")]  
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private Vector2 moveInput;
    #endregion
    
    #region 충돌 함수
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }
    #endregion
      
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerObject = GetComponent<PlayerObject>();
        attackCollider1.SetActive(false);
        attackCollider2.SetActive(false);
    }

    private void Start()
    {
        //추후 스크립터블 오브젝트로 세팅
        jumpForce = 10f; 
        dashForce = 15f;
        MoveSpeed = 7f;  
        Attack = 3f;
    }   

    public override void TakeDamage(float damage)     
    {
        if (isinvincibility) return; //무적 중이면 데미지 무시
        IsInvincibility = true; //무적 상태로 변경
        base.TakeDamage(damage);        
        GetComponentInChildren<HitAnim>().Flash();
    }

    protected override void Die()
    {

    }

    private IEnumerator DisableInvincibility(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsInvincibility = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactive"))
        {
            //대상 게임 오브젝트가 Interactive 태그를 가지고 있을 경우, 저장
            IObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactive"))
        {
            //대상 게임 오브젝트가 Interactive 태그를 가지고 있을 경우, 해제
            IObject = null;
        }
    } 
}
