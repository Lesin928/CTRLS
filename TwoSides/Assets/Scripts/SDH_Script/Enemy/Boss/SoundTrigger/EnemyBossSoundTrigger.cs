using UnityEngine;

public class EnemyBossSoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip magicSoundClip;
    [SerializeField] private float magicSoundVolume;
    [Space]
    [SerializeField] private AudioClip attack1SoundClip;
    [SerializeField] private float attack1SoundVolume;
    [Space]
    [SerializeField] private AudioClip attack2SoundClip;
    [SerializeField] private float attack2SoundVolume;
    [Space]
    [SerializeField] private AudioClip attack3SoundClip;
    [SerializeField] private float attack3SoundVolume;
    [Space]
    [SerializeField] private AudioClip attack4SoundClip;
    [SerializeField] private float attack4SoundVolume;
    [Space]
    [SerializeField] private AudioClip attack5SoundClip;
    [SerializeField] private float attack5SoundVolume;
    [Space]
    [SerializeField] private AudioClip attack6SoundClip;
    [SerializeField] private float attack6SoundVolume;
    [Space]
    [SerializeField] private AudioClip[] screamSoundClip;
    [SerializeField] private float screamSoundVolume;
    [Space]
    [SerializeField] private AudioClip deadSoundClip;
    [SerializeField] private float deadSoundVolume;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayMagicSound()
    {
        if (magicSoundClip != null)
        {
            audioSource.PlayOneShot(magicSoundClip, magicSoundVolume);
        }
    }

    private void PlayAttack1Sound()
    {
        if (attack1SoundClip != null)
        {
            audioSource.PlayOneShot(attack1SoundClip, attack1SoundVolume);
        }
    }

    private void PlayAttack2Sound()
    {
        if (attack2SoundClip != null)
        {
            audioSource.PlayOneShot(attack2SoundClip, attack2SoundVolume);
        }
    }

    private void PlayAttack3Sound()
    {
        if (attack3SoundClip != null)
        {
            audioSource.PlayOneShot(attack3SoundClip, attack3SoundVolume);
        }
    }

    private void PlayAttack4Sound()
    {
        if (attack4SoundClip != null)
        {
            audioSource.PlayOneShot(attack4SoundClip, attack4SoundVolume);
        }
    }

    private void PlayAttack5Sound()
    {
        if (attack5SoundClip != null)
        {
            audioSource.PlayOneShot(attack5SoundClip, attack5SoundVolume);
        }
    }

    private void PlayAttack6Sound()
    {
        if (attack6SoundClip != null)
        {
            audioSource.PlayOneShot(attack6SoundClip, attack6SoundVolume);
        }
    }

    private void PlayScreamSound()
    {
        if (screamSoundClip != null && screamSoundClip.Length > 0)
        {
            int index = Random.Range(0, screamSoundClip.Length);
            audioSource.PlayOneShot(screamSoundClip[index], screamSoundVolume);
        }
    }

    private void PlayDead6Sound()
    {
        if (deadSoundClip != null)
        {
            audioSource.PlayOneShot(deadSoundClip, deadSoundVolume);
        }
    }
}
