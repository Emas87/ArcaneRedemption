using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue", fileName = "New Dialogue")]
public class Dialogues : ScriptableObject
{

    string[][] dialogue = new string[10][];

    private void Awake()
    {
        // Initialize data
        dialogue[0] = new string[] {
            "Hey wake up, I need your help\r\n.....\r\nI need you to set me free, if you do so I'll help you\r\n",
            "Thanks, here you go \r\n ....",
            "Now you can move try using keys 'A' and 'D' to move left and right respectively.",
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
            "Thanks,\r\nNow you can attack by pressing 'Left Click'.",
            "I'll follow you to get out of here."
        };
        dialogue[4] = new string[] {
            "If you help me I'll help you",
            "Thanks,\r\nNow let me help you, You can open chests with 'Right Click'.",
            "I'll follow you to get out of here."
        };
        dialogue[5] = new string[] {
            "If you help me I'll help you",
            "Thanks, Be careful with the water, it slows you down",
            "I'll follow you to get out of here",
            "Now Run!!!!!."
        };
        dialogue[6] = new string[] {
            "If you help me I'll help you",
            "Thanks,\r\nNow let me help you, You can use levers with 'Right Click'.",
            "Now be carefull!!!",
        };
        dialogue[7] = new string[] {
            "If you help me I'll help you",
            "Thanks,\r\nNext platforms has a time limit, once you push the lever you wil have some seconds to cross them",
            "I'll follow you to get out of here."
        };
        dialogue[8] = new string[] {
            "Please help me",
            "Thanks",
            "I'll follow you to get out of here."
        };
    }

    public string[] GetDialogue(int index)
    {
        return dialogue[index];
    }
}
