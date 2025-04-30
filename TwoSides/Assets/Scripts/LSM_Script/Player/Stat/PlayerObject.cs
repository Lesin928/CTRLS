using UnityEngine;
// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)
public class PlayerObject : CharacterObject
{
    #region Components //PlayerAnimation ��ũ��Ʈ�� �ν����Ϳ� �ִ� ������Ʈ��
    protected PlayerStateMachine stateMachine;
    protected PlayerAnimation playerAnimation;
    protected PlayerObject playerObject;
    protected Rigidbody2D rb;
    #endregion


    [Header("�浹 ����")]
    //public Transform attackCheck;
    //public float attackCheckRadius;

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;


    #region �浹 �Լ�
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
    }

    public override void TakeDamage(float damage)     
    {

    }

    public override void Die()
    {

    }

}
