using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkForestDialogues : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int ownOrder = 0;

    string[][] dialogue = new string[10][];
    void Awake()
    {
        dialogue[0] = new string[] {
            "I must find the sorcerer who locked me up and left me in this state...\r\n\r\nAt any cost I will find him.\r\n",
        };
        dialogue[1] = new string[] {
            "I need to find how to open this door.\r\n",
        };
        dialogue[2] = new string[] {
            "Maybe this key works to open the door\r\n",
        };
        dialogue[3] = new string[] {
            "Hero: I finally found you, return me to my normal form!\r\n",
            "Dark Wizard: That will not happen, soon you will change and you will understand, you are too useful to be wasted\r\nDon't you realize that now you are more powerful?\r\n",
            "Hero: I don't mind being more powerful, being human is what makes me who I am, return me to my normal form\r\n",
            "Dark Wizard: The only way for that to happen is for me to die\r\nIt's a shame that so much power is wasted",
            "Hero: Understood, get ready!",
        };
        dialogue[4] = new string[] {
            "Skill unlocked, now you can double jump by double pressing space\r\n",
        };
        dialogue[5] = new string[] {
            "Skill unlocked, now you can stick to the wall to climb\r\n",
        };
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
            print("triger with DarkForestDialogue");
            FindObjectOfType<DialogueManager>().OpenDialogue(dialogue[ownOrder], null);
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.enabled = false;

        }
    }
}
