using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue", fileName = "New Dialogue")]
public class Dialogues : ScriptableObject
{

    string[][] dialogue = new string[10][];

    private void Awake()
    {
        // Initialize data
        dialogue[0] = new string[] {
            "Hey wake up, I need your help\r\n.....\r\nI need you to unlock my chains, if you do so I'll help with yours\r\n",
            "Thanks, here you go \r\n .... \r\nNow you can move try using keys 'A' and 'D' to move left and right respectively.",
            "I'll follow you to get out of here."
        };
        dialogue[1] = new string[] {
            "If you help me I'll help you",
            "Thanks,\r\nNow you can jump with the 'Space' key.",
            "I'll follow you to get out of here."
        };
        dialogue[2] = new string[] {
            "If you help me I'll help you",
            "Thanks,\r\nYou can dash with the 'Shift' key. Try using it in the air to reach more during a jump",
            "I'll follow you to get out of here."
        };
        dialogue[3] = new string[] {
            "If you help me I'll help you",
            "Thanks,\r\nNow you can attack by pressing left click.",
            "I'll follow you to get out of here."
        };
        dialogue[4] = new string[] {
            "Please help me",
            "I'll follow you to get out of here."
        };
        dialogue[5] = new string[] {
            "If you help me I'll help you",
            "Thanks, I'll follow you to get out of here",
            "Now Run!!!!!."
        };
        dialogue[6] = new string[] {
            "If you help me I'll help you",
            "Thanks,\r\nNow die!!! hahaha.",
        };
        dialogue[7] = new string[] {
            "If you help me I'll help you",
            "Thanks,\r\nNow you can dash with the 'Shift' key.",
            "I'll follow you to get out of here."
        };
        dialogue[8] = new string[] {
            "If you help me I'll help you",
            "Thanks,\r\nNow you can step on the button to activate it.",
            "I'll follow you to get out of here."
        };
        dialogue[9] = new string[] {
            "Thanks for saving us, as a thank you your death will be quick, hahaha"
        };
    }

    public string[] GetDialogue(int index)
    {
        return dialogue[index];
    }
}
