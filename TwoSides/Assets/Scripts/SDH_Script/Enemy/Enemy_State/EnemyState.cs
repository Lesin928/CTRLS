using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected EnemyObject enemyBase;
    protected Rigidbody2D rb;

    protected bool triggerCalled;
    protected float stateTimer;
    private string animBoolName;

    public EnemyState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}