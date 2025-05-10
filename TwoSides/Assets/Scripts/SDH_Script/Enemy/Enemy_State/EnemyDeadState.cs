using UnityEngine;

/// <summary>
/// ?곸씠 二쎌뿀???뚯쓽 ?곹깭瑜?泥섎━?섎뒗 ?대옒??
/// </summary>
public class EnemyDeadState : EnemyState
{
    // ?앹꽦?? ?곹깭癒몄떊, ?좊땲硫붿씠??bool ?대쫫??湲곕컲?쇰줈 Dead ?곹깭瑜?珥덇린??
    public EnemyDeadState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// ?곸씠 Dead ?곹깭??吏꾩엯?????몄텧
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        // 二쎌뿀?쇰?濡??곸쓽 ?띾룄瑜?0?쇰줈 ?ㅼ젙
        enemyBase.SetZeroVelocity();

        GameManager.Instance.OnMonsterDead();
        GameManager.Instance.SetGold(100);
    }

    /// <summary>
    /// 留??꾨젅?꾨쭏???곹깭瑜?媛깆떊?? ?몃━嫄곌? ?몄텧?섎㈃ ?ㅻ툕?앺듃瑜??쒓굅??
    /// </summary>
    public override void Update()
    {
        base.Update();

        // ???ㅻ툕?앺듃 ?쒓굅 議곌굔 ?뺤씤
        if (triggerCalled)
            GameObject.Destroy(enemyBase.gameObject);
    }

    /// <summary>
    /// ?곸씠 Dead ?곹깭?먯꽌 ?섍컝 ???몄텧
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
