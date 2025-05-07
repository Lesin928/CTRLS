using UnityEngine;

public class Moving_Thin_Ground : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;       // 如甸覆 加档
    [SerializeField] private float moveAmount = 1f;      // 如甸覆 芭府

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offsetX = Mathf.Sin(Time.time * moveSpeed) * moveAmount;
        transform.position = new Vector3(startPosition.x + offsetX, startPosition.y, startPosition.z);
    }
}
