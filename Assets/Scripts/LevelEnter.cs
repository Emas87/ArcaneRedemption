using UnityEngine;
public class LevelEnter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player"))
        {
            PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            player.moveToScene1();
            AudioSourceController audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSourceController>();
            audio.putLvl1Music();
        }
    }
}
