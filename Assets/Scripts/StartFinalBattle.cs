using UnityEngine;

public class StartFinalBattle : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Player"))
        {
            //trigger.StartDialogue();
            Wizard wizard = GameObject.FindGameObjectWithTag("Wizard").GetComponent<Wizard>();
            wizard.startBattle();
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.enabled = false;
            AudioSourceController audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSourceController>();
            audio.putFinalBattleMusic();

        }
    }

    public void Reset(){
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = true;
        AudioSourceController audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSourceController>();
        audio.putLvl2Music();
        
    }
}
