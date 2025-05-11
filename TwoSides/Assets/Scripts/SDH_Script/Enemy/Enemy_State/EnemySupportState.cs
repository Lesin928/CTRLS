using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적을 지원하는 상태를 처리하는 클래스입니다.
/// </summary>
public enum BuffType
{
    ATTACK,   // 공격 버프
    SPEED,    // 속도 버프
    HEAL      // 치유 버프
}

/// <summary>
/// 적을 지원하는 상태로, 주위 적에게 버프를 부여하고 전기 라인 이펙트를 생성하는 클래스입니다.
/// </summary>
public class EnemySupportState : EnemyState
{
    private float supportRange;    // 지원 범위
    private LayerMask enemyLayer;  // 적 레이어
    private GameObject buffPrefab; // 버프 프리팹
    private BuffType buffType;     // 버프 종류

    // 지원된 적들을 추적하는 딕셔너리
    private Dictionary<Transform, GameObject> supportedEnemies = new();

    // 생성자: 지원 범위, 버프 프리팹, 버프 종류를 초기화
    public EnemySupportState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName,
        float supportRange, GameObject buffPrefab, BuffType buffType)
        : base(enemyBase, stateMachine, animBoolName)
    {
        this.supportRange = supportRange;
        this.buffPrefab = buffPrefab;
        enemyLayer = LayerMask.GetMask("Enemy");
        this.buffType = buffType;
    }

    /// <summary>
    /// 지원 상태로 진입할 때 호출됩니다.
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        supportedEnemies.Clear(); // 이전에 지원된 적을 초기화
    }

    /// <summary>
    /// 지원 상태에서 주기적으로 실행되는 업데이트 함수입니다.
    /// </summary>
    public override void Update()
    {
        base.Update();

        // 지원 범위 내의 적들을 확인
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(enemyBase.transform.position, supportRange, enemyLayer);

        HashSet<Transform> currentEnemies = new();
        foreach (var enemy in enemiesInRange)
        {
            if (enemy.gameObject != enemyBase.gameObject) // 자신은 제외
            {
                currentEnemies.Add(enemy.transform);

                // 새로운 적에게 지원 버프를 추가
                if (!supportedEnemies.ContainsKey(enemy.transform))
                {
                    Support(enemy.transform);
                }
            }
        }

        // 범위 밖으로 나간 적들을 제거
        List<Transform> enemiesToRemove = new();
        foreach (var supported in supportedEnemies)
        {
            if (supported.Key == null || !currentEnemies.Contains(supported.Key))
            {
                RemoveSupport(supported.Key);
                enemiesToRemove.Add(supported.Key);
            }
        }

        // 제거할 적들 목록에서 실제로 제거
        foreach (var enemy in enemiesToRemove)
        {
            supportedEnemies.Remove(enemy);
        }
    }

    /// <summary>
    /// 지원 상태에서 종료될 때 호출됩니다.
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        // 지원된 모든 적의 버프와 전기 라인 이펙트를 제거
        foreach (var supported in supportedEnemies)
        {
            if (supported.Value != null)
            {
                RemoveSupport(supported.Key);
            }
        }

        supportedEnemies.Clear();
    }

    // 적에게 지원 버프를 추가하는 메서드
    private void Support(Transform targetEnemy)
    {
        Debug.Log($"{targetEnemy.name}에게 지원 버프를 추가합니다.");

        if (buffPrefab != null)
        {
            // 버프 프리팹을 적에게 적용
            GameObject buff = Object.Instantiate(buffPrefab, targetEnemy.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            buff.transform.SetParent(targetEnemy);

            // 적의 콜라이더 크기에 맞게 버프 크기 조정
            Collider2D collider = targetEnemy.GetComponent<Collider2D>();
            if (collider != null)
            {
                float targetScale = collider.bounds.size.y * 0.8f;
                buff.transform.localScale = new Vector3(targetScale, targetScale, 1f);
            }

            supportedEnemies.Add(targetEnemy, buff);

            // 전기 라인 이펙트 생성
            CreateElectricLine(targetEnemy);
        }
    }

    // 전기 라인 이펙트를 생성하는 메서드
    private void CreateElectricLine(Transform targetEnemy)
    {
        // 전기 라인 객체 생성
        GameObject lineObj = new GameObject($"ElectricLine_{targetEnemy.name}");
        lineObj.transform.SetParent(enemyBase.transform);

        // 라인 렌더러와 전기 라인 스크립트 추가
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        EnemyPixelLightningLine pixelLine = lineObj.AddComponent<EnemyPixelLightningLine>();

        // 라인 시작점과 끝점을 설정
        pixelLine.startPoint = enemyBase.transform;
        pixelLine.endPoint = targetEnemy;

        // 전기 라인의 세부 설정
        pixelLine.segmentCount = 30;
        pixelLine.waveAmplitude = 0.2f;
        pixelLine.waveFrequency = 8f;
        pixelLine.pixelSize = 0.05f;

        // 라인 렌더러의 텍스처 모드 및 두께 설정
        lr.textureMode = LineTextureMode.Tile;
        lr.alignment = LineAlignment.TransformZ;
        lr.startWidth = pixelLine.pixelSize * 0.3f;
        lr.endWidth = pixelLine.pixelSize * 0.3f;
        lr.material = new Material(Shader.Find("Sprites/Default"));

        // 버프 종류에 따른 전기 라인 색상 설정
        if (buffType == BuffType.ATTACK)
        {
            lr.startColor = new Color(1f, 0.8f, 0f);
            lr.endColor = new Color(1f, 0f, 0f);
        }
        else if (buffType == BuffType.SPEED)
        {
            lr.startColor = new Color(0f, 1f, 1f);
            lr.endColor = new Color(0f, 0f, 1f);
        }
        else if (buffType == BuffType.HEAL)
        {
            lr.startColor = new Color(1f, 1f, 1f);
            lr.endColor = new Color(0f, 1f, 0f);
        }

        // 전기 라인의 경로를 설정
        Vector3[] linePositions = new Vector3[pixelLine.segmentCount];
        for (int i = 0; i < pixelLine.segmentCount; i++)
        {
            float t = i / (float)(pixelLine.segmentCount - 1);
            Vector3 pos = Vector3.Lerp(pixelLine.startPoint.position, pixelLine.endPoint.position, t);

            // 노이즈를 추가하여 전기 라인에 흔들림 효과 적용
            float noise1 = Mathf.PerlinNoise(t * pixelLine.waveFrequency, Time.time * 0.5f) * pixelLine.waveAmplitude;
            float noise2 = Mathf.PerlinNoise(t * pixelLine.waveFrequency * 2f, Time.time * 0.6f) * pixelLine.waveAmplitude * 0.5f;
            float randomNoise = Random.Range(-0.1f, 0.1f);
            float intensity = Mathf.Lerp(0.5f, 1.5f, t);

            pos.y += (noise1 + noise2 + randomNoise) * intensity;
            pos.x += (noise1 - noise2 + randomNoise * 0.5f) * intensity * 0.5f;

            pos.x = Mathf.Round(pos.x / pixelLine.pixelSize) * pixelLine.pixelSize;
            pos.y = Mathf.Round(pos.y / pixelLine.pixelSize) * pixelLine.pixelSize;

            linePositions[i] = pos;
        }

        lr.positionCount = linePositions.Length;
        lr.SetPositions(linePositions);
    }

    // 지원을 제거하는 메서드
    private void RemoveSupport(Transform targetEnemy)
    {
        Debug.Log($"{targetEnemy.name}에게서 지원을 제거합니다.");

        if (supportedEnemies.TryGetValue(targetEnemy, out GameObject buff))
        {
            if (buff != null)
            {
                Object.Destroy(buff);
            }
        }

        // 전기 라인 이펙트 제거
        Transform electricLine = enemyBase.transform.Find($"ElectricLine_{targetEnemy.name}");
        if (electricLine != null)
            Object.Destroy(electricLine.gameObject);
    }
}
