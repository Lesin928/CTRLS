using UnityEngine;

public class EnemySoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip[] hitSoundClips;
    [SerializeField] private float hitSoundVolume;
    [SerializeField] private AudioClip attackSoundClip;
    [SerializeField] private float attackSoundVolume;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHitSound()
    {
        if (hitSoundClips != null && hitSoundClips.Length > 0)
        {
            int index = Random.Range(0, hitSoundClips.Length);
            audioSource.PlayOneShot(hitSoundClips[index], hitSoundVolume);
        }
    }

    private void PlayAttackSound()
    {
        if (attackSoundClip != null)
        {
            audioSource.PlayOneShot(attackSoundClip, attackSoundVolume);
        }
    }
}
