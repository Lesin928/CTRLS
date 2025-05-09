using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 지원 버프의 종류를 정의하는 열거형
/// </summary>
public enum BuffType
{
    ATTACK,   // 공격력 증가
    SPEED,    // 스피드 증가
    HEAL      // 체력 회복
}

/// <summary>
/// 적 유닛이 주변의 다른 적에게 버프를 주는 상태
/// </summary>
public class EnemySupportState : EnemyState
{
    private float supportRange;    // 버프를 줄 수 있는 범위
    private LayerMask enemyLayer;  // 적 레이어
    private GameObject buffPrefab; // 버프 이펙트 프리팹
    private BuffType buffType;     // 버프 종류

    // 지원한 적과 해당 버프 이펙트를 기록하는 딕셔너리
    private Dictionary<Transform, GameObject> supportedEnemies = new();

    // 생성자
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
    /// 상태 진입 시 초기화
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        supportedEnemies.Clear(); // 이전에 지원했던 적들을 초기화
    }

    /// <summary>
    /// 상태 갱신: 범위 내 적 탐지 및 지원 적용/제거 처리
    /// </summary>
    public override void Update()
    {
        base.Update();

        // 지원 범위 내에 있는 적을 탐색
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(enemyBase.transform.position, supportRange, enemyLayer);

        HashSet<Transform> currentEnemies = new();
        foreach (var enemy in enemiesInRange)
        {
            if (enemy.gameObject != enemyBase.gameObject) // 자신은 제외
            {
                currentEnemies.Add(enemy.transform);

                // 아직 지원하지 않은 적이라면 버프 적용
                if (!supportedEnemies.ContainsKey(enemy.transform))
                {
                    Support(enemy.transform);
                }
            }
        }

        // 범위 밖으로 나간 적의 지원 제거
        List<Transform> enemiesToRemove = new();
        foreach (var supported in supportedEnemies)
        {
            if (supported.Key == null || !currentEnemies.Contains(supported.Key))
            {
                RemoveSupport(supported.Key);
                enemiesToRemove.Add(supported.Key);
            }
        }

        // 딕셔너리에서도 제거
        foreach (var enemy in enemiesToRemove)
        {
            supportedEnemies.Remove(enemy);
        }
    }

    /// <summary>
    /// 상태 종료 시 모든 지원 제거
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        // 지원 중이던 적들의 버프 이펙트 제거
        foreach (var supported in supportedEnemies)
        {
            if (supported.Value != null)
            {
                RemoveSupport(supported.Key);
            }
        }

        supportedEnemies.Clear();
    }

    // 버프를 적용하는 함수
    private void Support(Transform targetEnemy)
    {
        Debug.Log($"{targetEnemy.name} 에게 버프 적용됨.");

        if (buffPrefab != null)
        {
            // 버프 이펙트를 생성하고 적 자식으로 붙임
            GameObject buff = Object.Instantiate(buffPrefab, targetEnemy.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            buff.transform.SetParent(targetEnemy);

            // 적 크기에 맞게 버프 이펙트 크기 조절
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

    // 전기 라인 이펙트를 생성하는 함수
    private void CreateElectricLine(Transform targetEnemy)
    {
        // 라인 오브젝트 생성 및 부모 설정
        GameObject lineObj = new GameObject($"ElectricLine_{targetEnemy.name}");
        lineObj.transform.SetParent(enemyBase.transform);

        // 컴포넌트 추가
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        EnemyPixelLightningLine pixelLine = lineObj.AddComponent<EnemyPixelLightningLine>();

        // 시작점과 끝점 설정
        pixelLine.startPoint = enemyBase.transform;
        pixelLine.endPoint = targetEnemy;

        // 라인 속성 설정
        pixelLine.segmentCount = 30;
        pixelLine.waveAmplitude = 0.2f;
        pixelLine.waveFrequency = 8f;
        pixelLine.pixelSize = 0.05f;

        // 라인렌더러 설정
        lr.textureMode = LineTextureMode.Tile;
        lr.alignment = LineAlignment.TransformZ;
        lr.startWidth = pixelLine.pixelSize * 0.3f;
        lr.endWidth = pixelLine.pixelSize * 0.3f;
        lr.material = new Material(Shader.Find("Sprites/Default"));

        // 버프 종류에 따라 색상 지정
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

        // 선 위치 계산 (Perlin Noise + 랜덤 + 계단화)
        Vector3[] linePositions = new Vector3[pixelLine.segmentCount];
        for (int i = 0; i < pixelLine.segmentCount; i++)
        {
            float t = i / (float)(pixelLine.segmentCount - 1);
            Vector3 pos = Vector3.Lerp(pixelLine.startPoint.position, pixelLine.endPoint.position, t);

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

    // 지원 제거 함수
    private void RemoveSupport(Transform targetEnemy)
    {
        Debug.Log($"{targetEnemy.name} 버프 해제");

        if (supportedEnemies.TryGetValue(targetEnemy, out GameObject buff))
        {
            if (buff != null)
            {
                Object.Destroy(buff);
            }
        }

        // 전기 라인 오브젝트 제거
        Transform electricLine = enemyBase.transform.Find($"ElectricLine_{targetEnemy.name}");
        if (electricLine != null)
            Object.Destroy(electricLine.gameObject);
    }
}
