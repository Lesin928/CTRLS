using System.Collections;
using UnityEngine;

// TODO: �����丵,DaethState ����, Shift ����
// FIXME:  Set Get �߰�
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
    public GameObject skillCollider;
    public GameObject parryCollider;
    public GameObject GOD;
    #endregion

    #region Player Info   
    [Header("�÷��̾� ����")]
    [SerializeField] private float jumpForce; // ���� ��
    [SerializeField] private float dashForce; // �뽬 �� 
    [SerializeField] private float invincibilityDuration = 0.5f; //���� ����
    [SerializeField] private bool isCombo = false; //�޺� ��
    [SerializeField] private bool isAttack = false; //���� ��
    [SerializeField] private bool isDashing = false; //�뽬 ��
    [SerializeField] private bool isSkill = false; // ��ų ��� ��
    [SerializeField] private bool isinvincibility = false; //���� ��
    [SerializeField] private bool isgod = false; //���� ��
    [SerializeField] private bool isDeath = false; //�����
    [SerializeField] private bool isEvasion = false; //ȸ�ǻ���
    [SerializeField] private bool isCanParry = false; //�и�����
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
    public virtual bool Isgod
    {
        get => isgod;
        set => isgod = value;
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
    [Header("�浹 ����")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private Vector2 moveInput;
    #endregion

    #region groundCheck
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
        skillCollider.SetActive(false);
        parryCollider.SetActive(false);
        GOD.SetActive(false);
    }

    private void Start()
    {
        //�׽�Ʈ�� �÷��̾� �ʱ� ����
        /*
        MaxHp = 100f; //ü��
        CurrentHp = 100f; //ü��
        Attack = 5f; //���ݷ�
        Armor = 3f; //����
        AttackSpeed = 1f; //���ݼӵ�
        Critical = 0.1f; //ġ��Ÿ Ȯ��
        CriticalDamage = 2f; //ġ��Ÿ ���� ����
        MoveSpeed = 7f; // �̵� �ӵ� */
    }   

    public override void TakeDamage(float damage)     
    {
        if(CurrentHp <= 0) return; //���� ���·� ����   
        if (IsInvincibility) return; //���� ���̸� ������ ����
        if (Isgod) return; //���� ���̸� ������ ����
        IsInvincibility = true; //���� ���·� ����
        base.TakeDamage(damage);

        GameManager.Instance.TakeDamage(CurrentHp);
        GetComponentInChildren<HitAnim>().Flash();
    }

    protected override void Die() //���� Death ������Ʈ ����
    {
        IsDeath = true; //��� ���·� ����
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
