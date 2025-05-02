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
    protected Rigidbody2D rb;
    public GameObject attackCollider1;
    public GameObject attackCollider2;
    public bool isCombo = false;
    public bool isAttack = false;
    public bool endAttack = false;
    #endregion

    #region Player state
    [Header("플레이어 스탯")]
    [SerializeField] public float jumpForce; //추후 스크립터블 오브젝트로 세팅
    [SerializeField] public float dashForce; //추후 스크립터블 오브젝트로 세팅
    #endregion

    #region Setter & Getter
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
    public virtual bool IsDashing
    {
        get => isDashing;
        set => isDashing = value;
    }
    #endregion

    #region Collision
    [Header("충돌 정보")]
    //public Transform attackCheck;
    //public float attackCheckRadius;

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private bool isDashing = false;
    #endregion
     
    #region 충돌 함수
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        //Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
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
        jumpForce = 8f; 
        dashForce = 15f;
        MoveSpeed = 7f;  
        Attack = 3f;
    }   

    public override void TakeDamage(float damage)     
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {

    }

}
