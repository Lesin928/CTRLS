using System.Collections;
using UnityEngine;

// TODO: 리펙토링,DaethState 구현, Shift 구현
// FIXME:  Set Get 추가
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
    public GameObject skillCollider;
    public GameObject parryCollider;
    #endregion

    #region Player Info   
    [Header("플레이어 정보")]
    [SerializeField] private float jumpForce; // 점프 힘
    [SerializeField] private float dashForce; // 대쉬 힘 
    [SerializeField] private float invincibilityDuration = 0.5f; //추후 세팅
    [SerializeField] private bool isCombo = false; //콤보 중
    [SerializeField] private bool isAttack = false; //공격 중
    [SerializeField] private bool isDashing = false; //대쉬 중
    [SerializeField] private bool isSkill = false; // 스킬 사용 중
    [SerializeField] private bool isinvincibility = false; //무적 중
    [SerializeField] private bool isDeath = false; //사망중
    [SerializeField] private bool isEvasion = false; //회피상태
    [SerializeField] private bool isCanParry = false; //패링가능
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
    public virtual bool IsCanParry
    {
        get => isCanParry;
        set => isCanParry = value;
    }
    public virtual bool IsEvasion
    {
        get => isEvasion;
        set => isEvasion = value;
    }   
    public virtual bool IsDeath
    {
        get => isDeath;
        set => isDeath = value;
    }

    public virtual bool IsAttack
    {
        get => isAttack;
        set => isAttack = value;
    }
    public virtual bool IsSkill
    {
        get => isSkill;
        set => isSkill = value;
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
        //테스트용 플레이어 초기 세팅
        /*
        MaxHp = 100f; //체력
        CurrentHp = 100f; //체력
        Attack = 5f; //공격력
        Armor = 3f; //방어력
        AttackSpeed = 1f; //공격속도
        Critical = 0.1f; //치명타 확률
        CriticalDamage = 2f; //치명타 피해 배율
        MoveSpeed = 7f; // 이동 속도 */
    }   

    public override void TakeDamage(float damage)     
    { 
        if (isinvincibility) return; //무적 중이면 데미지 무시
        IsInvincibility = true; //무적 상태로 변경
        base.TakeDamage(damage);

        GameManager.Instance.TakeDamage(CurrentHp);
        GetComponentInChildren<HitAnim>().Flash();
    }

    protected override void Die() //추후 Death 스테이트 구현
    {
        IsDeath = true; //사망 상태로 변경
        playerAnimation.stateMachine.ChangeState(playerAnimation.deathState); 
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
