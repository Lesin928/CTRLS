using UnityEngine;

public class HitAnimFlag : MonoBehaviour
{
    public void AnimFinished()
    {
        Destroy(gameObject);
    }
}
