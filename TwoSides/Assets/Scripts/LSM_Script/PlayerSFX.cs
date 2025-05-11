using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip attack1Clip;
    public AudioClip attack2Clip;
    public AudioClip dashClip;
    public AudioClip parryAttackClip;
    public AudioClip deathClip;
    public AudioClip fallClip;
    public AudioClip jumpClip;
    public AudioClip hurtClip;
    public AudioClip move1Clip;
    public AudioClip move2Clip;
    public AudioClip skill1Clip;
    public AudioClip skill2Clip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource 컴포넌트가 필요합니다.");
        }
    } 
    public void PlayAttack1() => PlayClip(attack1Clip);
    public void PlayAttack2() => PlayClip(attack2Clip);
    public void PlayDash() => PlayClip(dashClip);
    public void PlayParryAttack() => PlayClip(parryAttackClip);
    public void PlayDeath() => PlayClip(deathClip);
    public void PlayJump() => PlayClip(jumpClip);
    public void PlayHurt() => PlayClip(hurtClip);
    public void PlayMove1() => PlayClip(move1Clip);
    public void PlayMove2() => PlayClip(move2Clip);
    public void PlaySkill1() => PlayClip(skill1Clip);
    public void PlaySkill2() => PlayClip(skill2Clip);

    public void PlayClip(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        { 
            if (clip == dashClip)
                audioSource.volume = 1f; // 대쉬 소리 볼륨 조정
            else
                audioSource.volume = 0.5f;

            audioSource.PlayOneShot(clip);
        }
    }
}
