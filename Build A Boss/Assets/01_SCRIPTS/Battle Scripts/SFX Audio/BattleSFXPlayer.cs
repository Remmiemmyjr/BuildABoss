using UnityEngine;

public class BattleSFXPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static BattleSFXPlayer Instance;

    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private AudioClip attackSFX;
    [SerializeField] private AudioClip defendSFX;
    [SerializeField] private AudioClip specialMoveSFX;
    [SerializeField] private AudioClip ingratiateSFX;
    [SerializeField] private AudioClip recruitSFX;
    [SerializeField] private AudioClip winSFX;
    [SerializeField] private AudioClip loseSFX;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayDamageSFX()
    {
        sfxPlayer.PlayOneShot(damageSFX);
    }

    public void PlayAttackSFX()
    {
        sfxPlayer.PlayOneShot(attackSFX);
        Invoke("PlayDamageSFX", .35f);
    }

    public void PlayDefendSFX()
    {
        sfxPlayer.PlayOneShot(defendSFX);
    }

    public void PlaySpecialMoveSFX()
    {
        sfxPlayer.PlayOneShot(specialMoveSFX);
    }

    public void PlayIngratiateSFX()
    {
        sfxPlayer.PlayOneShot(ingratiateSFX);
    }

    public void PlayRecruitSFX()
    {
        sfxPlayer.PlayOneShot(specialMoveSFX);
    }

    public void PlayWinSFX()
    {
        sfxPlayer.PlayOneShot(winSFX);
    }

    public void PlayLoseSFX()
    {
        sfxPlayer.PlayOneShot(loseSFX);
    }
}
