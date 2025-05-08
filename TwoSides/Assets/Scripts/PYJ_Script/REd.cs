using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("스폰할 프리팹")]
    public GameObject prefabToSpawn;

    [Header("스폰 위치 (인스펙터에서 조정)")]
    public Vector3 spawnPosition = Vector3.zero;

    [Header("스폰 회전 (선택 사항)")]
    public Vector3 spawnRotationEuler = Vector3.zero;

    // 인스펙터에서 호출할 수 있게 만듭니다 (Editor에서 버튼 사용 가능)
    public void SpawnAtDefinedPosition()
    {
        if (prefabToSpawn != null)
        {
            Quaternion spawnRotation = Quaternion.Euler(spawnRotationEuler);
            Instantiate(prefabToSpawn, spawnPosition, spawnRotation);
        }
        else
        {
            Debug.LogWarning("프리팹이 할당되지 않았습니다!");
        }
    }
}
