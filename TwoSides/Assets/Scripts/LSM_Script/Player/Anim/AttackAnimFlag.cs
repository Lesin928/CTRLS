using UnityEngine;

public class AttackAnimFlag : MonoBehaviour
{
    public PlayerObject playerObject { get; private set; }
    private void Awake()
    {
        playerObject = GetComponentInParent<PlayerObject>();
    } 
    public void FirstAttackFinished()
    {
        if (playerObject.attackCollider1.activeSelf)
        {
            playerObject.attackCollider1.SetActive(false);
        }
        playerObject.EndAttack = true;
    }

    public void SecondAttackFinished()
    {
        if (playerObject.attackCollider2.activeSelf)
        { 
            playerObject.attackCollider2.SetActive(false); 
        }
          
    }
}
