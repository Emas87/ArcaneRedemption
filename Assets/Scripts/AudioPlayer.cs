using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("World")]
    [SerializeField] AudioClip complete;

    public void ChangeMusic(AudioClip newMusic)
    {
        GetComponent<AudioSource>().clip = newMusic;
        GetComponent<AudioSource>().Play();
    }

    public void PlayComplete()
    {
        PlayClip(complete);
    }

    [Header("PlayerMovement")]
    [SerializeField] AudioClip stepsClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip groundClip;
    [SerializeField] AudioClip attackClip;
    [SerializeField] AudioClip slashClip;
    [Range(0,1)] public float volume = 1f;

    public void PlaySteps()
    {
        PlayClip(stepsClip);
    }

    public void PlayJump()
    {
        PlayClip(jumpClip);
    }

    public void PlayGround()
    {
        PlayClip(groundClip);
    }

    public void PlayAttack()
    {
        PlayClip(attackClip);
    }

    public void PlaySlash()
    {
        float newVolume = volume < 0.2f ? 0f : volume - 0.2f;
        PlayClip(slashClip, newVolume);
    }

    void PlayClip(AudioClip clip, float vol = 0)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, vol == 0? volume: vol);
        }
    }

    [Header("Triggers")]
    [SerializeField] AudioClip chestClip;
    public void PlayChest()
    {
        PlayClip(chestClip);
    }
}
