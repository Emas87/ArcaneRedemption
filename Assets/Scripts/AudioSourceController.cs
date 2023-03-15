using UnityEngine;

public class AudioSourceController : MonoBehaviour
{


    [SerializeField] AudioClip lvl1Music;
    [SerializeField] AudioClip lvl2Music;
    [SerializeField] AudioClip finalBattleMusic;
    [SerializeField] AudioSource myAudioSource;




    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void putLvl1Music(){
        myAudioSource.clip = lvl1Music;
        myAudioSource.Play();
        myAudioSource.volume = 1f;
    }
    public void putLvl2Music(){
        myAudioSource.clip = lvl2Music;
        myAudioSource.Play();
        myAudioSource.volume = 0.1f;
    }

    public void putFinalBattleMusic(){
        myAudioSource.clip = finalBattleMusic;
        myAudioSource.Play();
        myAudioSource.volume = 0.1f;
    }
}
