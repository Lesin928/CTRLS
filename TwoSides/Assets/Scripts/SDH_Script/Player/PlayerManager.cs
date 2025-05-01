using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerObject player;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }
}
