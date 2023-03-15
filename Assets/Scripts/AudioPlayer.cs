using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("PlayerMovement")]
    [SerializeField] AudioClip stepsClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip groundClip;
    [SerializeField] AudioClip attackClip;
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

    void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }

    [Header("Triggers")]
    [SerializeField] AudioClip chestClip;
    public void PlayChest()
    {
        PlayClip(chestClip);
    }
}
