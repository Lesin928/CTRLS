using UnityEngine;

/// <summary>
/// ?怨몄뵠 雅뚯럩肉?????벥 ?怨밴묶??筌ｌ꼶???롫뮉 ?????
/// </summary>
public class EnemyDeadState : EnemyState
{
    // ??밴쉐?? ?怨밴묶?믩챷?? ?醫딅빍筌롫뗄???bool ??已??疫꿸퀡而??곗쨮 Dead ?怨밴묶???λ뜃由??
    public EnemyDeadState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// ?怨몄뵠 Dead ?怨밴묶??筌욊쑴??????紐꾪뀱
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        // 雅뚯럩肉???嚥??怨몄벥 ??얜즲??0??곗쨮 ??쇱젟
        enemyBase.SetZeroVelocity();

        GameManager.Instance.OnMonsterDead();
        GameManager.Instance.SetGold(10);
    }

    /// <summary>
    /// 筌??袁⑥쟿?袁⑥춳???怨밴묶??揶쏄퉮??? ?紐꺿봺椰꾧퀗? ?紐꾪뀱??롢늺 ??삵닏??븍뱜????볤탢??
    /// </summary>
    public override void Update()
    {
        base.Update();

        // ????삵닏??븍뱜 ??볤탢 鈺곌퀗援??類ㅼ뵥
        if (triggerCalled)
            GameObject.Destroy(enemyBase.gameObject);
    }

    /// <summary>
    /// ?怨몄뵠 Dead ?怨밴묶?癒?퐣 ??띿퍦 ???紐꾪뀱
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
