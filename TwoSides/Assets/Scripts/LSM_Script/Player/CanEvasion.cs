using UnityEngine;

public class CanEvasion : MonoBehaviour
{
    PlayerObject playerObject;

    private void Awake()
    {
        playerObject = GetComponentInParent<PlayerObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Effect"))
        { 
            playerObject.IsEvasion = true; //회피 가능
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Effect"))
        { 
            playerObject.IsEvasion = false; //회피 불가능
        }
    }
}
