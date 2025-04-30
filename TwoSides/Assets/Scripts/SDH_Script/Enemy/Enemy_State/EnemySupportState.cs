using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 버프 종류 정의
/// </summary>
public enum BuffType
{
    ATTACK,
    DEFENSE,
    HEAL
}

/// <summary>
/// 주변 아군에게 버프를 부여하는 적의 상태
/// </summary>
public class EnemySupportState : EnemyState
{
    private float supportRange;           // 지원 범위
    private LayerMask enemyLayer;         // 적 레이어 마스크
    private GameObject buffPrefab; // 마법진 프리팹
    private BuffType buffType;            // 부여할 버프 타입

    // 이미 지원한 적들을 저장 (적 Transform -> 생성된 마법진 오브젝트)
    private Dictionary<Transform, GameObject> supportedEnemies = new();

    // EnemySupportState 생성자
    public EnemySupportState(
        EnemyObject enemyBase,
        EnemyStateMachine stateMachine,
        string animBoolName,
        float supportRange,
        GameObject buffPrefab,
        BuffType buffType)
        : base(enemyBase, stateMachine, animBoolName)
    {
        this.supportRange = supportRange;
        this.buffPrefab = buffPrefab;
        enemyLayer = LayerMask.GetMask("Enemy");
        this.buffType = buffType;
    }

