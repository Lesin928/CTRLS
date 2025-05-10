using UnityEngine;

public class EnemyBulletSoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip ativateSoundClips;
    [SerializeField] private float ativateSoundVolume;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ActivateSound()
    {
        if (ativateSoundClips != null)
        {
            audioSource.PlayOneShot(ativateSoundClips, ativateSoundVolume);
        }
    }
}
