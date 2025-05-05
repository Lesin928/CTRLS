using System.Collections;
using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾��� �Ӽ��� �����ϴ� Ŭ����
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
    [Header("�÷��̾� ����")]
    [SerializeField] private float jumpForce; //���� ��ũ���ͺ� ������Ʈ�� ����
    [SerializeField] private float dashForce; //���� ��ũ���ͺ� ������Ʈ�� ����
    [SerializeField] private float invincibilityDuration = 0.5f; //���� ��ũ���ͺ� ������Ʈ�� ����
    [SerializeField] private bool isCombo = false; //�޺� ��
    [SerializeField] private bool isAttack = false; //���� ��
    [SerializeField] private bool isDashing = false; //�뽬 ��
    [SerializeField] private bool isinvincibility = false; //���� ��
    [SerializeField] private bool endAttack = false; //���� ���� 
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
                StartCoroutine(DisableInvincibility(invincibilityDuration)); // 0.5�� �Ŀ� ���� ����
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
    [Header("�浹 ����")]  
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private Vector2 moveInput;
    #endregion
    
    #region �浹 �Լ�
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
        //���� ��ũ���ͺ� ������Ʈ�� ����
        jumpForce = 10f; 
        dashForce = 15f;
        MoveSpeed = 7f;  
        Attack = 3f;
    }   

    public override void TakeDamage(float damage)     
    {
        if (isinvincibility) return; //���� ���̸� ������ ����
        IsInvincibility = true; //���� ���·� ����
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
            //��� ���� ������Ʈ�� Interactive �±׸� ������ ���� ���, ����
            IObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactive"))
        {
            //��� ���� ������Ʈ�� Interactive �±׸� ������ ���� ���, ����
            IObject = null;
        }
    } 
}