    /// <summary>
    /// 상태 시작 시 호출
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        supportedEnemies.Clear(); // 이전에 지원한 적 목록 초기화
    }

    /// <summary>
    /// 매 프레임 호출 (주변 적 감지 및 지원)
    /// </summary>
    public override void Update()
    {
        Debug.Log("Support");
        base.Update();

        // 지원 범위 내의 적들을 감지
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(enemyBase.transform.position, supportRange, enemyLayer);

        HashSet<Transform> currentEnemies = new();
        foreach (var enemy in enemiesInRange)
        {
            // 자기 자신 제외
            if (enemy.gameObject != enemyBase.gameObject)
            {
                currentEnemies.Add(enemy.transform);

                // 아직 지원하지 않았다면 지원 수행
                if (!supportedEnemies.ContainsKey(enemy.transform))
                {
                    Support(enemy.transform);
                }
            }
        }

        // 범위를 벗어난 적들을 제거
        List<Transform> enemiesToRemove = new();
        foreach (var supported in supportedEnemies)
        {
            if (!currentEnemies.Contains(supported.Key))
            {
                RemoveSupport(supported.Key);
                enemiesToRemove.Add(supported.Key);
            }
        }

        // 딕셔너리에서 제거
        foreach (var enemy in enemiesToRemove)
        {
            supportedEnemies.Remove(enemy);
        }
    }

    /// <summary>
    /// 상태 종료 시 호출
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        // 모든 마법진과 라인 정리
        foreach (var supported in supportedEnemies)
        {
            if (supported.Value != null)
            {
                Object.Destroy(supported.Value);
            }
        }
        supportedEnemies.Clear();
    }

    // 특정 적에게 버프 지원 수행
    void Support(Transform targetEnemy)
    {
        Debug.Log($"{targetEnemy.name} 을(를) 지원합니다.");

        if (buffPrefab != null)
        {
            // 마법진 생성 및 대상에 붙이기
            GameObject magicCircle = Object.Instantiate(buffPrefab, targetEnemy.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            magicCircle.transform.SetParent(targetEnemy);

            // 대상의 콜라이더에 맞춰 마법진 크기 조절
            Collider2D collider = targetEnemy.GetComponent<Collider2D>();
            if (collider != null)
            {
                float targetScale = collider.bounds.size.y * 1.1f;
                magicCircle.transform.localScale = new Vector3(targetScale, targetScale, 1f);
            }

            supportedEnemies.Add(targetEnemy, magicCircle);

            // 전기 효과 선 생성
            CreateElectricLine(targetEnemy);
        }
    }

    // 전기 선 시각 효과 생성
    private void CreateElectricLine(Transform targetEnemy)
    {
        // 새로운 GameObject 생성
        GameObject lineObj = new GameObject($"ElectricLine_{targetEnemy.name}");
        lineObj.transform.SetParent(enemyBase.transform);

        // 라인 렌더러 및 커스텀 라인 컴포넌트 추가
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        PixelLightningLine pixelLine = lineObj.AddComponent<PixelLightningLine>();

        // 시작/끝 지점 설정
        pixelLine.startPoint = enemyBase.transform;
        pixelLine.endPoint = targetEnemy;

        // 파형 구성 설정
        pixelLine.segmentCount = 30;
        pixelLine.waveAmplitude = 0.2f;
        pixelLine.waveFrequency = 8f;
        pixelLine.pixelSize = 0.05f;

        // 라인렌더러 속성 설정
        lr.textureMode = LineTextureMode.Tile;
        lr.alignment = LineAlignment.TransformZ;
        lr.startWidth = pixelLine.pixelSize * 0.3f;
        lr.endWidth = pixelLine.pixelSize * 0.3f;
        lr.material = new Material(Shader.Find("Sprites/Default"));

        // 버프 타입에 따른 색상 지정
        if (buffType == BuffType.ATTACK)
        {
            lr.startColor = new Color(1f, 0.8f, 0f); // 노란색 → 빨간색
            lr.endColor = new Color(1f, 0f, 0f);
        }
        else if (buffType == BuffType.DEFENSE)
        {
            lr.startColor = new Color(0f, 1f, 1f); // 시안 → 파란색
            lr.endColor = new Color(0f, 0f, 1f);
        }
        else if (buffType == BuffType.HEAL)
        {
            lr.startColor = new Color(1f, 1f, 1f); // 흰색 → 초록색
            lr.endColor = new Color(0f, 1f, 0f);
        }

        // 선 위치 계산 (Perlin Noise + 랜덤)
        Vector3[] linePositions = new Vector3[pixelLine.segmentCount];
        for (int i = 0; i < pixelLine.segmentCount; i++)
        {
            float t = i / (float)(pixelLine.segmentCount - 1);
            Vector3 pos = Vector3.Lerp(pixelLine.startPoint.position, pixelLine.endPoint.position, t);

            float noise1 = Mathf.PerlinNoise(t * pixelLine.waveFrequency, Time.time * 0.5f) * pixelLine.waveAmplitude;
            float noise2 = Mathf.PerlinNoise(t * pixelLine.waveFrequency * 2f, Time.time * 0.6f) * pixelLine.waveAmplitude * 0.5f;
            float randomNoise = Random.Range(-0.1f, 0.1f);
            float intensity = Mathf.Lerp(0.5f, 1.5f, t);

            // 흔들림 적용
            pos.y += (noise1 + noise2 + randomNoise) * intensity;
            pos.x += (noise1 - noise2 + randomNoise * 0.5f) * intensity * 0.5f;

            // 픽셀 스냅 적용
            pos.x = Mathf.Round(pos.x / pixelLine.pixelSize) * pixelLine.pixelSize;
            pos.y = Mathf.Round(pos.y / pixelLine.pixelSize) * pixelLine.pixelSize;

            linePositions[i] = pos;
        }

        lr.positionCount = linePositions.Length;
        lr.SetPositions(linePositions);
    }

    // 특정 적에게 준 지원을 해제
    void RemoveSupport(Transform targetEnemy)
    {
        Debug.Log($"{targetEnemy.name} 지원 해제");

        if (supportedEnemies.TryGetValue(targetEnemy, out GameObject magicCircle))
        {
            if (magicCircle != null)
            {
                Object.Destroy(magicCircle);
            }
        }

        // 전기선 오브젝트도 제거
        Transform electricLine = enemyBase.transform.Find($"ElectricLine_{targetEnemy.name}");
        if (electricLine != null)
            Object.Destroy(electricLine.gameObject);
    }
}
