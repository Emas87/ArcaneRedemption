using System;
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
    public void DestroyRescued()
    {
        if(ownOrder == 5) {
            GameObject[] triggers = GameObject.FindGameObjectsWithTag("Trigger");
            foreach(GameObject trigger in triggers)
            {
                if(trigger.name == "startBoulder")
                {
                   BoulderManager boulderManager = trigger.GetComponent<BoulderManager>();
                   boulderManager.StartBoulder();
                }
            }
        } else if (ownOrder == 6)
        {
            GameObject[] triggers = GameObject.FindGameObjectsWithTag("Trigger");
            foreach (GameObject trigger in triggers)
            {
                if (trigger.name == "Medieval_lever")
                {
                    Lever leverManager = trigger.GetComponent<Lever>();
                    leverManager.Switch();
                }
            }
        }
    }
}
