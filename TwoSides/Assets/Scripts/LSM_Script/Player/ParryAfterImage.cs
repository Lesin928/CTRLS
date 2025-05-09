using UnityEngine;

public class ParryAfterImage : MonoBehaviour
{
    public GameObject afterImagePrefab;
    public float spawnInterval = 0.05f;
    public float imageLifetime = 0.3f;

    [SerializeField] private bool isDodging = false;

    public void StartDodgeEffect()
    {
        if (!isDodging)
            StartCoroutine(SpawnAfterImages());
    }

    private System.Collections.IEnumerator SpawnAfterImages()
    {
        isDodging = true;
        float timer = 0f;

        while (timer < imageLifetime)
        {
            GameObject clone = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            Destroy(clone, imageLifetime);
            yield return new WaitForSeconds(spawnInterval);
            timer += spawnInterval;
        }

        isDodging = false;
    }
}