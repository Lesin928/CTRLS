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
            playerObject.IsEvasion = true; //ȸ�� ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Effect"))
        { 
            playerObject.IsEvasion = false; //ȸ�� �Ұ���
        }
    }
}
