using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossDoor : MonoBehaviour
{
    public Animator camAnim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            player.cinematic = true;

            SlimeBoss boss = FindObjectOfType<SlimeBoss>();
            boss.cinematic = true;

            PlayableDirector[] directors = FindObjectsOfType<PlayableDirector>();
            foreach (var director in directors)
            {
                if(director.name == "BossDirector")
                {
                    director.Play();
                }
            }
            Invoke(nameof(StopBossScene), 22f);
        }
    }

    void StopBossScene()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.cinematic = false;
        SlimeBoss boss = FindObjectOfType<SlimeBoss>();
        boss.cinematic = false;
        PlayableDirector[] directors = FindObjectsOfType<PlayableDirector>();
        foreach (var director in directors)
        {
            if (director.name == "BossDirector")
            {
                director.Stop();
            }
        }
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
