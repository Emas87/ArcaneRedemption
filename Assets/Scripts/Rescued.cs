    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescued : MonoBehaviour
{

    [SerializeField] int ownOrder = 0;

    Dialogues dialogue;
    bool rescued = false;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        dialogue = ScriptableObject.CreateInstance<Dialogues>();
        animator = GetComponent<Animator>();
        if (animator == null )
        {
            Debug.LogError("No Animator in Rescued gameobject");
            return;
        }
        animator.SetBool("rescued", rescued);
    }

    // Update is called once per frame
    void Update()
    {

                
    }

    public void Rescue()
    {
        animator.SetBool("rescued", true);
        transform.localScale = Vector3.one;
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

    public void SpecialAction(int indexMessage)
    {
        if (ownOrder == 0 && indexMessage == 2) {
            GameObject[] triggers = GameObject.FindGameObjectsWithTag("Trigger");
            foreach (GameObject trigger in triggers)
            {
                if (trigger.name == "chains")
                {
                    Destroy(trigger);
                }
            }
        }
    }
}
