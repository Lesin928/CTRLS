using UnityEngine;

public class EnemyBossTransformTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject demonBossPrefab;
    [SerializeField] private Transform spawnPoint;

    // 메인 보스 생성
    // Dead 애니메이션에서 이벤트로 호출
    private void CallDemonBoss()
    {
        // 부모 오브젝트(SlimeBoss)의 EnemyObject 컴포넌트에서 facingDir을 가져옴
        EnemyObject slimeBoss = transform.parent.GetComponent<EnemyObject>();
        int slimeFacingDir = slimeBoss != null ? slimeBoss.facingDir : 1;

        // DemonBoss 오브젝트 생성
        GameObject boss = Instantiate(demonBossPrefab, spawnPoint.position, Quaternion.identity);

        // DemonBoss에 facingDir 설정
        DemonBossObject demonScript = boss.GetComponent<DemonBossObject>();
        if (demonScript != null)
        {
            demonScript.InitFacingDir(slimeFacingDir);
        }
    }
}