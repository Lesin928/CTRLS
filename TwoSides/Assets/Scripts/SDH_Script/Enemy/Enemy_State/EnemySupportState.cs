using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 吏??踰꾪봽??醫낅쪟瑜??뺤쓽?섎뒗 ?닿굅??
/// </summary>
public enum BuffType
{
    ATTACK,   // 怨듦꺽??利앷?
    SPEED,    // ?ㅽ뵾??利앷?
    HEAL      // 泥대젰 ?뚮났
}

/// <summary>
/// ???좊떅??二쇰????ㅻⅨ ?곸뿉寃?踰꾪봽瑜?二쇰뒗 ?곹깭
/// </summary>
public class EnemySupportState : EnemyState
{
    private float supportRange;    // 踰꾪봽瑜?以????덈뒗 踰붿쐞
    private LayerMask enemyLayer;  // ???덉씠??
    private GameObject buffPrefab; // 踰꾪봽 ?댄럺???꾨━??
    private BuffType buffType;     // 踰꾪봽 醫낅쪟

    // 吏?먰븳 ?곴낵 ?대떦 踰꾪봽 ?댄럺?몃? 湲곕줉?섎뒗 ?뺤뀛?덈━
    private Dictionary<Transform, GameObject> supportedEnemies = new();

    // ?앹꽦??
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
    /// ?곹깭 吏꾩엯 ??珥덇린??
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        supportedEnemies.Clear(); // ?댁쟾??吏?먰뻽???곷뱾??珥덇린??
    }

    /// <summary>
    /// ?곹깭 媛깆떊: 踰붿쐞 ?????먯? 諛?吏???곸슜/?쒓굅 泥섎━
    /// </summary>
    public override void Update()
    {
        base.Update();

        // 吏??踰붿쐞 ?댁뿉 ?덈뒗 ?곸쓣 ?먯깋
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(enemyBase.transform.position, supportRange, enemyLayer);

        HashSet<Transform> currentEnemies = new();
        foreach (var enemy in enemiesInRange)
        {
            if (enemy.gameObject != enemyBase.gameObject) // ?먯떊? ?쒖쇅
            {
                currentEnemies.Add(enemy.transform);

                // ?꾩쭅 吏?먰븯吏 ?딆? ?곸씠?쇰㈃ 踰꾪봽 ?곸슜
                if (!supportedEnemies.ContainsKey(enemy.transform))
                {
                    Support(enemy.transform);
                }
            }
        }

        // 踰붿쐞 諛뽰쑝濡??섍컙 ?곸쓽 吏???쒓굅
        List<Transform> enemiesToRemove = new();
        foreach (var supported in supportedEnemies)
        {
            if (supported.Key == null || !currentEnemies.Contains(supported.Key))
            {
                RemoveSupport(supported.Key);
                enemiesToRemove.Add(supported.Key);
            }
        }

        // ?뺤뀛?덈━?먯꽌???쒓굅
        foreach (var enemy in enemiesToRemove)
        {
            supportedEnemies.Remove(enemy);
        }
    }

    /// <summary>
    /// ?곹깭 醫낅즺 ??紐⑤뱺 吏???쒓굅
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        // 吏??以묒씠???곷뱾??踰꾪봽 ?댄럺???쒓굅
        foreach (var supported in supportedEnemies)
        {
            if (supported.Value != null)
            {
                RemoveSupport(supported.Key);
            }
        }

        supportedEnemies.Clear();
    }

    // 踰꾪봽瑜??곸슜?섎뒗 ?⑥닔
    private void Support(Transform targetEnemy)
    {
        Debug.Log($"{targetEnemy.name} ?먭쾶 踰꾪봽 ?곸슜??");

        if (buffPrefab != null)
        {
            // 踰꾪봽 ?댄럺?몃? ?앹꽦?섍퀬 ???먯떇?쇰줈 遺숈엫
            GameObject buff = Object.Instantiate(buffPrefab, targetEnemy.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            buff.transform.SetParent(targetEnemy);

            // ???ш린??留욊쾶 踰꾪봽 ?댄럺???ш린 議곗젅
            Collider2D collider = targetEnemy.GetComponent<Collider2D>();
            if (collider != null)
            {
                float targetScale = collider.bounds.size.y * 0.8f;
                buff.transform.localScale = new Vector3(targetScale, targetScale, 1f);
            }

            supportedEnemies.Add(targetEnemy, buff);

            // ?꾧린 ?쇱씤 ?댄럺???앹꽦
            CreateElectricLine(targetEnemy);
        }
    }

    // ?꾧린 ?쇱씤 ?댄럺?몃? ?앹꽦?섎뒗 ?⑥닔
    private void CreateElectricLine(Transform targetEnemy)
    {
        // ?쇱씤 ?ㅻ툕?앺듃 ?앹꽦 諛?遺紐??ㅼ젙
        GameObject lineObj = new GameObject($"ElectricLine_{targetEnemy.name}");
        lineObj.transform.SetParent(enemyBase.transform);

        // 而댄룷?뚰듃 異붽?
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        EnemyPixelLightningLine pixelLine = lineObj.AddComponent<EnemyPixelLightningLine>();

        // ?쒖옉?먭낵 ?앹젏 ?ㅼ젙
        pixelLine.startPoint = enemyBase.transform;
        pixelLine.endPoint = targetEnemy;

        // ?쇱씤 ?띿꽦 ?ㅼ젙
        pixelLine.segmentCount = 30;
        pixelLine.waveAmplitude = 0.2f;
        pixelLine.waveFrequency = 8f;
        pixelLine.pixelSize = 0.05f;

        // ?쇱씤?뚮뜑???ㅼ젙
        lr.textureMode = LineTextureMode.Tile;
        lr.alignment = LineAlignment.TransformZ;
        lr.startWidth = pixelLine.pixelSize * 0.3f;
        lr.endWidth = pixelLine.pixelSize * 0.3f;
        lr.material = new Material(Shader.Find("Sprites/Default"));

        // 踰꾪봽 醫낅쪟???곕씪 ?됱긽 吏??
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

        // ???꾩튂 怨꾩궛 (Perlin Noise + ?쒕뜡 + 怨꾨떒??
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

    // 吏???쒓굅 ?⑥닔
    private void RemoveSupport(Transform targetEnemy)
    {
        Debug.Log($"{targetEnemy.name} 踰꾪봽 ?댁젣");

        if (supportedEnemies.TryGetValue(targetEnemy, out GameObject buff))
        {
            if (buff != null)
            {
                Object.Destroy(buff);
            }
        }

        // ?꾧린 ?쇱씤 ?ㅻ툕?앺듃 ?쒓굅
        Transform electricLine = enemyBase.transform.Find($"ElectricLine_{targetEnemy.name}");
        if (electricLine != null)
            Object.Destroy(electricLine.gameObject);
    }
}
