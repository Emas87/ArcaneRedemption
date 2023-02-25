using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescued : MonoBehaviour
{
    Dialogues dialogue;

    [SerializeField] int ownOrder = 0;


    // Start is called before the first frame update
    void Start()
    {
        dialogue = ScriptableObject.CreateInstance<Dialogues>();
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
            FindObjectOfType<DialogueManager>().OpenDialogue(dialogue.GetDialogue(ownOrder), this);

        }
    }
}
