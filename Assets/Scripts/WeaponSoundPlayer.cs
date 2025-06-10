using UnityEngine;

public class WeaponSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bulletShotSound;
    public AudioClip missileLaunchSound;

    public void PlayBulletShot()
    {
        audioSource.PlayOneShot(bulletShotSound);
    }

    public void PlayMissileLaunch()
    {
        audioSource.PlayOneShot(missileLaunchSound);
    }
}